// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputControllerComponentBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Controllers
{
    using System;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Base implementation for an <see cref="IInputController" />
    /// </summary>
    public abstract class InputControllerComponentBase : IInputController, IComponent, IDisposable
    {
        private readonly float speed = 8f;

        // To detect redundant calls
        private bool disposedValue;

        /// <summary>
        /// Finalizes an instance of the <see cref="InputControllerComponentBase" /> class.
        /// </summary>
        ~InputControllerComponentBase()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the direction vector.
        /// </summary>
        public abstract Vector2 Direction { get; }

        /// <summary>
        /// Destroys the component.
        /// </summary>
        public abstract void Destroy();

        /// <summary>
        /// Cleans up any resources used during the execution of this class
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="camera">The <see cref="ICamera"/>.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        public void Draw(IGameObject gameObject, ICamera camera, ISpriteBatchAdapter spriteBatch, GameTime time)
        {
        }

        /// <summary>
        /// Gets a value indicating whether a fire was requested.
        /// </summary>
        /// <returns>True if the action was requested; false otherwise.</returns>
        public abstract bool Fire();

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public virtual void LoadContent(IContentManagerAdapter contentManager)
        {
        }

        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="message">The message object.</param>
        public void ReceiveMessage(IGameObject gameObject, ComponentMessage message)
        {
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="time">The game time.</param>
        public virtual void Update(IGameObject gameObject, GameTime time)
        {
            if (this.Fire())
            {
                gameObject.BroadcastMessage(new ComponentMessage(MessageType.Fire));
            }

            this.Direction.Normalize();
            gameObject.Velocity = this.Direction * this.speed;
        }

        /// <summary>
        /// Unloads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public virtual void UnloadContent(IContentManagerAdapter contentManager)
        {
        }

        /// <summary>
        /// Cleans up any resources used during the execution of this class
        /// </summary>
        /// <param name="disposing">Disposing or not?</param>
        protected void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.ManagedDispose();
                }

                this.disposedValue = true;
            }
        }

        /// <summary>
        /// Cleans up any managed resources during disposal
        /// </summary>
        protected virtual void ManagedDispose()
        {
        }
    }
}