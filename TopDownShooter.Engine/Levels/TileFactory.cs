// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TileFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Levels
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Provides a default implementation of the <see cref="ITileFactory" /> interface.
    /// </summary>
    public class TileFactory : ITileFactory
    {
        /// <summary>
        /// Creates a tile.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="tileInteractionType">The <see cref="TileInteractionType" />.</param>
        /// <param name="texture">The <see cref="Texture2D" />.</param>
        /// <param name="position">The position of the tile.</param>
        /// <param name="tileCoordinates">The tile's position in tile coordinates.</param>
        /// <param name="width">The width of the tile.</param>
        /// <param name="height">The height of the tile.</param>
        /// <param name="texturePosition">The texture's position in the tile set.</param>
        /// <returns>The created tile.</returns>
        public ITile CreateTile(int id, ICollisionSystem collisionSystem, TileInteractionType tileInteractionType, Texture2D texture, Vector2 position, Point tileCoordinates, int width, int height, Vector2 texturePosition)
        {
            return new Tile(id, collisionSystem, tileInteractionType, texture, position, tileCoordinates, width, height, texturePosition);
        }
    }
}