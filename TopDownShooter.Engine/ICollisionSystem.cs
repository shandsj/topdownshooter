// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollisionSystem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    /// <summary>
    /// Defines an interface for providing game object lookups.
    /// </summary>
    public interface ICollisionSystem
    {
        /// <summary>
        /// Gets the <see cref="IGameObject" /> with the specified identifer.
        /// </summary>
        /// <param name="id">Game object identifier.</param>
        /// <returns>The <see cref="IGameObject" />.</returns>
        IGameObject GetGameObject(int id);

        /// <summary>
        /// Checks the collisions for the specified <see cref="ColliderComponentBase" />.
        /// </summary>
        /// <param name="collider">The rigid body.</param>
        void CheckCollisions(ColliderComponentBase collider);

        /// <summary>
        /// Registers the specifieid <see cref="IGameObject" /> and <see cref="ColliderComponentBase" /> with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the game object.</param>
        /// <param name="gameObject">The game object.</param>
        /// <param name="collider">The collider component.</param>
        void Register(int id, IGameObject gameObject, ColliderComponentBase collider);

        /// <summary>
        /// Unregisters the game object and collider with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the game object.</param>
        void Unregister(int id);
    }
}