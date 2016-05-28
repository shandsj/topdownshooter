// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAnimationComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines an interface for an animation component.
    /// </summary>
    public interface IAnimationComponent : IComponent
    {
        /// <summary>
        /// Gets the current frame index.
        /// </summary>
        int FrameIndex { get; }

        /// <summary>
        /// Gets the <see cref="FrameProperties" />  for this <see cref="AnimationComponent" />.
        /// </summary>
        FrameProperties FrameProperties { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="AnimationComponent" /> is animating.
        /// </summary>
        bool IsAnimating { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to loop the <see cref="AnimationComponent    " />.
        /// </summary>
        bool IsLooping { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to render this animation component.
        /// </summary>
        bool IsRendered { get; set; }

        /// <summary>
        /// Gets the label of the animation.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Gets or sets the rotation for this <see cref="AnimationComponent" />.
        /// </summary>
        float Rotation { get; set; }

        /// <summary>
        /// Gets or sets the scale for this <see cref="AnimationComponent" />.
        /// </summary>
        float Scale { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SpriteEffects" /> for this <see cref="AnimationComponent" />.
        /// </summary>
        SpriteEffects SpriteEffect { get; set; }

        /// <summary>
        /// Resets the <see cref="AnimationComponent" />.
        /// </summary>
        void Reset();
    }
}