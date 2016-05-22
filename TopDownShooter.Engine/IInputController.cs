// <copyright file="IInputController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Simple Controller for requesting Movements
    /// </summary>
    public interface IInputController
    {
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

        /// <summary>
        /// Updates based on the current gametime.
        /// </summary>
        /// <param name="gameTime">The current <see cref="GameTime"/></param>
        /// <remarks>Don't even know if GameTime is needed...</remarks>
        void Update(GameTime gameTime);
    }
}