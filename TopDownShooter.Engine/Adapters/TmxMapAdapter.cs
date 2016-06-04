// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TmxMapAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Adapters
{
    using TiledSharp;

    /// <summary>
    /// Provides a default implementation of the <see cref="ITmxMapAdapter" /> interface.
    /// </summary>
    public class TmxMapAdapter : ITmxMapAdapter
    {
        private readonly TmxMap map;

        /// <summary>
        /// Initializes a new instance of the <see cref="TmxMapAdapter" /> class.
        /// </summary>
        /// <param name="map">The <see cref="TmxMap" /> to adapt.</param>
        public TmxMapAdapter(TmxMap map)
        {
            this.map = map;
        }

        /// <summary>
        /// Gets the collection of layers.
        /// </summary>
        public TmxList<TmxLayer> Layers => this.map.Layers;

        /// <summary>
        /// Gets the height of the tiles.
        /// </summary>
        public int TileHeight => this.map.TileHeight;

        /// <summary>
        /// Gets the collection of tilesets.
        /// </summary>
        public TmxList<TmxTileset> Tilesets => this.map.Tilesets;

        /// <summary>
        /// Gets the width of the tiles.
        /// </summary>
        public int TileWidth => this.map.TileWidth;
    }
}