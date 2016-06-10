// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Defines an interface for a game object.
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// Raised when a message is ready.
        /// </summary>
        event EventHandler<MessageEventArgs> MessageReady;

        /// <summary>
        /// Gets the bounds of the game object.
        /// </summary>
        Rectangle Bounds { get; }

        /// <summary>
        /// Gets the collection of components.
        /// </summary>
        IList<IComponent> Components { get; }

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the identifier for this game object.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets or sets the position of the game object.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets the projected bounds, based off the <see cref="Position" /> and the <see cref="Velocity" />.
        /// </summary>
        Rectangle ProjectedBounds { get; }

        /// <summary>
        /// Gets the projected position, based off the <see cref="Position" /> and the <see cref="Velocity" />.
        /// </summary>
        Vector2 ProjectedPosition { get; }

        /// <summary>
        /// Gets or sets the velocity of the game object.
        /// </summary>
        Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets the width of the game object.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Broadcasts a message to all components.
        /// </summary>
        /// <param name="message">The message to broadcast.</param>
        /// <param name="gameTime">The game time.</param>
        void BroadcastMessage(Message message, GameTime gameTime);

        /// <summary>
        /// Destroyes the game object.
        /// </summary>
        void Destroy();

        /// <summary>
        /// Draws the game object with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="camera">The <see cref="ICamera2DAdapter" />.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="gameTime">The game time.</param>
        void Draw(ICamera2DAdapter camera, ISpriteBatchAdapter spriteBatch, GameTime gameTime);

        /// <summary>
        /// Initializes the game object.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        void LoadContent(IContentManagerAdapter contentManager);

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        void Update(GameTime gameTime);
    }
}