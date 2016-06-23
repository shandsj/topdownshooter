// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDashComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using System;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine;

    /// <summary>
    /// Defines an interface for a dash component.
    /// </summary>
    public interface IDashComponent : IComponent
    {
        /// <summary>
        /// Gets the amount of time the dash is active.
        /// </summary>
        TimeSpan ActiveTime { get; }

        /// <summary>
        /// Gets the time the last dash was completed.
        /// </summary>
        TimeSpan CompletedTime { get; }

        /// <summary>
        /// Gets the cooldown time.
        /// </summary>
        TimeSpan CooldownTime { get; }

        /// <summary>
        /// Gets the cost of a dash.
        /// </summary>
        int DashCost { get; }

        /// <summary>
        /// Gets the value affecting the velocity vector during the dash.
        /// </summary>
        float SpeedFactor { get; }

        /// <summary>
        /// Gets the time the last dash started.
        /// </summary>
        TimeSpan StartedTime { get; }

        /// <summary>
        /// Gets a value indicating whether the component is dashing at the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <returns>True if the component is dashing; false otherwise.</returns>
        bool IsDashing(GameTime gameTime);

        /// <summary>
        /// Attempts to start a dash at the specified game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="gameTime">The game time.</param>
        /// <returns>True if the dash successfully started; false otherwise.</returns>
        bool TryStartDash(IGameObject gameObject, GameTime gameTime);
    }
}