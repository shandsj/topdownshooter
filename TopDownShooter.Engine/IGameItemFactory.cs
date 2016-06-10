// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameItemFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Items;

    /// <summary>
    /// Factory that can be used to create <see cref="IGameItem" />
    /// </summary>
    public interface IGameItemFactory
    {
        /// <summary>
        /// Creates a bullet item game object.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <returns>The created bullet item.</returns>
        IGameItem CreateBulletItem(int id, Vector2 position, ICollisionSystem collisionSystem);

        /// <summary>
        /// Creates a coin item game object.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="isImmuneToPickup">A value indicating whether the created coin item will be immune to pickup for a short time.</param>
        /// <returns>The created bullet item.</returns>
        IGameItem CreateCoinItem(int id, Vector2 position, ICollisionSystem collisionSystem, bool isImmuneToPickup);

        /// <summary>
        /// Spawns a random number of <see cref="CoinGameItem" />s
        /// </summary>
        /// <param name="count">Number to create</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="minX">Minimum X coordinate</param>
        /// <param name="maxX">Maximum X coordinate</param>
        /// <param name="minY">Minimum Y coordiante</param>
        /// <param name="maxY">Maximum Y coordinate</param>
        /// <returns>Collection of uninitalized <see cref="IEnumerable{IGameItem}" /></returns>
        IEnumerable<IGameItem> SpawnRandomBulletItems(int count, ICollisionSystem collisionSystem, int minX, int maxX, int minY, int maxY);

        /// <summary>
        /// Spawns a random number of coin items.
        /// </summary>
        /// <param name="count">Number to create</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="minX">Minimum X coordinate</param>
        /// <param name="maxX">Maximum X coordinate</param>
        /// <param name="minY">Minimum Y coordiante</param>
        /// <param name="maxY">Maximum Y coordinate</param>
        /// <param name="isImmuneToPickup">A value indicating whether the spawned coin items will be immune to pickup for a short time.</param>
        /// <returns>Collection of uninitalized <see cref="IEnumerable{IGameItem}" /></returns>
        IEnumerable<IGameItem> SpawnRandomCoinItems(int count, ICollisionSystem collisionSystem, int minX, int maxX, int minY, int maxY, bool isImmuneToPickup);
    }
}