// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRandomGenerator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    /// <summary>
    /// Interface for providing random values.
    /// </summary>
    public interface IRandomGenerator
    {
        /// <summary>
        /// Retreives the next random double between 0 and 1.
        /// </summary>
        /// <returns>The random double.</returns>
        double NextDouble();

        /// <summary>
        /// Retreives the next random double between a specified min and max value.
        /// </summary>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum exclusive value.</param>
        /// <returns>The random double.</returns>
        double NextDouble(double minimum, double maximum);
    }
}
