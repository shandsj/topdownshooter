// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompletedEventArgs.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System;

    /// <summary>
    /// Contains event data for a completed operation.
    /// </summary>
    public class CompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompletedEventArgs" /> class.
        /// </summary>
        public CompletedEventArgs()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompletedEventArgs" /> class.
        /// </summary>
        /// <param name="data">The data associated with the completed event.</param>
        public CompletedEventArgs(object data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Gets the data associated with the completed event.
        /// </summary>
        public object Data { get; }
    }
}