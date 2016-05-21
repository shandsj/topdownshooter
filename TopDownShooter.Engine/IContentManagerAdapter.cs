// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IContentManagerAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework.Content;

    /// <summary>
    /// Defines an interface for adapting <see cref="ContentManager" />.
    /// </summary>
    public interface IContentManagerAdapter
    {
        /// <summary>
        /// Loads an asset from the specified asset name.
        /// </summary>
        /// <typeparam name="T">The type of the asset to load.</typeparam>
        /// <param name="assetName">The asset name.</param>
        /// <returns>The loaded asset.</returns>
        T Load<T>(string assetName);
    }
}