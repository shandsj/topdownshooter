// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IColliderComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    /// <summary>
    /// Defines an interface for a collider component.
    /// </summary>
    public interface IColliderComponent : IComponent
    {
        /// <summary>
        /// Gets the parent game object identifer.
        /// </summary>
        int GameObjectId { get; }

        /// <summary>
        /// Performs a collision with the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="other">The other collider component.</param>
        void Collide(IColliderComponent other);

        /// <summary>
        /// Determines a collision occured with the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="other">The rigid body to check for a collision.</param>
        /// <returns>TTrue if a collision occured, false otherwise.</returns>
        bool IsCollision(IColliderComponent other);
    }
}