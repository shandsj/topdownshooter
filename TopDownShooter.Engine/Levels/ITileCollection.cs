// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITileCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Levels
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines an interface for storing and indexing tiles.
    /// </summary>
    public interface ITileCollection
    {
        /// <summary>
        /// Adds a tile to the collection at the specified world coordinate.
        /// </summary>
        /// <param name="coordinate">The location of the tile, in world coordinates.</param>
        /// <param name="tile">The <see cref="ITile" />.</param>
        void Add(Vector2 coordinate, ITile tile);

        /// <summary>
        /// Gets the tile at the specified world coordinates.
        /// </summary>
        /// <param name="coordinate">The location of the tile, in world coordinates.</param>
        /// <returns>The <see cref="ITile" />.</returns>
        ITile GetTile(Vector2 coordinate);
    }
}