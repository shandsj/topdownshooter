// <copyright file="IGameItemFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using Collisions;
    using Items;
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Factory that can be used to create <see cref="IGameItem"/>
    /// </summary>
    public interface IGameItemFactory
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
        IEnumerable<IGameItem> SpawnRandomBulletItems(int count, ICollisionSystem collisionSystem, int minX, int maxX, int minY, int maxY);

        /// <summary>
        /// Creates a bullet item game object.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <returns>The created bullet item.</returns>
        IGameItem CreateBulletItem(int id, Vector2 position, ICollisionSystem collisionSystem);

        /// <summary>
        /// Spawns a random number of 
        /// </summary>
        /// <param name="count">Number to create</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="minX">Minimum X coordinate</param>
        /// <param name="maxX">Maximum X coordinate</param>
        /// <param name="minY">Minimum Y coordiante</param>
        /// <param name="maxY">Maximum Y coordinate</param>
        /// <returns>Collection of uninitalized <see cref="IEnumerable{IGameItem}"/></returns>
        IEnumerable<IGameItem> SpawnRandomCoinItems(int count, ICollisionSystem collisionSystem, int minX, int maxX, int minY, int maxY);

        /// <summary>
        /// Creates a coin item game object.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <returns>The created bullet item.</returns>
        IGameItem CreateCoinItem(int id, Vector2 position, ICollisionSystem collisionSystem);
    }
}
