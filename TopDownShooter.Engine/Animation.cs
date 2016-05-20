// <copyright file="Animation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines an animation class.
    /// </summary>
    public class Animation
    {
        /// <summary>
        /// The asset name used to load content.
        /// </summary>
        private readonly string assetName;

        /// <summary>
        /// The current duration of the current frame.
        /// </summary>
        private TimeSpan duration;

        /// <summary>
        /// The texture that contains the frames.
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// Initializes a new instance of the <see cref="Animation" /> class.
        /// </summary>
        /// <param name="assetName">The asset name used to load content.</param>
        /// <param name="frameProperties">The <see cref="FrameProperties" /> for this <see cref="Animation" />.</param>
        public Animation(string assetName, FrameProperties frameProperties)
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
        /// Gets the <see cref="FrameProperties" />  for this <see cref="Animation" />.
        /// </summary>
        public FrameProperties FrameProperties { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Animation" /> is animating.
        /// </summary>
        public bool IsAnimating { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to loop the <see cref="Animation    " />.
        /// </summary>
        public bool IsLooping { get; set; }

        /// <summary>
        /// Gets or sets the position of this animation.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the rotation for this <see cref="Animation" />.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Gets or sets the scale for this <see cref="Animation" />.
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SpriteEffects" /> for this <see cref="Animation" />.
        /// </summary>
        public SpriteEffects SpriteEffect { get; set; }

        /// <summary>
        /// Draws this <see cref="Animation" />.
        /// </summary>
        /// <param name="spriteBatch">
        /// The <see cref="ISpriteBatchAdapter" />.
        /// </param>
        /// <param name="gameTime">
        /// The game time.
        /// </param>
        public void Draw(ISpriteBatchAdapter spriteBatch, GameTime gameTime)
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
                this.Position,
                source,
                Color.White,
                this.Rotation,
                origin,
                this.Scale,
                this.SpriteEffect,
                0.0f);
        }

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
        /// Loads the content of this <see cref="Animation" />.
        /// </summary>
        /// <param name="content">
        /// The <see cref="ContentManager" />.
        /// </param>
        public void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>(this.assetName);
        }

        /// <summary>
        /// Resets the <see cref="Animation" />.
        /// </summary>
        public void Reset()
        {
            this.FrameIndex = 0;
            this.duration = TimeSpan.FromSeconds(0);
        }

        /// <summary>
        /// Unloads the content of this <see cref="Animation" />.
        /// </summary>
        /// <param name="content">The <see cref="ContentManager" />.</param>
        public void UnloadContent(ContentManager content)
        {
        }

        /// <summary>
        /// Updates the state of this <see cref="Animation" />.
        /// </summary>
        /// <param name="gameTime">
        /// The game time.
        /// </param>
        public void Update(GameTime gameTime)
        {
            if (!this.IsAnimating)
            {
                return;
            }

            this.duration += gameTime.ElapsedGameTime;
            while (this.duration >= this.FrameProperties.Duration)
            {
                // Play the next frame in the SpriteSheet
                this.FrameIndex++;
                if (this.IsLooping && this.FrameIndex > this.FrameProperties.Count)
                {
                    this.FrameIndex = 0;
                }

                // reset elapsed time
                this.duration = TimeSpan.FromSeconds(0);
            }
        }
    }
}