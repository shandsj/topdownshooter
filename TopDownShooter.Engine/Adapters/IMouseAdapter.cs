// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMouseAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Adapters
{
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Defines a mouse adapter interface.
    /// </summary>
    public interface IMouseAdapter
    {
        /// <summary>
        /// Gets mouse state information that includes position and button presses for the primary window
        /// </summary>
        /// <returns>The current state of the mouse.</returns>
        MouseState GetState();
    }
}