// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MouseAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Adapters
{
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Provides a default implementation of the <see cref="IMouseAdapter" /> interface.
    /// </summary>
    public class MouseAdapter : IMouseAdapter
    {
        /// <summary>
        /// Gets mouse state information that includes position and button presses for the primary window
        /// </summary>
        /// <returns>The current state of the mouse.</returns>
        public MouseState GetState()
        {
            return Mouse.GetState();
        }
    }
}