// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollisionSystem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines an interface for providing game object lookups.
    /// </summary>
    public interface ICollisionSystem
    {
        /// <summary>
        /// Gets the collection of <see cref="IColliderComponent" /> objects registered with this <see cref="ICollisionSystem" />.
        /// </summary>
        IEnumerable<IColliderComponent> Colliders { get; }

        /// <summary>
        /// Gets the collection of <see cref="IGameObject" /> objects registered with this <see cref="ICollisionSystem" />.
        /// </summary>
        IEnumerable<IGameObject> GameObjects { get; }

        /// <summary>
        /// Checks the collisions for the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="collider">The rigid body.</param>
        void CheckCollisions(IColliderComponent collider);

        /// <summary>
        /// Gets the <see cref="IGameObject" /> with the specified identifer.
        /// </summary>
        /// <param name="id">Game object identifier.</param>
        /// <returns>The <see cref="IGameObject" />.</returns>
        IGameObject GetGameObject(int id);

        /// <summary>
        /// Registers the specifieid <see cref="IGameObject" /> and <see cref="IColliderComponent" /> with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the game object.</param>
        /// <param name="gameObject">The game object.</param>
        /// <param name="collider">The collider component.</param>
        void Register(int id, IGameObject gameObject, IColliderComponent collider);

        /// <summary>
        /// Unregisters the game object and collider with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the game object.</param>
        void Unregister(int id);
    }
}