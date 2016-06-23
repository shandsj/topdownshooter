// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FireMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Messages
{
    using TopDownShooter.Engine;
    using TopDownShooter.Projectiles;

    /// <summary>
    /// Defines a fire message class.
    /// </summary>
    public class FireMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FireMessage" /> class.
        /// </summary>
        public FireMessage()
            : base((int)Messages.MessageType.Fire)
        {
        }

        /// <summary>
        /// Gets or sets the projectile type.
        /// </summary>
        public ProjectileType ProjectileType { get; set; }
    }
}