// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageType.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Messages
{
    /// <summary>
    /// Defines the message types used to communicate between <see cref="IComponent" /> objects.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// The fire message type.
        /// </summary>
        Fire,

        /// <summary>
        /// Represents a change in movement
        /// </summary>
        /// <remarks>Details will be a velocity vector?</remarks>
        Movement,

        /// <summary>
        /// Represents an item requested to be picked up.
        /// </summary>
        /// <remarks>Details should be the <see cref="IGameItem"/></remarks>
        ItemPickup,

        /// <summary>
        /// Represents a dash movement.
        /// </summary>
        Dash,

        /// <summary>
        /// Represents a dash status request.
        /// </summary>
        DashStatusRequest,

        /// <summary>
        /// Represents a drop coins message.
        /// </summary>
        DropCoins
    }
}