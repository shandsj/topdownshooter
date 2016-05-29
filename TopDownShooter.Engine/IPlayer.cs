// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPlayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    /// <summary>
    /// Defines an interface for a player.
    /// </summary>
    public interface IPlayer : IGameObject
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        string Name { get; set; }
    }
}