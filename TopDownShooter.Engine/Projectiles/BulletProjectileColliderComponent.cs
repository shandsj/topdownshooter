// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BulletProjectileColliderComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Projectiles
{
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Defines a collider component for the bullet projectile.
    /// </summary>
    public class BulletProjectileColliderComponent : SimpleColliderComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BulletProjectileColliderComponent" /> class.
        /// </summary>
        /// <param name="gameObjectId">The parent game object identifier.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        public BulletProjectileColliderComponent(int gameObjectId, ICollisionSystem collisionSystem)
            : base(gameObjectId, collisionSystem)
        {
        }

        /// <summary>
        /// Performs a collision with the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="other">The other collider component.</param>
        public override void Collide(IColliderComponent other)
        {
            var player = this.CollisionSystem.GetGameObject(other.GameObjectId) as Player;
            if (player != null && player.Health > 0)
            {
                player.Health--;
            }

            base.Collide(other);
        }
    }
}