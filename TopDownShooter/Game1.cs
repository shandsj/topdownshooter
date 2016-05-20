// <copyright file="Game1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using TopDownShooter.Engine;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private Animation animation;

        private GraphicsDeviceManager graphics;

        private InputHerder m_InputHerder;

        private Player m_SimplePlayer;

        private ISpriteBatchAdapter spriteBatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game1"/> class.
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

            this.spriteBatch.Begin();

            // TODO: Add your drawing code here
            this.m_SimplePlayer.Draw(this.spriteBatch, gameTime);
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
            // TODO: Add your initialization logic here
            this.m_InputHerder = new InputHerder(Keyboard.GetState(), Mouse.GetState(), GamePad.GetState(PlayerIndex.One));

            this.animation = new Animation("hoodieguy", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true };

            this.m_SimplePlayer = new Player();
            this.m_SimplePlayer.Initialize(this.animation, new Vector2(this.GraphicsDevice.Viewport.Width / 2f, this.GraphicsDevice.Viewport.Height / 2f));
            this.m_SimplePlayer.IsMoving = true;

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
            this.m_InputHerder.Update(Keyboard.GetState(), Mouse.GetState(), GamePad.GetState(PlayerIndex.One));
            if (this.m_InputHerder.ShouldQuit())
            {
                this.Exit();
            }

            this.m_SimplePlayer.Update(gameTime);

            // TODO: Add your update logic here
            // this.animation.Position = new Vector2(this.GraphicsDevice.Viewport.Width / 2f, this.GraphicsDevice.Viewport.Height / 2f);
            // this.animation.IsAnimating = true;
            // this.animation.Update(gameTime);
            base.Update(gameTime);
        }
    }
}