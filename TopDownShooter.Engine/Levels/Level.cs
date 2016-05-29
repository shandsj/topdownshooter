// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Level.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Levels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using TiledSharp;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Defines a level.
    /// </summary>
    public class Level : GameObject
    {
        /// <summary>
        /// The <see cref="TiledSharp.TmxMap" />.
        /// </summary>
        private readonly TmxMap map;

        /// <summary>
        /// The collection of tiles, mapped from tile coordinates.
        /// </summary>
        private readonly Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
        private readonly ICollisionSystem collisionSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="Level" /> class.
        /// </summary>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem"/>.</param>
        /// <param name="map">
        /// The <see cref="TmxMap" /> used to generate the level.
        /// </param>
        /// <param name="id">The game object identifier</param>
        public Level(int id, ICollisionSystem collisionSystem, TmxMap map)
            : base(id)
        {
            this.collisionSystem = collisionSystem;
            this.map = map;
        }

        /// <summary>
        /// Gets the collection of <see cref="Tile" /> objects.
        /// </summary>
        public IEnumerable<Tile> Tiles => this.tiles.Values.AsEnumerable();

        /// <summary>
        /// Gets the width of the game object.
        /// </summary>
        public override int Width { get; }

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public override int Height { get; }

        /// <summary>
        /// Draws the game object with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(ISpriteBatchAdapter spriteBatch, GameTime gameTime)
        {
            foreach (var tile in this.Tiles)
            {
                tile.Draw(spriteBatch, gameTime);
            }

            base.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Gets the tile at the specified tile coordinates.
        /// </summary>
        /// <param name="point">The location of the tile, in tile coordinates.</param>
        /// <returns>The <see cref="Tile" />.</returns>
        public ITile GetTile(Point point)
        {
            Tile tile;
            this.tiles.TryGetValue(point, out tile);
            return tile;
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public override void LoadContent(IContentManagerAdapter contentManager)
        {
            var textures = new Dictionary<TmxTileset, Texture2D>();

            // Grab the first tileset.
            foreach (var tileSet in this.map.Tilesets)
            {
                var texture = contentManager.Load<Texture2D>("Tilesets/" + tileSet.Name);
                if (!tileSet.Columns.HasValue)
                {
                    throw new InvalidOperationException("Need to specifiy column in tile set.");
                }

                textures.Add(tileSet, texture);
            }

            var layer = this.map.Layers["Default"];
            if (layer != null)
            {
                this.ImportLayer(layer, textures);
            }

            base.LoadContent(contentManager);
        }

        /// <summary>
        /// Imports the tiles from the specified <see cref="TmxLayer" /> using the specified textures.
        /// </summary>
        /// <param name="layer">The <see cref="TmxLayer" /> to import from.</param>
        /// <param name="textures">The textures map that contains <see cref="TmxTileset" /> to <see cref="Texture2D" /> mappings.</param>
        private void ImportLayer(TmxLayer layer, Dictionary<TmxTileset, Texture2D> textures)
        {
            foreach (var tile in layer.Tiles)
            {
                // If the tile doesn't have a GID, then it is a transparent tile
                if (tile.Gid == 0)
                {
                    continue;
                }

                var tileSet = textures.Keys.OrderByDescending(kvp => kvp.FirstGid).FirstOrDefault(ts => tile.Gid >= ts.FirstGid);
                if (tileSet == null)
                {
                    throw new InvalidOperationException("Tile set not found");
                }

                // Load the texture
                Texture2D texture;
                if (!textures.TryGetValue(tileSet, out texture))
                {
                    throw new InvalidOperationException("Texture not found");
                }

                if (!tileSet.Columns.HasValue)
                {
                    throw new InvalidOperationException("Need to specifiy column in tile set.");
                }

                var columCount = tileSet.Columns.Value;

                // Determine the index of the tile into the tile set.
                var index = tile.Gid - tileSet.FirstGid;

                var y = (index / tileSet.Columns.Value) + 1;
                var x = (index % tileSet.Columns.Value) + 1;

                bool isBlocking = false;
                if (tileSet.Tiles.Count > 0)
                {
                    var tileSetTile = tileSet.Tiles.FirstOrDefault(t => t.Id == index);
                    if (tileSetTile != null)
                    {
                        string isBlockingValue;
                        if (tileSetTile.Properties.TryGetValue("IsBlocking", out isBlockingValue))
                        {
                            isBlocking = true;
                        }
                    }
                }

                var tileInteractionType = isBlocking ? TileInteractionType.Blocking : TileInteractionType.NonBlocking;

                var myTile = new Tile(
                    CollisionSystem.NextGameObjectId++,
                    this.collisionSystem,
                    tileInteractionType,
                    texture,
                    new Vector2(tile.X * tileSet.TileWidth, tile.Y * tileSet.TileHeight),
                    new Point(tile.X, tile.Y),
                    tileSet.TileWidth,
                    tileSet.TileHeight,
                    new Vector2((x - 1) * tileSet.TileWidth, (y - 1) * tileSet.TileHeight));

                myTile.Initialize();
                this.tiles.Add(new Point(tile.X, tile.Y), myTile);
            }
        }
    }
}