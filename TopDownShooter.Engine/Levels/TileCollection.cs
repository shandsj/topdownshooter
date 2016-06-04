// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TileCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Levels
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Provides a default implementation of the <see cref="ITileCollection" /> interface.
    /// </summary>
    public class TileCollection : ITileCollection
    {
        /// <summary>
        /// The collection of tiles, mapped from world coordinates.
        /// </summary>
        private readonly Dictionary<Vector2, ITile> tiles = new Dictionary<Vector2, ITile>();

        /// <summary>
        /// Adds a tile to the collection at the specified world coordinate.
        /// </summary>
        /// <param name="coordinate">The location of the tile, in world coordinates.</param>
        /// <param name="tile">The <see cref="ITile" />.</param>
        public void Add(Vector2 coordinate, ITile tile)
        {
            this.tiles.Add(coordinate, tile);
        }

        /// <summary>
        /// Gets the tile at the specified world coordinates.
        /// </summary>
        /// <param name="coordinate">The location of the tile, in world coordinates.</param>
        /// <returns>The <see cref="ITile" />.</returns>
        public ITile GetTile(Vector2 coordinate)
        {
            ITile tile;
            this.tiles.TryGetValue(coordinate, out tile);
            return tile;
        }
    }
}