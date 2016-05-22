// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleColliderComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    /// <summary>
    /// Provides a simple collider component that does bounds checking for collision detection.
    /// </summary>
    public class SimpleColliderComponent : ColliderComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleColliderComponent" /> class.
        /// </summary>
        /// <param name="gameObjectId">The parent game object identifier.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        public SimpleColliderComponent(int gameObjectId, ICollisionSystem collisionSystem)
            : base(gameObjectId)
        {
            this.CollisionSystem = collisionSystem;
        }

        /// <summary>
        /// Gets the <see cref="ICollisionSystem" />.
        /// </summary>
        protected ICollisionSystem CollisionSystem { get; }

        /// <summary>
        /// Performs a collision with the specified <see cref="ColliderComponentBase" />.
        /// </summary>
        /// <param name="other">The other collider component.</param>
        public override void Collide(ColliderComponentBase other)
        {
        }

        /// <summary>
        /// Determines a collision occured with the specified <see cref="ColliderComponentBase" />.
        /// </summary>
        /// <param name="other">The rigid body to check for a collision.</param>
        /// <returns>TTrue if a collision occured, false otherwise.</returns>
        public override bool IsCollision(ColliderComponentBase other)
        {
            var otherGameObject = this.CollisionSystem.GetGameObject(other.GameObjectId);
            var gameObject = this.CollisionSystem.GetGameObject(this.GameObjectId);

            if (gameObject != null && otherGameObject != null && gameObject != other)
            {
                return otherGameObject.Bounds.Intersects(gameObject.Bounds);
            }

            return false;
        }
    }
}