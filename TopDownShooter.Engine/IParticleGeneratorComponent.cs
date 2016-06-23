namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Adapters;

    public interface IParticleGeneratorComponent : IComponent
    {
        /// <summary>
        /// Gets the collection of <see cref="Color" /> values used for particles.
        /// </summary>
        List<Color> Colors { get; }

        /// <summary>
        /// Gets or sets the number of particles.
        /// </summary>
        int Count { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ParticleGeneratorComponent" /> is emitting.
        /// </summary>
        bool IsEmitting { get; set; }

        /// <summary>
        /// Gets or sets the maximum size of the particles.
        /// </summary>
        float MaximumSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum time to live.
        /// </summary>
        TimeSpan MaximumTimeToLive { get; set; }

        /// <summary>
        /// Gets or sets the maximum velocity of the <see cref="Particle" /> objects generated by this <see cref="ParticleGeneratorComponent" />.
        /// </summary>
        Vector2 MaximumVelocity { get; set; }

        /// <summary>
        /// Gets or sets the minimum size of the particles.
        /// </summary>
        float MinimumSize { get; set; }

        /// <summary>
        /// Gets or sets the minimum time to live.
        /// </summary>
        TimeSpan MinimumTimeToLive { get; set; }

        /// <summary>
        /// Gets or sets the minimum velocity of the <see cref="Particle" /> objects generated by this <see cref="ParticleGeneratorComponent" />.
        /// </summary>
        Vector2 MinimumVelocity { get; set; }
    }
}