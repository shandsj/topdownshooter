// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DashStatusRequestMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    /// <summary>
    /// Defines a message that requests a dash status of a player.
    /// </summary>
    public class DashStatusRequestMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashStatusRequestMessage" /> class.
        /// </summary>
        public DashStatusRequestMessage()
            : base(MessageType.DashStatusRequest)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether a dash is in progress.
        /// </summary>
        public bool IsDashing { get; set; }
    }
}