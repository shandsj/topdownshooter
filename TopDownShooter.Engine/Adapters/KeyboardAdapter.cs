// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyboardAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Adapters
{
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Provides a default implementation of the <see cref="IKeyboardAdapter" /> interface.
    /// </summary>
    public class KeyboardAdapter : IKeyboardAdapter
    {
        /// <summary>
        /// Returns the current keyboard state.
        /// </summary>
        /// <returns>The current keyboard state.</returns>
        public KeyboardState GetState()
        {
            return Keyboard.GetState();
        }
    }
}