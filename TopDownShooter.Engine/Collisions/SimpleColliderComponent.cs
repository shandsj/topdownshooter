// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleColliderComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Collisions
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
        /// Performs a collision with the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="other">The other collider component.</param>
        public override void Collide(IColliderComponent other)
        {
        }

        /// <summary>
        /// Determines a collision occured with the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="other">The rigid body to check for a collision.</param>
        /// <returns>TTrue if a collision occured, false otherwise.</returns>
        public override bool IsCollision(IColliderComponent other)
        {
            var otherGameObject = this.CollisionSystem.GetGameObject(other.GameObjectId);
            var gameObject = this.CollisionSystem.GetGameObject(this.GameObjectId);

            if (gameObject != null && otherGameObject != null && gameObject != other)
            {
                return otherGameObject.ProjectedBounds.Intersects(gameObject.ProjectedBounds);
            }

            return false;
        }
    }
}