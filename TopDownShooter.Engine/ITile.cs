// <copyright file="ITile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines an inteface for a tile.
    /// </summary>
    public interface ITile
    {
        /// <summary>
        /// Gets the tile's position in tile coordinates.
        /// </summary>
        Point TileCoordinates { get; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the texture
        /// </summary>
        Texture2D Texture { get; }

        /// <summary>
        /// Gets the position of the tile in the tileset.
        /// </summary>
        Vector2 TexturePosition { get; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        int Width { get; }
    }
}