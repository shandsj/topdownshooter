// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestInputControllerComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests.Controllers
{
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Controllers;

    /// <summary>
    /// Defines a test <see cref="InputControllerComponentBase" /> class.
    /// </summary>
    public class TestInputControllerComponent : InputControllerComponentBase
    {
        /// <summary>
        /// Gets the direction vector.
        /// </summary>
        public override Vector2 Direction { get; }

        /// <summary>
        /// Destroys the component.
        /// </summary>
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
        /// Gets a value indicating whether a dash was requested.
        /// </summary>
        /// <returns>True if the action was requested; false otherwise.</returns>
        public override bool Dash()
        {
            return false;
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public override void Initialize()
        {
        }
    }
}