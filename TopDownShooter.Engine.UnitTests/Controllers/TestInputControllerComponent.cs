// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestInputControllerComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests.Controllers
{
    using TopDownShooter.Engine.Controllers;

    /// <summary>
    /// Defines a test <see cref="InputControllerComponentBase" /> class.
    /// </summary>
    public class TestInputControllerComponent : InputControllerComponentBase
    {
        public override void Destroy()
        {
        }

        /// <summary>
        /// Gets a value indicating whether a fire was requested.
        /// </summary>
        /// <returns>True if the action was requested; false otherwise.</returns>
        public override bool Fire()
        {
            return true;
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public override void Initialize()
        {
        }

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move down.</returns>
        public override bool MoveDown()
        {
            return false;
        }

        /// <summary>
        /// Gets whether or not a left move was requested.
        /// </summary>
        /// <returns>True to move left.</returns>
        public override bool MoveLeft()
        {
            return false;
        }

        /// <summary>
        /// Gets whether or not a right move was requested.
        /// </summary>
        /// <returns>True to move right.</returns>
        public override bool MoveRight()
        {
            return false;
        }

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move up.</returns>
        public override bool MoveUp()
        {
            return false;
        }
    }
}