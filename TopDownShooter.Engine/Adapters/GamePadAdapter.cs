// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GamePadAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Adapters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Provides a default implementation of the <see cref="IGamePadAdapter" /> interface.
    /// </summary>
    public class GamePadAdapter : IGamePadAdapter
    {
        /// <summary>
        /// Gets the current state of a game pad controller with an independent axes dead zone.
        /// </summary>
        /// <param name="playerIndex">Player index for the controller you want to query.</param>
        /// <returns>The state of the controller.</returns>
        public GamePadState GetState(PlayerIndex playerIndex)
        {
            return GamePad.GetState(playerIndex);
        }
    }
}