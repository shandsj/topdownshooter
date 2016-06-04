// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITmxMapAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Adapters
{
    using TiledSharp;

    /// <summary>
    /// Defines an adapter interface for a <see cref="TmxMap" />.
    /// </summary>
    public interface ITmxMapAdapter
    {
        /// <summary>
        /// Gets the collection of layers.
        /// </summary>
        TmxList<TmxLayer> Layers { get; }

        /// <summary>
        /// Gets the height of the tiles.
        /// </summary>
        int TileHeight { get; }

        /// <summary>
        /// Gets the collection of tilesets.
        /// </summary>
        TmxList<TmxTileset> Tilesets { get; }

        /// <summary>
        /// Gets the width of the tiles.
        /// </summary>
        int TileWidth { get; }
    }
}