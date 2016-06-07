// <copyright file="DebugComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    ///  Primitive Debug Component, uses the <see cref="GameObject.Bounds"/> to place a hollow <see cref="Rectangle"/>
    ///  around it.
    /// </summary>
    public class DebugComponent : IComponent
    {
        private Texture2D pointTexture;
        private Color color;
        private int borderThickness;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugComponent"/> class.
        /// </summary>
        /// <param name="color">Border color for the bounding box around a given <see cref="GameObject"/></param>
        /// <param name="borderThickness">The thickness for the rectangluar border for a given <see cref="GameObject"/></param>
        public DebugComponent(Color color, int borderThickness)
        {
            this.color = color;
            this.borderThickness = borderThickness;
        }

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="camera">The <see cref="ICamera2DAdapter"/>.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        public void Draw(IGameObject gameObject, ICamera2DAdapter camera, ISpriteBatchAdapter spriteBatch, GameTime time)
        {
            this.DrawRectangle(spriteBatch, gameObject.Bounds);
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public void LoadContent(IContentManagerAdapter contentManager)
        {
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Destroys the component.
        /// </summary>
        public void Destroy()
        {
            this.pointTexture?.Dispose();
        }

        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="message">The message object.</param>
        /// <param name="gameTime">The game time.</param>
        public void ReceiveMessage(IGameObject gameObject, ComponentMessage message, GameTime gameTime)
        {
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="gameTime">The game time.</param>
        public void Update(IGameObject gameObject, GameTime gameTime)
        {
        }

        /// <summary>
        /// Draws a hollow rectangle around a provided <see cref="Rectangle"/>
        /// </summary>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="area">Area to wrap with a border</param>
        private void DrawRectangle(ISpriteBatchAdapter spriteBatch, Rectangle area)
        {
            if (this.pointTexture == null)
            {
                this.pointTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                this.pointTexture.SetData<Color>(new Color[] { this.color });
            }

            // left, top, right, bottom
            spriteBatch.Draw(this.pointTexture, new Rectangle(area.X, area.Y, this.borderThickness, area.Height), this.color);
            spriteBatch.Draw(this.pointTexture, new Rectangle(area.X, area.Y, area.Width, this.borderThickness), this.color);
            spriteBatch.Draw(this.pointTexture, new Rectangle(area.X + area.Width - this.borderThickness, area.Y, this.borderThickness, area.Height), this.color);
            spriteBatch.Draw(this.pointTexture, new Rectangle(area.X, area.Y + area.Height - this.borderThickness, area.Width, this.borderThickness), this.color);
        }
    }
}
