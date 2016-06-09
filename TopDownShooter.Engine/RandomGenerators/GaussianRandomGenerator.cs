// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GaussianRandomGenerator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.RandomGenerators
{
    using System;

    /// <summary>
    /// Provides a gaussian distribution provider.
    /// </summary>
    public class GaussianRandomGenerator : IRandomGenerator
    {
        private Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="GaussianRandomGenerator"/> class.
        /// </summary>
        public GaussianRandomGenerator()
        {
            this.random = new Random((int)DateTime.Now.Ticks);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GaussianRandomGenerator"/> class.
        /// </summary>
        /// <param name="randomProvider">Can provide a configured random provider.</param>
        public GaussianRandomGenerator(Random randomProvider)
        {
            this.random = randomProvider;
        }

        /// <summary>
        /// Retreives the next random double between 0 and 1.
        /// </summary>
        /// <returns>The random double.</returns>
        public double NextDouble()
        {
            return this.NextDouble(0, 1);
        }

        /// <summary>
        /// Retreives the next random double between a specified min and max value.
        /// </summary>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum exclusive value.</param>
        /// <returns>The random double.</returns>
        public double NextDouble(double minimum, double maximum)
        {
            // Just making shit up as a I go here...
            double mean = maximum / 2;
            double stdDev = maximum / 4;

            return this.NextGaussian(mean, stdDev, minimum, maximum);
        }

        /// <summary>
        /// Generates a random gaussian value with a mean of 0 and standard deviation of 1
        /// </summary>
        /// <returns>Random value</returns>
        private double NextGaussian()
        {
            double v1, v2, s;
            do
            {
                v1 = (2.0f * this.random.NextDouble()) - 1.0f;
                v2 = (2.0f * this.random.NextDouble()) - 1.0f;
                s = (v1 * v1) + (v2 * v2);
            }
            while (s >= 1.0f || s == 0f);

            s = Math.Sqrt((-2.0f * Math.Log(s)) / s);

            return v1 * s;
        }

        /// <summary>
        /// Generates a random gaussian value with a mean of 0 and standard deviation of 1
        /// </summary>
        /// <param name="mean">Configure the mean value for this distribution sample</param>
        /// <param name="standard_deviation">Configure the standard deviation value for this distribution sample</param>
        /// <returns>Random value</returns>
        private double NextGaussian(double mean, double standard_deviation)
        {
            return mean + (this.NextGaussian() * standard_deviation);
        }

        /// <summary>
        /// Generates a random gaussian value with a mean of 0 and standard deviation of 1
        /// </summary>
        /// <param name="mean">Configure the mean value for this distribution sample</param>
        /// <param name="standard_deviation">Configure the standard deviation value for this distribution sample</param>
        /// <param name="minimum">Minimum value</param>
        /// <param name="maximum">Maximum value</param>
        /// <returns>Random value</returns>
        private double NextGaussian(double mean, double standard_deviation, double minimum, double maximum)
        {
            double x;
            do
            {
                x = this.NextGaussian(mean, standard_deviation);
            }
            while (x < minimum || x > maximum);

            return x;
        }
    }
}