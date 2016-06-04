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
        private readonly ICollisionSystem collisionSystem;

        /// <summary>
        /// The <see cref="TiledSharp.TmxMap" />.
        /// </summary>
        private readonly ITmxMapAdapter map;

        private readonly int halfTileHeight;

        private readonly int halfTileWidth;

        private ITileFactory tileFactory;

        private ITileCollection tileCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Level" /> class.
        /// </summary>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="map">
        /// The <see cref="ITmxMapAdapter" /> used to generate the level.
        /// </param>
        /// <param name="id">The game object identifier</param>
        public Level(int id, ICollisionSystem collisionSystem, ITmxMapAdapter map)
            : this(id, collisionSystem, map, new TileFactory(), new TileCollection())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Level" /> class.
        /// </summary>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="map">
        /// The <see cref="ITmxMapAdapter" /> used to generate the level.
        /// </param>
        /// <param name="id">The game object identifier</param>
        /// <param name="tileFactory">The <see cref="ITileFactory"/> used to generate tiles.</param>
        /// <param name="tileCollection">The <see cref="ITileCollection"/> used to store tiles.</param>
        /// <remarks>Internal for unit testing.</remarks>
        internal Level(int id, ICollisionSystem collisionSystem, ITmxMapAdapter map, ITileFactory tileFactory, ITileCollection tileCollection)
            : base(id)
        {
            this.collisionSystem = collisionSystem;
            this.map = map;
            this.tileFactory = tileFactory;
            this.tileCollection = tileCollection;

            this.halfTileHeight = map.TileHeight / 2;
            this.halfTileWidth = map.TileWidth / 2;
        }

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public override int Height { get; }

        /// <summary>
        /// Gets the width of the game object.
        /// </summary>
        public override int Width { get; }

        /// <summary>
        /// Draws the game object with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="camera">The <see cref="ICamera" />.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(ICamera camera, ISpriteBatchAdapter spriteBatch, GameTime gameTime)
        {
            // Calculate the tile sizes in the current camera view based off zoom level.
            var scaledTileWidth = (int)(this.map.TileWidth * camera.Zoom);
            var scaledTileHeight = (int)(this.map.TileHeight * camera.Zoom);

            // Calculate the bounds we will use to draw tiles from. We will go a tile extra
            // just to make sure the entire screen is covered in tiles.
            var boundWidth = camera.Bounds.Width + scaledTileWidth;
            var boundHeight = camera.Bounds.Height + scaledTileHeight;

            for (int i = 0; i < boundWidth; i += scaledTileWidth)
            {
                for (int j = 0; j < boundHeight; j += scaledTileHeight)
                {
                    // Grab the world coordinates of the current iteration, and then figure out the nearest tile coordinates.
                    var worldCoordinates = camera.GetWorldCoordinates(new Vector2(i, j));
                    var nearestTileLocation = new Vector2(
                        (int)worldCoordinates.X / this.map.TileWidth * this.map.TileWidth,
                        (int)worldCoordinates.Y / this.map.TileHeight * this.map.TileHeight);

                    // Get the tile and render it!
                    var tile = this.tileCollection.GetTile(nearestTileLocation);
                    tile?.Draw(camera, spriteBatch, gameTime);
                }
            }

            base.Draw(camera, spriteBatch, gameTime);
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

                var position = new Vector2(tile.X * tileSet.TileWidth, tile.Y * tileSet.TileHeight);
                var myTile = this.tileFactory.CreateTile(
                    CollisionSystem.NextGameObjectId++,
                    this.collisionSystem,
                    tileInteractionType,
                    texture,
                    position,
                    new Point(tile.X, tile.Y),
                    tileSet.TileWidth,
                    tileSet.TileHeight,
                    new Vector2((x - 1) * (tileSet.TileWidth + tileSet.Spacing), (y - 1) * (tileSet.TileHeight + tileSet.Spacing)));

                myTile.Initialize();
                this.tileCollection.Add(position, myTile);
            }
        }
    }
}