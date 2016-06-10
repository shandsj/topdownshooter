// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageEventArgs.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Contains event data for message events.
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs" /> class.
        /// </summary>
        /// <param name="message">The message associated with the event.</param>
        /// <param name="gameTime">The game time associated with the event.</param>
        public MessageEventArgs(Message message, GameTime gameTime)
        {
            this.Message = message;
            this.GameTime = gameTime;
        }

        /// <summary>
        /// Gets the game time associated with the event.
        /// </summary>
        public GameTime GameTime { get; private set; }

        /// <summary>
        /// Gets the message associated with the event.
        /// </summary>
        public Message Message { get; private set; }
    }
}