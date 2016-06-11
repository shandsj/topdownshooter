// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FireMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Messages
{
    using TopDownShooter.Engine.Inventory;

    /// <summary>
    /// Defines a fire message class.
    /// </summary>
    public class FireMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FireMessage" /> class.
        /// </summary>
        public FireMessage()
            : base(MessageType.Fire)
        {
        }

        /// <summary>
        /// Gets or sets the projectile type.
        /// </summary>
        public ProjectileType ProjectileType { get; set; }
    }
}