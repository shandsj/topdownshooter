// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestColliderComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests.Collisions
{
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Defines a test collider component.
    /// </summary>
    public class TestColliderComponent : ColliderComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestColliderComponent" /> class.
        /// </summary>
        /// <param name="gameObjectId">The game object identifier.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        public TestColliderComponent(int gameObjectId, ICollisionSystem collisionSystem)
            : base(gameObjectId, collisionSystem)
        {
        }

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
            return false;
        }
    }
}