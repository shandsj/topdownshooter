// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColliderComponentBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Collisions
{
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Messages;

    /// <summary>
    /// Defines a rigid body component that will respond to collisions with other rigid bodies.
    /// </summary>
    public abstract class ColliderComponentBase : IColliderComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColliderComponentBase" /> class.
        /// </summary>
        /// <param name="gameObjectId">The parent game object identifier.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem"/>.</param>
        protected ColliderComponentBase(int gameObjectId, ICollisionSystem collisionSystem)
        {
            this.GameObjectId = gameObjectId;
            this.CollisionSystem = collisionSystem;
        }

        /// <summary>
        /// Gets the parent game object identifer.
        /// </summary>
        public int GameObjectId { get; }

        /// <summary>
        /// Gets the <see cref="ICollisionSystem" />.
        /// </summary>
        protected ICollisionSystem CollisionSystem { get; }

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="camera">The <see cref="ICamera2DAdapter"/>.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        public virtual void Draw(IGameObject gameObject, ICamera2DAdapter camera, ISpriteBatchAdapter spriteBatch, GameTime time)
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
        /// Initializes the component.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Destroys the component.
        /// </summary>
        public virtual void Destroy()
        {
        }

        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="message">The message object.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void ReceiveMessage(IGameObject gameObject, Message message, GameTime gameTime)
        {
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(IGameObject gameObject, GameTime gameTime)
        {
            // If the entity is moving, check for collision in the collider system.
            if (gameObject.Velocity.Length() != 0)
            {
                this.CollisionSystem.CheckCollisions(this, gameTime);
            }
        }

        /// <summary>
        /// Performs a collision with the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="other">The other collider component.</param>
        /// <param name="gameTime">The game time.</param>
        public abstract void Collide(IColliderComponent other, GameTime gameTime);

        /// <summary>
        /// Determines a collision occured with the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="other">The rigid body to check for a collision.</param>
        /// <returns>TTrue if a collision occured, false otherwise.</returns>
        public abstract bool IsCollision(IColliderComponent other);
    }
}