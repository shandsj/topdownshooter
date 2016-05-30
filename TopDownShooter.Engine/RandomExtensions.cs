// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System;

    /// <summary>
    /// Contains extension methods for the <see cref="Random" /> class.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Retreives the next random double between the specified range.
        /// </summary>
        /// <param name="random">The <see cref="Random" /> object.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum exclusive value.</param>
        /// <returns>The random double.</returns>
        public static double NextDouble(this Random random, double minimum, double maximum)
        {
            return (random.NextDouble() * (maximum - minimum)) + minimum;
        }
    }
}