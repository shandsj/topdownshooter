// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemRandomGenerator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.RandomGenerators
{
    using System;

    /// <summary>
    /// Provides a random distribution provider based around the <see cref="Random"/> class.
    /// </summary>
    /// <remarks>Pretty much just a wrapper around the <see cref="Random"/> for consistency.</remarks>
    public class SystemRandomGenerator : IRandomGenerator
    {
        private Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRandomGenerator"/> class.
        /// </summary>
        public SystemRandomGenerator()
        {
            this.random = new Random((int)DateTime.Now.Ticks);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRandomGenerator"/> class.
        /// </summary>
        /// <param name="randomProvider">Can provide a configured random provider.</param>
        public SystemRandomGenerator(Random randomProvider)
        {
            this.random = randomProvider;
        }

        /// <summary>
        /// Retreives the next random double between 0 and 1.
        /// </summary>
        /// <returns>The random double.</returns>
        public double NextDouble()
        {
            return this.random.NextDouble();
        }

        /// <summary>
        /// Retreives the next random double between a specified min and max value.
        /// </summary>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum exclusive value.</param>
        /// <returns>The random double.</returns>
        public double NextDouble(double minimum, double maximum)
        {
            return this.random.NextDouble(minimum, maximum);
        }
    }
}