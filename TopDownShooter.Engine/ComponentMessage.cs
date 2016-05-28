// <copyright file="ComponentMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Message object that can detail what type of message it is
    /// and provide further optional details.
    /// </summary>
    public class ComponentMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentMessage"/> class.
        /// </summary>
        /// <param name="messageType">Message Type</param>
        /// <param name="messageDetails">Optional message details</param>
        public ComponentMessage(MessageType messageType, object messageDetails = null)
        {
            this.MessageType = messageType;
            this.MessageDetails = messageDetails;
        }

        /// <summary>
        /// Gets the MessageType
        /// </summary>
        public MessageType MessageType { get; private set; }

        /// <summary>
        /// Gets the optional details object that can
        /// be used to detail more about the message.
        /// </summary>
        public object MessageDetails { get; private set; }
    }
}
