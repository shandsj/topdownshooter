// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Simple Controller for requesting Movements
    /// </summary>
    public interface IInputController
    {
        /// <summary>
        /// Gets the direction vector.
        /// </summary>
        Vector2 MovementDirection { get; }

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        float Rotation { get; }

        /// <summary>
        /// Gets a value indicating whether a fire was requested.
        /// </summary>
        /// <returns>True if the action was requested; false otherwise.</returns>
        bool Fire();

        /// <summary>
        /// Gets a value indicating whether a dash was requested.
        /// </summary>
        /// <returns>True if the action was requested; false otherwise.</returns>
        bool Dash();
    }
}