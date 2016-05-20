// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameProperties.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System;

    /// <summary>
    /// Defines a structure for the frame properties of an <see cref="Animation" />.
    /// </summary>
    public struct FrameProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameProperties"/> struct.
        /// </summary>
        /// <param name="width">The width of a frame.</param>
        /// <param name="height">The height of a frame.</param>
        /// <param name="duration">The duration of a frame.</param>
        /// <param name="count">The number of frames in an <see cref="Animation" />.</param>
        public FrameProperties(int width, int height, TimeSpan duration, int count)
            : this()
        {
            this.Width = width;
            this.Height = height;
            this.Duration = duration;
            this.Count = count;
        }

        /// <summary>
        /// Gets the number of frames in an <see cref="Animation" />.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the duration of a frame.
        /// </summary>
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// Gets the height of a frame.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the width of a frame.
        /// </summary>
        public int Width { get; private set; }
    }
}