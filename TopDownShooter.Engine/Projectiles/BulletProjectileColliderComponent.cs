// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BulletProjectileColliderComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Projectiles
{
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Levels;

    /// <summary>
    /// Defines a collider component for the bullet projectile.
    /// </summary>
    public class BulletProjectileColliderComponent : SimpleColliderComponent
    {
        private IGameObject bulletParent;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletProjectileColliderComponent" /> class.
        /// </summary>
        /// <param name="gameObjectId">The parent game object identifier.</param>
        /// <param name="bulletParentId">The bullet's parent game object identifier.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        public BulletProjectileColliderComponent(int gameObjectId, int bulletParentId, ICollisionSystem collisionSystem)
            : base(gameObjectId, collisionSystem)
        {
            this.bulletParent = this.CollisionSystem.GetGameObject(bulletParentId);
        }

        /// <summary>
        /// Performs a collision with the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="other">The other collider component.</param>
        public override void Collide(IColliderComponent other)
        {
            var otherGameObject = this.CollisionSystem.GetGameObject(other.GameObjectId);
            var player = otherGameObject as Player;
            if (player != null && player != this.bulletParent && player.Health > 0)
            {
                player.Health--;
                this.CollisionSystem.Unregister(this.GameObjectId);
            }

            var tile = otherGameObject as ITile;
            if (tile != null)
            {
                this.CollisionSystem.Unregister(this.GameObjectId);
            }

            base.Collide(other);
        }
    }
}