// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IKeyboardAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Adapters
{
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Defines an interface for a keyboard adapter.
    /// </summary>
    public interface IKeyboardAdapter
    {
        /// <summary>
        /// Returns the current keyboard state.
        /// </summary>
        /// <returns>The current keyboard state.</returns>
        KeyboardState GetState();
    }
}