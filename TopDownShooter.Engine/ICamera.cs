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

        /// <summary>
        /// Gets the screen coordinates for the specified world coordinates.
        /// </summary>
        /// <param name="worldCoordinates">The world coordinates.</param>
        /// <returns>The screen coordinates.</returns>
        Vector2 GetScreenCoordinates(Vector2 worldCoordinates);

        /// <summary>
        /// Gets the world coordinates for the specified screen coordinates.
        /// </summary>
        /// <param name="screenCoordinates">The screen coordinates.</param>
        /// <returns>The world coordinates.</returns>
        Vector2 GetWorldCoordinates(Vector2 screenCoordinates);
    }
}