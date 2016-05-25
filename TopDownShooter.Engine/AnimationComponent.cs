// <copyright file="AnimationComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Defines an animation class.
    /// </summary>
    public class AnimationComponent : IComponent
    {
        /// <summary>
        /// The asset name used to load content.
        /// </summary>
        private readonly string assetName;

        private TimeSpan lastFrameIndexChangeTime = TimeSpan.Zero;

        /// <summary>
        /// The texture that contains the frames.
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationComponent" /> class.
        /// </summary>
        /// <param name="assetName">The asset name used to load content.</param>
        /// <param name="frameProperties">The <see cref="FrameProperties" /> for this <see cref="AnimationComponent" />.</param>
        public AnimationComponent(string assetName, FrameProperties frameProperties)
        {
            this.assetName = assetName;
            this.FrameProperties = frameProperties;
            this.Scale = 1f;
        }

        /// <summary>
        /// Gets the current frame index.
        /// </summary>
        public int FrameIndex { get; private set; }

        /// <summary>
        /// Gets the <see cref="FrameProperties" />  for this <see cref="AnimationComponent" />.
        /// </summary>
        public FrameProperties FrameProperties { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="AnimationComponent" /> is animating.
        /// </summary>
        public bool IsAnimating { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to loop the <see cref="AnimationComponent    " />.
        /// </summary>
        public bool IsLooping { get; set; }

        /// <summary>
        /// Gets or sets the rotation for this <see cref="AnimationComponent" />.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Gets or sets the scale for this <see cref="AnimationComponent" />.
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SpriteEffects" /> for this <see cref="AnimationComponent" />.
        /// </summary>
        public SpriteEffects SpriteEffect { get; set; }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Loads the content of this <see cref="AnimationComponent" />.
        /// </summary>
        /// <param name="contentManager">
        /// The <see cref="IContentManagerAdapter" />.
        /// </param>
        public void LoadContent(IContentManagerAdapter contentManager)
        {
            this.texture = contentManager.Load<Texture2D>(this.assetName);
        }

        /// <summary>
        /// Resets the <see cref="AnimationComponent" />.
        /// </summary>
        public void Reset()
        {
            this.FrameIndex = 0;
            this.lastFrameIndexChangeTime = TimeSpan.Zero;
        }

        /// <summary>
        /// Unloads the content of this <see cref="AnimationComponent" />.
        /// </summary>
        /// <param name="contentManager">The <see cref="IContentManagerAdapter" />.</param>
        public void UnloadContent(IContentManagerAdapter contentManager)
        {
        }

        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="message">The message object.</param>
        public void ReceiveMessage(IGameObject gameObject, object message)
        {
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="time">The game time.</param>
        public void Update(IGameObject gameObject, GameTime time)
        {
            if (!this.IsAnimating)
            {
                return;
            }

            if (time.TotalGameTime - this.lastFrameIndexChangeTime > this.FrameProperties.Duration)
            {
                // Play the next frame in the SpriteSheet
                this.FrameIndex++;
                if (this.IsLooping && this.FrameIndex > this.FrameProperties.Count)
                {
                    this.FrameIndex = 0;
                }

                // reset elapsed time
                this.lastFrameIndexChangeTime = time.TotalGameTime;
            }
        }

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        public void Draw(IGameObject gameObject, ISpriteBatchAdapter spriteBatch, GameTime time)
        {
            // Calculate the source rectangle of the current frame.
            var source = new Rectangle(this.FrameIndex * this.FrameProperties.Width, 0, this.FrameProperties.Width, this.FrameProperties.Height);

            //// Calculate position and origin to draw in the center of the screen
            // Vector2 position = new Vector2(game.Window.ClientBounds.Width / 2,
            // game.Window.ClientBounds.Height / 2);
            Vector2 origin = new Vector2(this.FrameProperties.Width / 2.0f, this.FrameProperties.Height / 2.0f);

            // Draw the current frame.
            spriteBatch.Draw(
                this.texture,
                gameObject.Position,
                source,
                Color.White,
                this.Rotation,
                origin,
                this.Scale,
                this.SpriteEffect,
                0.0f);
        }
    }
}