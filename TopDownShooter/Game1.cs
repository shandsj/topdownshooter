// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using TiledSharp;
    using TopDownShooter.Engine;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        /// <summary>
        /// A simple test animation
        /// </summary>
        private Animation animation;

        /// <summary>
        /// The <see cref="ICamera"/>.
        /// </summary>
        private ICamera camera;

        /// <summary>
        /// The <see cref="IContentManagerAdapter" />.
        /// </summary>
        private IContentManagerAdapter contentManager;

        /// <summary>
        /// The <see cref="GraphicsDeviceManager" />.
        /// </summary>
        private GraphicsDeviceManager graphics;

        /// <summary>
        /// The <see cref="Level" />.
        /// </summary>
        private Level level;

        private IInputController inputController;

        private Player simplePlayer;

        /// <summary>
        /// The default <see cref="ISpriteBatchAdapter" />.
        /// </summary>
        private ISpriteBatchAdapter spriteBatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game1" /> class.
        /// </summary>
        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin(transformMatrix: this.camera.TransformMatrix);

            this.level.Draw(this.spriteBatch, gameTime);

            // TODO: Add your drawing code here
            this.simplePlayer.Draw(this.spriteBatch, gameTime);
            this.animation.Draw(this.spriteBatch, gameTime);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.camera = new Camera(this.GraphicsDevice.Viewport);
            this.level = new Level(new TmxMap("Content/TmxFiles/DefaultLevel.tmx"));

            // this.inputController = new HumanInputController();
            this.inputController = new SimpleAiInputController();

            this.animation = new Animation("hoodieguy", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true };

            this.simplePlayer = new Player(this.inputController);
            this.simplePlayer.Initialize(this.animation, new Vector2(1600, 1600)); // World Coordinates
            this.simplePlayer.IsMoving = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatchAdapter(new SpriteBatch(this.GraphicsDevice));
            this.contentManager = new ContentManagerAdapter(this.Content);

            this.level.LoadContent(this.contentManager);

            // TODO: use this.Content to load your game content here
            this.animation.LoadContent(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            this.animation.UnloadContent(this.Content);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var gamePadState = GamePad.GetState(PlayerIndex.One);

            if (keyboardState.IsKeyDown(Keys.Escape) || gamePadState.Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            this.inputController.Update(gameTime);

            this.simplePlayer.Update(gameTime);
            this.camera.Position = this.simplePlayer.Position;

            // TODO: Add your update logic here
            // this.animation.Position = new Vector2(this.GraphicsDevice.Viewport.Width / 2f, this.GraphicsDevice.Viewport.Height / 2f);
            // this.animation.IsAnimating = true;
            // this.animation.Update(gameTime);
            base.Update(gameTime);
        }
    }
}