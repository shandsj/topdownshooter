// <copyright file="GameItemFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Collisions;
    using Items;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Factor that can be used to generate <see cref="IGameItem"/>
    /// </summary>
    public class GameItemFactory : IGameItemFactory
    {
        /// <summary>
        /// Spawns a random number of <see cref="CoinGameItem"/>s
        /// </summary>
        /// <param name="count">Number to create</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="minX">Minimum X coordinate</param>
        /// <param name="maxX">Maximum X coordinate</param>
        /// <param name="minY">Minimum Y coordiante</param>
        /// <param name="maxY">Maximum Y coordinate</param>
        /// <returns>Collection of uninitalized <see cref="IEnumerable{IGameItem}"/></returns>
        public IEnumerable<IGameItem> SpawnRandomBulletItems(int count, ICollisionSystem collisionSystem, int minX, int maxX, int minY, int maxY)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            List<IGameItem> result = new List<IGameItem>();

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
        public IGameItem CreateBulletItem(int id, Vector2 position, ICollisionSystem collisionSystem)
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
        /// Spawns a random number of <see cref="CoinGameItem"/>s
        /// </summary>
        /// <param name="count">Number to create</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="minX">Minimum X coordinate</param>
        /// <param name="maxX">Maximum X coordinate</param>
        /// <param name="minY">Minimum Y coordiante</param>
        /// <param name="maxY">Maximum Y coordinate</param>
        /// <returns>Collection of uninitalized <see cref="IEnumerable{IGameItem}"/></returns>
        public IEnumerable<IGameItem> SpawnRandomCoinItems(int count, ICollisionSystem collisionSystem, int minX, int maxX, int minY, int maxY)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            List<IGameItem> result = new List<IGameItem>();

            for (int i = 0; i < count; i++)
            {
                int x = random.Next(minX, maxX);
                int y = random.Next(minY, maxY);

                result.Add(this.CreateCoinItem(CollisionSystem.NextGameObjectId++, new Vector2(x, y), collisionSystem));
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
        public IGameItem CreateCoinItem(int id, Vector2 position, ICollisionSystem collisionSystem)
        {
            var components = new IComponent[]
            {
                new SimpleColliderComponent(id, collisionSystem),
                new AnimationComponentManager(
                    new AnimationComponent("Coin", "SpriteSheets/Coin24", new FrameProperties(24, 24, TimeSpan.FromSeconds(0.001), 61)) { IsRendered = true, IsAnimating = true, IsLooping = true })
            };

            return new CoinGameItem(id, position, collisionSystem, components);
        }
    }
}
