﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Levels;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private IContentManagerAdapter contentManager;

        private GraphicsDeviceManager graphics;

        private ISceneController sceneController;

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
            this.sceneController.Draw(gameTime);
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
            this.contentManager = new ContentManagerAdapter(this.Content);
            this.sceneController = new SceneController(this.contentManager);

            var titleScene = new TitleScene(this.GraphicsDevice);
            titleScene.Completed += (s, e) => this.sceneController.Switch(new ArenaScene(this.GraphicsDevice, (Level)e.Data));
            this.sceneController.Switch(titleScene);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            this.Content.Unload();
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

            this.sceneController.Update(gameTime);
            base.Update(gameTime);
        }
    }
}