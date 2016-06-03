// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TitleScene.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Defines a title scene.
    /// </summary>
    public class TitleScene : IScene
    {
        private readonly GraphicsDevice graphicsDevice;

        private int alpha;

        private SpriteFont font;

        private TimeSpan notFadingStartTime;

        private ISpriteBatchAdapter spriteBatch;

        private State state = State.FadingIn;

        private string text = "My Shooter Game";

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleScene" /> class.
        /// </summary>
        /// <param name="graphicsDevice">The <see cref="GraphicsDevice" />.</param>
        public TitleScene(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        /// <summary>
        /// Raised when the scene is completed.
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// Defines the states for the title screens
        /// </summary>
        public enum State
        {
            /// <summary>
            /// Text is fading in.
            /// </summary>
            FadingIn,

            /// <summary>
            /// Text is fading out.
            /// </summary>
            FadingOut,

            /// <summary>
            /// Text is not fading.
            /// </summary>
            NotFading
        }

        /// <summary>
        /// Destroyes the game object.
        /// </summary>
        public void Destroy()
        {
        }

        /// <summary>
        /// Draws the scene with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            this.graphicsDevice.Clear(Color.Black);
            var textSize = this.font.MeasureString(this.text);

            this.spriteBatch.Begin(blendState: BlendState.NonPremultiplied);
            this.spriteBatch.DrawString(
                this.font,
                this.text,
                new Vector2((this.graphicsDevice.Viewport.Width / 2f) - (textSize.X / 2f), (this.graphicsDevice.Viewport.Height / 2f) - (textSize.Y / 2f)),
                new Color(Color.Blue, this.alpha));
            this.spriteBatch.End();
        }

        /// <summary>
        /// Initializes the game object.
        /// </summary>
        public void Initialize()
        {
            this.spriteBatch = new SpriteBatchAdapter(new SpriteBatch(this.graphicsDevice));
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public void LoadContent(IContentManagerAdapter contentManager)
        {
            this.font = contentManager.Load<SpriteFont>("Fonts/PlayerName");
        }

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            switch (this.state)
            {
                case State.FadingIn:
                    if (this.alpha < 255)
                    {
                        this.alpha += (int)(gameTime.ElapsedGameTime.TotalSeconds * 60f);
                    }
                    else
                    {
                        this.state = State.NotFading;
                        this.notFadingStartTime = gameTime.TotalGameTime;
                    }

                    break;

                case State.NotFading:
                    if (gameTime.TotalGameTime - this.notFadingStartTime > TimeSpan.FromSeconds(2))
                    {
                        this.state = State.FadingOut;
                        this.alpha = 255;
                    }

                    break;

                case State.FadingOut:
                    if (this.alpha > 0)
                    {
                        this.alpha -= (int)(gameTime.ElapsedGameTime.TotalSeconds * 60f);
                    }
                    else
                    {
                        if (this.text == "My Shooter Game")
                        {
                            this.text = "J&J Studios";
                            this.alpha = 0;
                            this.state = State.FadingIn;
                        }
                        else
                        {
                            this.OnCompleted();
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// Raises the <see cref="Completed" /> event.
        /// </summary>
        protected virtual void OnCompleted()
        {
            this.Completed?.Invoke(this, EventArgs.Empty);
        }
    }
}