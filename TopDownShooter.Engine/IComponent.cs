﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines an interface for components.
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="time">The game time.</param>
        void Update(IGameObject gameObject, GameTime time);

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        void Draw(IGameObject gameObject, ISpriteBatchAdapter spriteBatch, GameTime time);

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        void LoadContent(IContentManagerAdapter contentManager);

        /// <summary>
        /// Unloads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        void UnloadContent(IContentManagerAdapter contentManager);
    }
}