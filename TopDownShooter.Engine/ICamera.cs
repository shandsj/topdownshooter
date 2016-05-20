// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICamera.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines an interface for a camera.
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        Rectangle Bounds { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        float Rotation { get; set; }

        /// <summary>
        /// Gets the transform matrix.
        /// </summary>
        Matrix TransformMatrix { get; }

        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        float Zoom { get; set; }
    }
}