// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Defines an interface for components.
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="message">The message object.</param>
        /// <param name="gameTime">The game time.</param>
        void ReceiveMessage(IGameObject gameObject, Message message, GameTime gameTime);

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="gameTime">The game time.</param>
        void Update(IGameObject gameObject, GameTime gameTime);

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="camera">The <see cref="ICamera2DAdapter"/>.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        void Draw(IGameObject gameObject, ICamera2DAdapter camera, ISpriteBatchAdapter spriteBatch, GameTime time);

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        void LoadContent(IContentManagerAdapter contentManager);

        /// <summary>
        /// Initializes the component.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Destroys the component.
        /// </summary>
        void Destroy();
    }
}