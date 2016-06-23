// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DropCoinsMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Messages
{
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine;

    /// <summary>
    /// Defines a message to notify a game object to drop coins.
    /// </summary>
    public class DropCoinsMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropCoinsMessage" /> class.
        /// </summary>
        /// <param name="location">The location where the coin drop should occur.</param>
        public DropCoinsMessage(Vector2 location)
            : this(location, int.MaxValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DropCoinsMessage" /> class.
        /// </summary>
        /// <param name="location">The location where the coin drop should occur.</param>
        /// <param name="count">The number of coins to drop.</param>
        public DropCoinsMessage(Vector2 location, int count)
            : base((int)Messages.MessageType.DropCoins)
        {
            this.Location = location;
            this.Count = count;
        }

        /// <summary>
        /// Gets or sets the number of coins to drop.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets the location where the coin drop should occur.
        /// </summary>
        public Vector2 Location { get; private set; }
    }
}