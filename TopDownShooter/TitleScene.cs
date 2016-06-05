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
    using Microsoft.Xna.Framework.Input;
    using MonoGame.Extended;
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

        private readonly GraphicsDevice graphicsDevice;

        private readonly IMouseAdapter mouse;

        private ICamera2DAdapter camera2DAdapter;

        private SpriteFont font;

        private bool isLoaded;

        private Level level;

        private Level background;

        private int loadProgress;

        private Vector2 logoPosition;

        private Texture2D logoTexture;

        private Vector2 playButtonPosition;

        private Texture2D playButtonTexture;

        private ISpriteBatchAdapter spriteBatch;

        private ICollisionSystem collisionSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleScene" /> class.
        /// </summary>
        /// <param name="graphicsDevice">The <see cref="GraphicsDevice" />.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem"/>.</param>
        public TitleScene(GraphicsDevice graphicsDevice, ICollisionSystem collisionSystem)
            : this(graphicsDevice, collisionSystem, new MouseAdapter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleScene" /> class.
        /// </summary>
        /// <param name="graphicsDevice">The <see cref="GraphicsDevice" />.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem"/>.</param>
        /// <param name="mouse">The <see cref="IMouseAdapter" />.</param>
        internal TitleScene(GraphicsDevice graphicsDevice, ICollisionSystem collisionSystem, IMouseAdapter mouse)
        {
            this.graphicsDevice = graphicsDevice;
            this.collisionSystem = collisionSystem;
            this.camera2DAdapter = new Camera2DAdapter(new Camera2D(this.graphicsDevice) { Zoom = .5f });
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
                this.spriteBatch.DrawString(
                    this.font,
                    this.loadProgress.ToString(),
                    this.playButtonPosition,
                    Color.White);
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

            this.background = new Level(CollisionSystem.NextGameObjectId++, this.collisionSystem, new TmxMapAdapter(new TmxMap("Content/TmxFiles/TitleScene.tmx")));
            this.background.Initialize();

            this.level = new Level(CollisionSystem.NextGameObjectId++, this.collisionSystem, new TmxMapAdapter(new TmxMap("Content/TmxFiles/DefaultLevel.tmx")));
            this.level.Initialize();
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public void LoadContent(IContentManagerAdapter contentManager)
        {
            this.playButtonTexture = contentManager.Load<Texture2D>("UI/BluePlayButton");
            this.logoTexture = contentManager.Load<Texture2D>("UI/SingleShot");

            // Scale everything off the view port. Put the play button in the center of the screen,
            //  but down about 30% to make room for the title image and the user name textbox
            var x = (this.spriteBatch.GraphicsDevice.Viewport.Width - (this.playButtonTexture.Width * PlayButtonScale)) / 2f;
            var y = ((this.spriteBatch.GraphicsDevice.Viewport.Height - (this.playButtonTexture.Height * PlayButtonScale)) / 2f) +
                    (this.spriteBatch.GraphicsDevice.Viewport.Height * .30f);
            this.playButtonPosition = new Vector2(x, y);

            // Scale everything off the view port. Put the play button in the center of the screen,
            //  but down about 5% to make room for the title image and the user name textbox
            x = (this.spriteBatch.GraphicsDevice.Viewport.Width - (this.logoTexture.Width * LogoScale)) / 2f;
            y = ((this.spriteBatch.GraphicsDevice.Viewport.Height - (this.logoTexture.Height * LogoScale)) / 2f) -
                (this.spriteBatch.GraphicsDevice.Viewport.Height * .08f);
            this.logoPosition = new Vector2(x, y);

            this.font = contentManager.Load<SpriteFont>("Fonts/PlayerName");

            this.background.LoadContent(contentManager);
            this.level.LoadContentAsync(contentManager, this).ContinueWith(a => { this.isLoaded = true; });
        }

        /// <summary>
        /// Reports a progress update.
        /// </summary>
        /// <param name="value">The value of the updated progress.</param>
        public void Report(int value)
        {
            this.loadProgress = value;
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
                    this.OnCompleted(new CompletedEventArgs(this.level));
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