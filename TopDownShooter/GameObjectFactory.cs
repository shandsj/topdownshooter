// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameObjectFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Projectiles;

    /// <summary>
    /// Provides an implementation of <see cref="IGameObjectFactory" />.
    /// </summary>
    public class GameObjectFactory : IGameObjectFactory
    {
        /// <summary>
        /// Creates a bullet projectile game object.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="parentId">The parent game object identifier.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="direction">The diretion of the bullet, as a unit vector.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <returns>The created bullet projectile.</returns>
        public IGameObject CreateShortRangeProjectile(int id, int parentId, Vector2 position, Vector2 direction, ICollisionSystem collisionSystem)
        {
            var components = new IComponent[]
            {
                new ProjectileColliderComponent(id, parentId, collisionSystem),
                new AnimationComponent("Bullet", "SpriteSheets/BulletProjectile", new FrameProperties(32, 32, TimeSpan.MaxValue, 1)) { IsRendered = true }
            };

            return this.CreateShortRangeProjectile(id, parentId, position, direction, collisionSystem, components);
        }

        /// <summary>
        /// Creates a bullet projectile game object.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="parentId">The parent game object identifier.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="direction">The diretion of the bullet, as a unit vector.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="components">The collection of components</param>
        /// <returns>The created bullet projectile.</returns>
        public IGameObject CreateShortRangeProjectile(int id, int parentId, Vector2 position, Vector2 direction, ICollisionSystem collisionSystem, IEnumerable<IComponent> components)
        {
            return new ShortRangeProjectile(id, position, direction, collisionSystem, components);
        }

        /// <summary>
        /// Creates a bullet projectile game object.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="parentId">The parent game object identifier.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="direction">The diretion of the bullet, as a unit vector.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <returns>The created bullet projectile.</returns>
        public IGameObject CreateLongRangeProjectile(int id, int parentId, Vector2 position, Vector2 direction, ICollisionSystem collisionSystem)
        {
            var components = new IComponent[]
            {
                new ProjectileColliderComponent(id, parentId, collisionSystem),
                new AnimationComponent("Bullet", "SpriteSheets/BulletProjectile", new FrameProperties(32, 32, TimeSpan.MaxValue, 1)) { IsRendered = true }
            };

            return this.CreateLongRangeProjectile(id, parentId, position, direction, collisionSystem, components);
        }

        /// <summary>
        /// Creates a bullet projectile game object.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="parentId">The parent game object identifier.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="direction">The diretion of the bullet, as a unit vector.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="components">The collection of components</param>
        /// <returns>The created bullet projectile.</returns>
        public IGameObject CreateLongRangeProjectile(int id, int parentId, Vector2 position, Vector2 direction, ICollisionSystem collisionSystem, IEnumerable<IComponent> components)
        {
            return new LongRangeProjectile(id, parentId, position, direction, collisionSystem, components);
        }
    }
}