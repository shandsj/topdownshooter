// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICamera2DAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TopDownShooter.Engine.Adapters
{
    using Microsoft.Xna.Framework;
    using MonoGame.Extended;
    using MonoGame.Extended.Shapes;

    /// <summary>
    /// Defines an interface for a camera.
    /// </summary>
    public interface ICamera2DAdapter
    {
        /// <summary>
        /// Gets or sets the maximum zoom.
        /// </summary>
        float MaximumZoom { get; set; }

        /// <summary>
        /// Gets or sets the minimum zoom.
        /// </summary>
        float MinimumZoom { get; set; }

        /// <summary>
        /// Gets or sets the origin.
        /// </summary>
        Vector2 Origin { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        float Rotation { get; set; }

        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        float Zoom { get; set; }

        /// <summary>
        /// Determines if the specified rectangle is contained in the camera.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="ContainmentType"/>.</returns>
        ContainmentType Contains(Rectangle rectangle);

        /// <summary>
        /// Determines if the specified vector is contained in the camera.
        /// </summary>
        /// <param name="vector2">The vector.</param>
        /// <returns>The <see cref="ContainmentType"/>.</returns>
        ContainmentType Contains(Vector2 vector2);

        /// <summary>
        /// Determines if the specified point is contained in the camera.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="ContainmentType"/>.</returns>
        ContainmentType Contains(Point point);

        /// <summary>
        /// Gets the bounding frustum.
        /// </summary>
        /// <returns>The bounding frustum.</returns>
        BoundingFrustum GetBoundingFrustum();

        /// <summary>
        /// Gets the bounding rectangle.
        /// </summary>
        /// <returns>The bounding rectangle.</returns>
        RectangleF GetBoundingRectangle();

        /// <summary>
        /// Gets the inverse view matrix.
        /// </summary>
        /// <returns>The inverse view matrix.</returns>
        Matrix GetInverseViewMatrix();

        /// <summary>
        /// Gets the view matrix.
        /// </summary>
        /// <returns>The view matrix.</returns>
        Matrix GetViewMatrix();

        /// <summary>
        /// Gets the view matrix with the specified parallax factor.
        /// </summary>
        /// <param name="parallaxFactor">The parallax factor.</param>
        /// <returns>The view matrix.</returns>
        Matrix GetViewMatrix(Vector2 parallaxFactor);

        /// <summary>
        /// Instructs the camera to look at the specified location.
        /// </summary>
        /// <param name="position">The location.</param>
        void LookAt(Vector2 position);

        /// <summary>
        /// Instructs the camera to move in the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        void Move(Vector2 direction);

        /// <summary>
        /// Rotates the camera the specified radians.
        /// </summary>
        /// <param name="deltaRadians">The change in radians.</param>
        void Rotate(float deltaRadians);

        /// <summary>
        /// Converts the specified screen position to world coordinates.
        /// </summary>
        /// <param name="screenPosition">The screen position.</param>
        /// <returns>The converted world coordinates.</returns>
        Vector2 ScreenToWorld(Vector2 screenPosition);

        /// <summary>
        /// Converts the specified screen position to world coordinates.
        /// </summary>
        /// <param name="x">The x screen coordinate.</param>
        /// <param name="y">The y screen coordinate.</param>
        /// <returns>The converted world coordinates.</returns>
        Vector2 ScreenToWorld(float x, float y);

        /// <summary>
        /// Converts the specified world position to screen coordinates.
        /// </summary>
        /// <param name="worldPosition">The world position.</param>
        /// <returns>The converted screen coordinates.</returns>
        Vector2 WorldToScreen(Vector2 worldPosition);

        /// <summary>
        /// Converts the specified world position to screen coordinates.
        /// </summary>
        /// <param name="x">The x world coordinate.</param>
        /// <param name="y">The y world coordinate.</param>
        /// <returns>The converted screen coordinates.</returns>
        Vector2 WorldToScreen(float x, float y);

        /// <summary>
        /// Zooms in the camera the specified change.
        /// </summary>
        /// <param name="deltaZoom">The change in zoom level.</param>
        void ZoomIn(float deltaZoom);

        /// <summary>
        /// Zooms out the camera the specified change.
        /// </summary>
        /// <param name="deltaZoom">The change in zoom level.</param>
        void ZoomOut(float deltaZoom);
    }
}