// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IColliderComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Collisions
{
    using Microsoft.Xna.Framework;

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
        /// <param name="gameTime">The game time.</param>
        void Collide(IColliderComponent other, GameTime gameTime);

        /// <summary>
        /// Determines a collision occured with the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="other">The rigid body to check for a collision.</param>
        /// <returns>TTrue if a collision occured, false otherwise.</returns>
        bool IsCollision(IColliderComponent other);
    }
}