// <copyright file="Message.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    /// <summary>
    /// Message object that can detail what type of message it is
    /// and provide further optional details.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="messageType">Message Type</param>
        /// <param name="messageDetails">Optional message details</param>
        public Message(int messageType, object messageDetails = null)
        {
            this.MessageType = messageType;
            this.MessageDetails = messageDetails;
        }

        /// <summary>
        /// Gets the MessageType
        /// </summary>
        public int MessageType { get; private set; }

        /// <summary>
        /// Gets the optional details object that can
        /// be used to detail more about the message.
        /// </summary>
        public object MessageDetails { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether a component or game object handled this message.
        /// </summary>
        public bool IsHandled { get; set; }
    }
}
