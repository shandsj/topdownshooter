// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TitleScene.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoGame.Extended;
    using MonoGame.Extended.Shapes;
    using TiledSharp;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Levels;

    /// <summary>
    /// Defines a title scene.
    /// </summary>
    public class TitleScene : IScene, IProgress<int>
    {
        private const float LogoScale = .70f;

        private const float PlayButtonScale = .25f;

        private const float ProgressBarScale = .15f;

        private readonly GraphicsDevice graphicsDevice;

        private readonly IMouseAdapter mouse;

        private ICamera2DAdapter camera2DAdapter;

        private RectangleF progressBarRectangle;

        private SpriteFont font;

        private bool isLoaded;

        private Level background;

        private int loadProgress;

        private Vector2 logoPosition;

        private Texture2D logoTexture;

        private Vector2 playButtonPosition;

        private Texture2D playButtonTexture;

        private Texture2D progressBarTexture;

        private Vector2 progressBarPosition;

        private ISpriteBatchAdapter spriteBatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleScene" /> class.
        /// </summary>
        /// <param name="graphicsDevice">The <see cref="GraphicsDevice" />.</param>
        public TitleScene(GraphicsDevice graphicsDevice)
            : this(graphicsDevice, new MouseAdapter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleScene" /> class.
        /// </summary>
        /// <param name="graphicsDevice">The <see cref="GraphicsDevice" />.</param>
        /// <param name="mouse">The <see cref="IMouseAdapter" />.</param>
        internal TitleScene(GraphicsDevice graphicsDevice, IMouseAdapter mouse)
        {
            this.graphicsDevice = graphicsDevice;
            this.camera2DAdapter = new Camera2DAdapter(new OrthographicCamera(this.graphicsDevice) { Zoom = .5f });
            this.mouse = mouse;
        }

        /// <summary>
        /// Raised when the scene is completed.
        /// </summary>
        public event EventHandler<CompletedEventArgs> Completed;

        /// <summary>
        /// Destroyes the game object.
        /// </summary>
        public void Destroy()
        {
            this.background.Destroy();
        }

        /// <summary>
        /// Draws the scene with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            this.graphicsDevice.Clear(Color.Black);

            this.spriteBatch.Begin();
            this.background.Draw(this.camera2DAdapter, this.spriteBatch, gameTime);

            if (this.isLoaded)
            {
                this.spriteBatch.Draw(this.playButtonTexture, this.playButtonPosition, null, Color.White, 0f, new Vector2(0, 0), PlayButtonScale, SpriteEffects.None, 0f);
            }
            else
            {
                this.spriteBatch.Draw(this.progressBarTexture, this.progressBarPosition, null, Color.White, 0f, new Vector2(0, 0), ProgressBarScale, SpriteEffects.None, 0f);
                this.spriteBatch.FillRectangle(this.progressBarRectangle, new Color(60, 115, 202));
            }

            this.spriteBatch.Draw(this.logoTexture, this.logoPosition, null, Color.White, 0f, new Vector2(0, 0), LogoScale, SpriteEffects.None, 0f);
            this.spriteBatch.End();
        }

        /// <summary>
        /// Initializes the game object.
        /// </summary>
        public void Initialize()
        {
            this.spriteBatch = new SpriteBatchAdapter(new SpriteBatch(this.graphicsDevice));

            this.background = new Level(CollisionSystem.NextGameObjectId++, new CollisionSystem(), new TmxMapAdapter(new TmxMap("Content/TmxFiles/TitleScene.tmx")));
            this.background.Initialize();
        }

        /// <summary>
        /// Asynchronously oads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        /// <param name="progress">The <see cref="IProgress{Int32}"/> to report progress.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task LoadContentAsync(IContentManagerAdapter contentManager, IProgress<int> progress)
        {
            this.playButtonTexture = contentManager.Load<Texture2D>("UI/BluePlayButton");
            this.progressBarTexture = contentManager.Load<Texture2D>("UI/LoadingProgressBar");
            this.logoTexture = contentManager.Load<Texture2D>("UI/SingleShot");

            // Scale everything off the view port. Put the play button in the center of the screen,
            //  but down about 30% to make room for the title image and the user name textbox
            var x = (this.spriteBatch.GraphicsDevice.Viewport.Width - (this.playButtonTexture.Width * PlayButtonScale)) / 2f;
            var y = ((this.spriteBatch.GraphicsDevice.Viewport.Height - (this.playButtonTexture.Height * PlayButtonScale)) / 2f) +
                    (this.spriteBatch.GraphicsDevice.Viewport.Height * .30f);
            this.playButtonPosition = new Vector2(x, y);

            // Scale everything off the view port. Put the play button in the center of the screen,
            //  but down about 30% to make room for the title image and the user name textbox
            x = (this.spriteBatch.GraphicsDevice.Viewport.Width - (this.progressBarTexture.Width * ProgressBarScale)) / 2f;
            y = ((this.spriteBatch.GraphicsDevice.Viewport.Height - (this.progressBarTexture.Height * ProgressBarScale)) / 2f) +
                    (this.spriteBatch.GraphicsDevice.Viewport.Height * .30f);
            this.progressBarPosition = new Vector2(x, y);

            // Scale everything off the view port. Put the play button in the center of the screen,
            //  but down about 5% to make room for the title image and the user name textbox
            x = (this.spriteBatch.GraphicsDevice.Viewport.Width - (this.logoTexture.Width * LogoScale)) / 2f;
            y = ((this.spriteBatch.GraphicsDevice.Viewport.Height - (this.logoTexture.Height * LogoScale)) / 2f) -
                (this.spriteBatch.GraphicsDevice.Viewport.Height * .08f);
            this.logoPosition = new Vector2(x, y);

            this.font = contentManager.Load<SpriteFont>("Fonts/PlayerName");

            this.background.LoadContent(contentManager);

            return Task.Delay(0);
        }

        /// <summary>
        /// Reports a progress update.
        /// </summary>
        /// <param name="value">The value of the updated progress.</param>
        public void Report(int value)
        {
            var maxWidth = this.spriteBatch.GraphicsDevice.Viewport.Width * .8f;
            var width = maxWidth * (value / 100f);
            var height = this.spriteBatch.GraphicsDevice.Viewport.Height * .05f;

            // Scale everything off the view port. Put the play button in the center of the screen,
            //  but down about 30% to make room for the title image and the user name textbox
            var x = (this.spriteBatch.GraphicsDevice.Viewport.Width - (maxWidth * 1f)) / 2f;
            var y = ((this.spriteBatch.GraphicsDevice.Viewport.Height - (height * 1f)) / 2f) +
                    (this.spriteBatch.GraphicsDevice.Viewport.Height * .36f);

            var rectangle = new RectangleF(x, y, width, height);

            this.loadProgress = value;
            this.progressBarRectangle = rectangle;

            if (this.loadProgress == 100)
            {
                this.isLoaded = true;
            }
        }

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            var mouseState = this.mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                // TODO: Draw a mouse cursor and enable this
                ////if (this.playButtonTexture.Bounds.Contains(mouseState.Position))
                {
                    this.OnCompleted(new CompletedEventArgs());
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="Completed" /> event.
        /// </summary>
        /// <param name="args">A <see cref="CompletedEventArgs"/> that contains the event data.</param>
        protected virtual void OnCompleted(CompletedEventArgs args)
        {
            this.Completed?.Invoke(this, args);
        }
    }
}