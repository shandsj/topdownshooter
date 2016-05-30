// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGamePadAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Adapters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Defines an interface for a game pad adapter.
    /// </summary>
    public interface IGamePadAdapter
    {
        /// <summary>
        /// Gets the current state of a game pad controller with an independent axes dead zone.
        /// </summary>
        /// <param name="playerIndex">Player index for the controller you want to query.</param>
        /// <returns>The state of the controller.</returns>
        GamePadState GetState(PlayerIndex playerIndex);
    }
}