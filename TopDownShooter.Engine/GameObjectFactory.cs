// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameObjectFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using Collisions;
    using Items;
    using Microsoft.Xna.Framework;
    using Projectiles;

    /// <summary>
    /// Provides an implementation of <see cref="IGameObjectFactory" />.
    /// </summary>
    public class GameObjectFactory : IGameObjectFactory
    {
        /// <summary>
        /// Spawns a random number of <see cref="BulletGameItem"/>s
        /// </summary>
        /// <param name="count">Number to create</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="minX">Minimum X coordinate</param>
        /// <param name="maxX">Maximum X coordinate</param>
        /// <param name="minY">Minimum Y coordiante</param>
        /// <param name="maxY">Maximum Y coordinate</param>
        /// <returns>Collection of uninitalized <see cref="IEnumerable{IGameObject}"/></returns>
        public IEnumerable<IGameObject> SpawnRandomBulletItems(int count, ICollisionSystem collisionSystem, int minX, int maxX, int minY, int maxY)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            List<IGameObject> result = new List<IGameObject>();

            for (int i = 0; i < count; i++)
            {
                int x = random.Next(minX, maxX);
                int y = random.Next(minY, maxY);

                result.Add(this.CreateBulletItem(CollisionSystem.NextGameObjectId++, new Vector2(x, y), collisionSystem));
            }

            return result;
        }

        /// <summary>
        /// Creates a bullet item game object.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <returns>The created bullet item.</returns>
        public IGameObject CreateBulletItem(int id, Vector2 position, ICollisionSystem collisionSystem)
        {
            var components = new IComponent[]
            {
                new SimpleColliderComponent(id, collisionSystem),
                new AnimationComponentManager(
                    new AnimationComponent("Bullet", "SpriteSheets/Bullet Item", new FrameProperties(100, 100, TimeSpan.MaxValue, 1)) { IsRendered = true })
            };

            return new BulletGameItem(id, position, collisionSystem, components);
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
        public IGameObject CreateBulletProjectile(int id, int parentId, Vector2 position, Vector2 direction, ICollisionSystem collisionSystem)
        {
            var components = new IComponent[]
            {
                new BulletProjectileColliderComponent(id, parentId, collisionSystem),
                new AnimationComponent("Bullet", "SpriteSheets/BulletProjectile", new FrameProperties(32, 32, TimeSpan.MaxValue, 1)) { IsRendered = true }
            };

            return this.CreateBulletProjectile(id, parentId, position, direction, collisionSystem, components);
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
        public IGameObject CreateBulletProjectile(int id, int parentId, Vector2 position, Vector2 direction, ICollisionSystem collisionSystem, IEnumerable<IComponent> components)
        {
            return new BulletProjectile(id, parentId, position, direction, collisionSystem, components);
        }
    }
}