// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectileColliderComponent.cs" company="PlaceholderCompany">
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
    public class ProjectileColliderComponent : SimpleColliderComponent
    {
        private readonly IPlayer projectileParent;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileColliderComponent" /> class.
        /// </summary>
        /// <param name="gameObjectId">The parent game object identifier.</param>
        /// <param name="projectileParentId">The projectile's parent game object identifier.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        public ProjectileColliderComponent(int gameObjectId, int projectileParentId, ICollisionSystem collisionSystem)
            : base(gameObjectId, collisionSystem)
        {
            this.projectileParent = this.CollisionSystem.GetGameObject(projectileParentId) as IPlayer;
        }

        /// <summary>
        /// Performs a collision with the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="other">The other collider component.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Collide(IColliderComponent other, GameTime gameTime)
        {
            bool destroy = false;
            var otherGameObject = this.CollisionSystem.GetGameObject(other.GameObjectId);

            var player = otherGameObject as IPlayer;
            if (player != null && player != this.projectileParent && player.Health > 0)
            {
                this.projectileParent.KillCount++;
                player.Health--;
                destroy = true;
            }

            var tile = otherGameObject as ITile;
            if (tile != null)
            {
                destroy = true;
            }

            if (destroy)
            {
                var gameObject = this.CollisionSystem.GetGameObject(this.GameObjectId);
                gameObject?.Destroy();
            }

            base.Collide(other, gameTime);
        }
    }
}