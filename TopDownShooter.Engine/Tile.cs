// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines a tile.
    /// </summary>
    public class Tile : ITile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tile" /> class.
        /// </summary>
        /// <param name="tileInteractionType">The <see cref="TileInteractionType" />.</param>
        /// <param name="texture">The <see cref="Texture2D" />.</param>
        /// <param name="position">The position of the tile.</param>
        /// <param name="tileCoordinates">The tile's position in tile coordinates.</param>
        /// <param name="width">The width of the tile.</param>
        /// <param name="height">The height of the tile.</param>
        /// <param name="texturePosition">The texture's position in the tile set.</param>
        public Tile(TileInteractionType tileInteractionType, Texture2D texture, Vector2 position, Point tileCoordinates, int width, int height, Vector2 texturePosition)
        {
            this.Texture = texture;
            this.Position = position;
            this.TileCoordinates = tileCoordinates;
            this.Width = width;
            this.Height = height;
            this.TexturePosition = texturePosition;
            this.TileInteractionType = tileInteractionType;
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the texture
        /// </summary>
        public Texture2D Texture { get; }

        /// <summary>
        /// Gets the position of the tile in the tileset.
        /// </summary>
        public Vector2 TexturePosition { get; }

        /// <summary>
        /// Gets or sets the tile's position in tile coordinates.
        /// </summary>
        public Point TileCoordinates { get; set; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the bounding box.
        /// </summary>
        public Rectangle BoundingBox => new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Width, this.Height);

        /// <summary>
        /// Gets the position of the tile.
        /// </summary>
        public Vector2 Position { get; }

        /// <summary>
        /// Gets the <see cref="TileInteractionType" />.
        /// </summary>
        public TileInteractionType TileInteractionType { get; }

        /// <summary>
        /// Draws this <see cref="Tile" />.
        /// </summary>
        /// <param name="spriteBatch">
        /// The <see cref="ISpriteBatchAdapter" />.
        /// </param>
        /// <param name="gameTime">
        /// The game time.
        /// </param>
        public void Draw(ISpriteBatchAdapter spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(
                this.Texture,
                this.Position,
                new Rectangle((int)this.TexturePosition.X, (int)this.TexturePosition.Y, this.Width, this.Height),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0.0f);

            ////spriteBatch.DrawString(this.tileCoordinateTexture, string.Format("{0},{1}", this.TileCoordinates.X, this.TileCoordinates.Y), this.Position, Color.Green);
        }
    }
}