// <copyright file="InputControllerComponentBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Base implementation for an <see cref="IInputController"/>
    /// </summary>
    public abstract class InputControllerComponentBase : IInputController, IComponent, IDisposable
    {
        // To detect redundant calls
        private bool disposedValue = false;
        private float speed = 8f;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputControllerComponentBase"/> class.
        /// </summary>
        protected InputControllerComponentBase()
        {
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="InputControllerComponentBase"/> class.
        /// </summary>
        ~InputControllerComponentBase()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="message">The message object.</param>
        public void ReceiveMessage(IGameObject gameObject, object message)
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
                gameObject.BroadcastMessage(MessageType.Fire);
            }

            float x = 0;
            float y = 0;

            if (this.MoveLeft())
            {
                x = -this.speed;
            }

            if (this.MoveRight())
            {
                x = this.speed;
            }

            if (this.MoveUp())
            {
                y = -this.speed;
            }

            if (this.MoveDown())
            {
                y = this.speed;
            }

            gameObject.Velocity = new Vector2(x, y);
        }

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        public void Draw(IGameObject gameObject, ISpriteBatchAdapter spriteBatch, GameTime time)
        {
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public virtual void LoadContent(IContentManagerAdapter contentManager)
        {
        }

        /// <summary>
        /// Unloads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public virtual void UnloadContent(IContentManagerAdapter contentManager)
        {
        }

        /// <summary>
        /// Gets a value indicating whether a fire was requested.
        /// </summary>
        /// <returns>True if the action was requested; false otherwise.</returns>
        public abstract bool Fire();

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move down.</returns>
        public abstract bool MoveDown();

        /// <summary>
        /// Gets whether or not a left move was requested.
        /// </summary>
        /// <returns>True to move left.</returns>
        public abstract bool MoveLeft();

        /// <summary>
        /// Gets whether or not a right move was requested.
        /// </summary>
        /// <returns>True to move right.</returns>
        public abstract bool MoveRight();

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move up.</returns>
        public abstract bool MoveUp();

        /// <summary>
        /// Cleans up any resources used during the execution of this class
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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
