// <copyright file="IInputController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine.Controllers
{
    /// <summary>
    /// Simple Controller for requesting Movements
    /// </summary>
    public interface IInputController
    {
        /// <summary>
        /// Gets a value indicating whether a fire was requested.
        /// </summary>
        /// <returns>True if the action was requested; false otherwise.</returns>
        bool Fire();

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move down.</returns>
        bool MoveDown();

        /// <summary>
        /// Gets whether or not a left move was requested.
        /// </summary>
        /// <returns>True to move left.</returns>
        bool MoveLeft();

        /// <summary>
        /// Gets whether or not a right move was requested.
        /// </summary>
        /// <returns>True to move right.</returns>
        bool MoveRight();

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move up.</returns>
        bool MoveUp();
    }
}