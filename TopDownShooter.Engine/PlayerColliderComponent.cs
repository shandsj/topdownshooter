// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerColliderComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Levels;

    /// <summary>
    /// Defines a collider component for player objects.
    /// </summary>
    public class PlayerColliderComponent : SimpleColliderComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerColliderComponent" /> class.
        /// </summary>
        /// <param name="gameObjectId">The parent game object identifier.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        public PlayerColliderComponent(int gameObjectId, ICollisionSystem collisionSystem)
            : base(gameObjectId, collisionSystem)
        {
        }

        /// <summary>
        /// Performs a collision with the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="other">The other collider component.</param>
        public override void Collide(IColliderComponent other)
        {
            var gameObject = this.CollisionSystem.GetGameObject(this.GameObjectId);
            gameObject.Velocity = new Vector2(0, 0); // TODO: modify velocity correctly based on location of collided object
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="time">The game time.</param>
        public override void Update(IGameObject gameObject, GameTime time)
        {
            // If the entity is moving, check for collision in the collider system.
            if (gameObject.Velocity.Length() != 0)
            {
                this.CollisionSystem.CheckCollisions(this);
            }

            base.Update(gameObject, time);
        }
    }
}