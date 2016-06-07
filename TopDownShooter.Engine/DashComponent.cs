// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DashComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Defines a dash component.
    /// </summary>
    public class DashComponent : IComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashComponent"/> class.
        /// </summary>
        public DashComponent()
        {
            this.StartedTime = TimeSpan.Zero;
            this.CompletedTime = TimeSpan.Zero;
        }

        /// <summary>
        /// Gets the amount of time the dash is active.
        /// </summary>
        public TimeSpan ActiveTime => TimeSpan.FromMilliseconds(500);

        /// <summary>
        /// Gets the time the last dash was completed.
        /// </summary>
        public TimeSpan CompletedTime { get; private set; }

        /// <summary>
        /// Gets the cooldown time.
        /// </summary>
        public TimeSpan CooldownTime => TimeSpan.FromSeconds(1);

        /// <summary>
        /// Gets the value affecting the velocity vector during the dash.
        /// </summary>
        public float SpeedFactor => 5f;

        /// <summary>
        /// Gets the time the last dash started.
        /// </summary>
        public TimeSpan StartedTime { get; private set; }

        /// <summary>
        /// Destroys the component.
        /// </summary>
        public void Destroy()
        {
        }

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="camera">The <see cref="ICamera2DAdapter" />.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        public void Draw(IGameObject gameObject, ICamera2DAdapter camera, ISpriteBatchAdapter spriteBatch, GameTime time)
        {
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public void LoadContent(IContentManagerAdapter contentManager)
        {
        }

        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="message">The message object.</param>
        /// <param name="gameTime">The game time.</param>
        public void ReceiveMessage(IGameObject gameObject, ComponentMessage message, GameTime gameTime)
        {
            if (message.MessageType == MessageType.Dash)
            {
                this.TryStartDash(gameTime);
            }
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="gameTime">The game time.</param>
        public void Update(IGameObject gameObject, GameTime gameTime)
        {
            if (gameTime.TotalGameTime > this.StartedTime &&
                gameTime.TotalGameTime < this.CompletedTime)
            {
                gameObject.Velocity *= this.SpeedFactor;
            }
        }

        /// <summary>
        /// Attempts to start a dash at the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <returns>True if the dash successfully started; false otherwise.</returns>
        public bool TryStartDash(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - this.CompletedTime > this.CooldownTime)
            {
                this.StartedTime = gameTime.TotalGameTime;
                this.CompletedTime = gameTime.TotalGameTime + this.ActiveTime;
                return true;
            }

            return false;
        }
    }
}