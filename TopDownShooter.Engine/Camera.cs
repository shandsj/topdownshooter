﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Camera.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The camera.
    /// </summary>
    public class Camera : ICamera
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Camera" /> class.
        /// </summary>
        /// <param name="viewport">
        /// The viewport.
        /// </param>
        public Camera(Viewport viewport)
        {
            this.Bounds = viewport.Bounds;
            this.Position = Vector2.Zero;
            this.Zoom = 1f;
            this.Rotation = 0;
        }

        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Gets the transform matrix.
        /// </summary>
        public Matrix TransformMatrix => Matrix.CreateTranslation(new Vector3(-this.Position.X, -this.Position.Y, 0)) *
                                         Matrix.CreateRotationZ(this.Rotation) *
                                         Matrix.CreateScale(this.Zoom) *
                                         Matrix.CreateTranslation(new Vector3(this.Bounds.Width * 0.5f, this.Bounds.Height * 0.5f, 0));

        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        public float Zoom { get; set; }

        /// <summary>
        /// Gets the screen coordinates for the specified world coordinates.
        /// </summary>
        /// <param name="worldCoordinates">The world coordinates.</param>
        /// <returns>The screen coordinates.</returns>
        public Vector2 GetScreenCoordinates(Vector2 worldCoordinates)
        {
            return Vector2.Transform(worldCoordinates, this.TransformMatrix);
        }

        /// <summary>
        /// Gets the world coordinates for the specified screen coordinates.
        /// </summary>
        /// <param name="screenCoordinates">The screen coordinates.</param>
        /// <returns>The world coordinates.</returns>
        public Vector2 GetWorldCoordinates(Vector2 screenCoordinates)
        {
            return Vector2.Transform(screenCoordinates, Matrix.Invert(this.TransformMatrix));
        }
    }
}