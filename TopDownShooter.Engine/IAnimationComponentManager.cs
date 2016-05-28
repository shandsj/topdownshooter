// <copyright file="IAnimationComponentManager.cs" company="PlaceholderCompany">
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
    /// Interface for managing a collection of <see cref="IAnimationComponent"/>
    /// </summary>
    public interface IAnimationComponentManager : IComponent
    {
        /// <summary>
        /// Gets the <see cref="FrameProperties"/> for the current animation
        /// </summary>
        FrameProperties FrameProperties { get; }

        /// <summary>
        /// Plays an animation with the given <paramref name="animationLabel"/>
        /// </summary>
        /// <param name="animationLabel">Labeled animation to play.</param>
        /// <param name="isLooping">Toggles whether this animation will play in loop mode</param>
        void Play(string animationLabel, bool isLooping = true);

        /// <summary>
        /// Stops the currently playing animation.
        /// </summary>
        void Stop();
    }
}
