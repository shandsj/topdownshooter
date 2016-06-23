// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestInputControllerComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.UnitTests.Controllers
{
    using Microsoft.Xna.Framework;
    using TopDownShooter.Controllers;

    /// <summary>
    /// Defines a test <see cref="TopDownShooter.Controllers.InputControllerComponentBase" /> class.
    /// </summary>
    public class TestInputControllerComponent : InputControllerComponentBase
    {
        /// <summary>
        /// Gets the movement direction vector.
        /// </summary>
        public override Vector2 MovementDirection { get; }

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        public override float Rotation { get; }

        /// <summary>
        /// Gets a value indicating whether a dash was requested.
        /// </summary>
        /// <returns>True if the action was requested; false otherwise.</returns>
        public override bool Dash()
        {
            return false;
        }

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
        /// Initializes the component.
        /// </summary>
        public override void Initialize()
        {
        }
    }
}