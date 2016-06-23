// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryRequestMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Messages
{
    using TopDownShooter.Engine;

    /// <summary>
    /// Defines an inventory request message.
    /// </summary>
    public class InventoryRequestMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryRequestMessage" /> class.
        /// </summary>
        public InventoryRequestMessage()
            : base((int)Messages.MessageType.InventoryRequest)
        {
        }

        /// <summary>
        /// Gets or sets the number of coins in the message.
        /// </summary>
        public int CoinCount { get; set; }

        /// <summary>
        /// Gets or sets the number of long range projectiles in the message.
        /// </summary>
        public int LongRangeProjectileCoint { get; set; }
    }
}