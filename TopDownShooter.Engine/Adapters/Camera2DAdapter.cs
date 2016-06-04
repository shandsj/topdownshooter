// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Camera2DAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Adapters
{
    using Microsoft.Xna.Framework;
    using MonoGame.Extended;
    using MonoGame.Extended.Shapes;

    /// <summary>
    /// Provides a default implementation of the <see cref="ICamera2DAdapter" /> class.
    /// </summary>
    public class Camera2DAdapter : ICamera2DAdapter
    {
        private readonly Camera2D camera;

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera2DAdapter"/> class.
        /// </summary>
        /// <param name="camera">The <see cref="Camera2D"/>.</param>
        public Camera2DAdapter(Camera2D camera)
        {
            this.camera = camera;
        }

        /// <summary>
        /// Gets or sets the maximum zoom.
        /// </summary>
        public float MaximumZoom
        {
            get
            {
                return this.camera.MaximumZoom;
            }

            set
            {
                this.camera.MaximumZoom = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum zoom.
        /// </summary>
        public float MinimumZoom
        {
            get
            {
                return this.camera.MinimumZoom;
            }

            set
            {
                this.camera.MinimumZoom = value;
            }
        }

        /// <summary>
        /// Gets or sets the origin.
        /// </summary>
        public Vector2 Origin
        {
            get
            {
                return this.camera.Origin;
            }

            set
            {
                this.camera.Origin = value;
            }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return this.camera.Position;
            }

            set
            {
                this.camera.Position = value;
            }
        }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        public float Rotation
        {
            get
            {
                return this.camera.Rotation;
            }

            set
            {
                this.camera.Rotation = value;
            }
        }

        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        public float Zoom
        {
            get
            {
                return this.camera.Zoom;
            }

            set
            {
                this.camera.Zoom = value;
            }
        }

        /// <summary>
        /// Determines if the specified rectangle is contained in the camera.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="ContainmentType" />.</returns>
        public ContainmentType Contains(Rectangle rectangle)
        {
            return this.camera.Contains(rectangle);
        }

        /// <summary>
        /// Determines if the specified vector is contained in the camera.
        /// </summary>
        /// <param name="vector2">The vector.</param>
        /// <returns>The <see cref="ContainmentType" />.</returns>
        public ContainmentType Contains(Vector2 vector2)
        {
            return this.camera.Contains(vector2);
        }

        /// <summary>
        /// Determines if the specified point is contained in the camera.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="ContainmentType" />.</returns>
        public ContainmentType Contains(Point point)
        {
            return this.camera.Contains(point);
        }

        /// <summary>
        /// Gets the bounding frustum.
        /// </summary>
        /// <returns>The bounding frustum.</returns>
        public BoundingFrustum GetBoundingFrustum()
        {
            return this.camera.GetBoundingFrustum();
        }

        /// <summary>
        /// Gets the bounding rectangle.
        /// </summary>
        /// <returns>The bounding rectangle</returns>
        public RectangleF GetBoundingRectangle()
        {
            return this.camera.GetBoundingRectangle();
        }

        /// <summary>
        /// Gets the inverse view matrix.
        /// </summary>
        /// <returns>The inverse view matrix.</returns>
        public Matrix GetInverseViewMatrix()
        {
            return this.camera.GetInverseViewMatrix();
        }

        /// <summary>
        /// Gets the view matrix.
        /// </summary>
        /// <returns>The view matrix.</returns>
        public Matrix GetViewMatrix()
        {
            return this.camera.GetViewMatrix();
        }

        /// <summary>
        /// Gets the view matrix with the specified parallax factor.
        /// </summary>
        /// <param name="parallaxFactor">The parallax factor.</param>
        /// <returns>The view matrix.</returns>
        public Matrix GetViewMatrix(Vector2 parallaxFactor)
        {
            return this.camera.GetViewMatrix(parallaxFactor);
        }

        /// <summary>
        /// Instructs the camera to look at the specified location.
        /// </summary>
        /// <param name="position">The location.</param>
        public void LookAt(Vector2 position)
        {
            this.camera.LookAt(position);
        }

        /// <summary>
        /// Instructs the camera to move in the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void Move(Vector2 direction)
        {
            this.camera.Move(direction);
        }

        /// <summary>
        /// Rotates the camera the specified radians.
        /// </summary>
        /// <param name="deltaRadians">The change in radians.</param>
        public void Rotate(float deltaRadians)
        {
            this.camera.Rotate(deltaRadians);
        }

        /// <summary>
        /// Converts the specified screen position to world coordinates.
        /// </summary>
        /// <param name="screenPosition">The screen position.</param>
        /// <returns>The converted world coordinates.</returns>
        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return this.camera.ScreenToWorld(screenPosition);
        }

        /// <summary>
        /// Converts the specified screen position to world coordinates.
        /// </summary>
        /// <param name="x">The x screen coordinate.</param>
        /// <param name="y">The y screen coordinate.</param>
        /// <returns>The converted world coordinates.</returns>
        public Vector2 ScreenToWorld(float x, float y)
        {
            return this.camera.ScreenToWorld(x, y);
        }

        /// <summary>
        /// Converts the specified world position to screen coordinates.
        /// </summary>
        /// <param name="worldPosition">The world position.</param>
        /// <returns>The converted screen coordinates.</returns>
        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return this.camera.WorldToScreen(worldPosition);
        }

        /// <summary>
        /// Converts the specified world position to screen coordinates.
        /// </summary>
        /// <param name="x">The x world coordinate.</param>
        /// <param name="y">The y world coordinate.</param>
        /// <returns>The converted screen coordinates.</returns>
        public Vector2 WorldToScreen(float x, float y)
        {
            return this.camera.WorldToScreen(x, y);
        }

        /// <summary>
        /// Zooms in the camera the specified change.
        /// </summary>
        /// <param name="deltaZoom">The change in zoom level.</param>
        public void ZoomIn(float deltaZoom)
        {
            this.camera.ZoomIn(deltaZoom);
        }

        /// <summary>
        /// Zooms out the camera the specified change.
        /// </summary>
        /// <param name="deltaZoom">The change in zoom level.</param>
        public void ZoomOut(float deltaZoom)
        {
            this.camera.ZoomOut(deltaZoom);
        }
    }
}