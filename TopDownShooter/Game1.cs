// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using TiledSharp;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Controllers;
    using TopDownShooter.Engine.Levels;
    using TopDownShooter.Engine.Projectiles;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
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
        private ICollisionSystem collisionSystem;
        private Player focusedPlayer;
        private List<Player> players;

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
            this.collisionSystem = new CollisionSystem();
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

            this.players.ForEach(o => o.Draw(this.spriteBatch, gameTime));

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
            this.players = new List<Player>();

            this.camera = new Camera(this.GraphicsDevice.Viewport) { Zoom = .5f };
            this.level = new Level(CollisionSystem.NextGameObjectId++, this.collisionSystem, new TmxMap("Content/TmxFiles/DefaultLevel.tmx"));

#pragma warning disable SA1118 // Parameter must not span multiple lines
            var focusedPlayerId = CollisionSystem.NextGameObjectId++;
            this.focusedPlayer = new Player(
                focusedPlayerId,
                new Vector2(1600, 1600),
                this.collisionSystem,
                new IComponent[]
                {
                    // Order matters for calls to update and draw
                    new HumanInputControllerComponent(),
                    new BulletProjectileGeneratorComponent(this.collisionSystem),
                    new PlayerColliderComponent(focusedPlayerId, this.collisionSystem),
                    new MovementComponent(),
                    new AnimationComponent("hoodieguy", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true, IsAnimating = true },
                    new BulletProjectileGeneratorComponent(this.collisionSystem),
                    new DebugComponent(Color.Red, 2)
                });
            this.focusedPlayer.Name = $"Player {focusedPlayerId}";
            this.players.Add(this.focusedPlayer);

            int spawn = 1600;

            // TODO: Uncomment when wall collision logic is finished
            for (int i = 0; i < 3; i++)
            {
                spawn -= 200;
                var id = CollisionSystem.NextGameObjectId++;
                var player = new Player(
                    id,
                    new Vector2(spawn, spawn),
                    this.collisionSystem,
                    new IComponent[]
                    {
                    new SimpleAiInputControllerComponent(),
                    new PlayerColliderComponent(id, this.collisionSystem),
                    new MovementComponent(),
                    new AnimationComponent("hoodieguy", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true, IsAnimating = true },

                    // They were pooping them every where!
                    new BulletProjectileGeneratorComponent(this.collisionSystem),
                    new DebugComponent(Color.Blue, 2)
                    });
                player.Name = $"Ai Player {id}";
                this.players.Add(player);
            }
#pragma warning restore SA1118

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
            this.players.ForEach(o => o.LoadContent(this.contentManager));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            this.level.UnloadContent(this.contentManager);
            this.players.ForEach(o => o.UnloadContent(this.contentManager));
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

            this.players.ForEach(o => o.Update(gameTime));
            this.camera.Position = this.focusedPlayer.Position;

            base.Update(gameTime);
        }
    }
}