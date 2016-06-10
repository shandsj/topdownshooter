// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContentManagerAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TopDownShooter.Engine.Adapters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    /// <summary>
    /// Defines an adapter for the <see cref="ContentManager" /> class.
    /// </summary>
    public class ContentManagerAdapter : IContentManagerAdapter
    {
        /// <summary>
        /// The <see cref="ContentManager" />.
        /// </summary>
        private readonly ContentManager content;

        /// <summary>
        /// Load lock access, if two seperate threads attempt to load an
        /// asset at the same time, they can really stomp on each other
        /// producing tearing artifacts. This is to keep that from happening.
        /// </summary>
        private readonly object synchronizationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentManagerAdapter" /> class.
        /// </summary>
        /// <param name="content">
        /// The <see cref="ContentManager" />.
        /// </param>
        public ContentManagerAdapter(ContentManager content)
        {
            this.synchronizationContext = new object();
            this.content = content;
        }

        /// <summary>
        /// Loads an asset from the specified asset name.
        /// </summary>
        /// <typeparam name="T">The type of the asset to load.</typeparam>
        /// <param name="assetName">The asset name.</param>
        /// <returns>The loaded asset.</returns>
        public T Load<T>(string assetName)
        {
            lock (this.synchronizationContext)
            {
                return this.content.Load<T>(assetName);
            }
        }

        /// <summary>
        /// Unloads all loaded content from the content manager.
        /// </summary>
        public void Unload()
        {
            this.content.Unload();
        }
    }
}