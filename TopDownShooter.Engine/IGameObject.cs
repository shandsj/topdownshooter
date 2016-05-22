// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines an interface for a game object.
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// Gets or sets the position of the game object.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the velocity of the game object.
        /// </summary>
        Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets the collection of components.
        /// </summary>
        IList<IComponent> Components { get; }

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draws the game object with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="gameTime">The game time.</param>
        void Draw(ISpriteBatchAdapter spriteBatch, GameTime gameTime);

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