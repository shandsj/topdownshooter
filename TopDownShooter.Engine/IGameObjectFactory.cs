// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameObjectFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Defines an interface for a game object factory.
    /// </summary>
    public interface IGameObjectFactory
    {
        /// <summary>
        /// Creates a bullet projectile game object.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="direction">The diretion of the bullet, as a unit vector.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <returns>The created bullet projectile.</returns>
        IGameObject CreateBulletProjectile(int id, Vector2 position, Vector2 direction, ICollisionSystem collisionSystem);

        /// <summary>
        /// Creates a bullet projectile game object.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="direction">The diretion of the bullet, as a unit vector.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="components">The collection of components</param>
        /// <returns>The created bullet projectile.</returns>
        IGameObject CreateBulletProjectile(int id, Vector2 position, Vector2 direction, ICollisionSystem collisionSystem, IEnumerable<IComponent> components);
    }
}