﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArenaScene.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGame.Extended;
    using TiledSharp;
    using TopDownShooter.Controllers;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Levels;
    using TopDownShooter.Inventory;
    using TopDownShooter.Items;
    using TopDownShooter.Messages;

    /// <summary>
    /// Defines the scene for the arena
    /// </summary>
    public class ArenaScene : IScene
    {
        private readonly ICollisionSystem collisionSystem;

        private readonly GameItemFactory gameItemFactory;

        private readonly GraphicsDevice graphicsDevice;

        private readonly Random random = new Random((int)DateTime.Now.Ticks);

        private ICamera2DAdapter camera2DAdapter;

        private IContentManagerAdapter contentManager;

        private Player focusedPlayer;

        private List<IGameItem> gameItems;

        private LeaderBoard leaderBoard;

        private Level level;

        private List<Player> players;

        private ISpriteBatchAdapter screenSpriteBatch;

        private ISpriteBatchAdapter worldSpriteBatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArenaScene" /> class.
        /// </summary>
        /// <param name="graphicsDevice">The <see cref="GraphicsDevice" />.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        public ArenaScene(GraphicsDevice graphicsDevice, ICollisionSystem collisionSystem)
        {
            this.graphicsDevice = graphicsDevice;
            this.collisionSystem = collisionSystem;
            this.gameItemFactory = new GameItemFactory();
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
            this.OnCompleted(new CompletedEventArgs());
            //// TODO: destroy all game objects.
        }

        /// <summary>
        /// Draws the scene with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            this.worldSpriteBatch.Begin(transformMatrix: this.camera2DAdapter.GetViewMatrix());
            this.level.Draw(this.camera2DAdapter, this.worldSpriteBatch, gameTime);
            this.gameItems.ForEach(o => o.Draw(this.camera2DAdapter, this.worldSpriteBatch, gameTime));
            this.players.ForEach(o => o.Draw(this.camera2DAdapter, this.worldSpriteBatch, gameTime));
            this.worldSpriteBatch.End();

            this.screenSpriteBatch.Begin();
            this.leaderBoard.Draw(this.camera2DAdapter, this.screenSpriteBatch, gameTime);
            this.screenSpriteBatch.End();
        }

        /// <summary>
        /// Initializes the game object.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Asynchronously oads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        /// <param name="progress">The <see cref="IProgress{Int32}" /> to report progress.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public Task LoadContentAsync(IContentManagerAdapter contentManager, IProgress<int> progress)
        {
            this.contentManager = contentManager;

            return Task.Run(async () =>
                {
                    this.level = new Level(CollisionSystem.NextGameObjectId++, this.collisionSystem, new TmxMapAdapter(new TmxMap("Content/TmxFiles/DefaultLevel.tmx")));
                    this.level.Initialize();

                    this.players = new List<Player>();
                    this.gameItems = new List<IGameItem>();

                    int minimumSpawnLocation = -20000;
                    int maximumSpawnLocation = 20000;
                    this.gameItems.AddRange(this.gameItemFactory.SpawnRandomCoinItems(1000, this.collisionSystem, minimumSpawnLocation, maximumSpawnLocation, minimumSpawnLocation, maximumSpawnLocation, false));
                    this.gameItems.AddRange(this.gameItemFactory.SpawnRandomBulletItems(100, this.collisionSystem, minimumSpawnLocation, maximumSpawnLocation, minimumSpawnLocation, maximumSpawnLocation));

                    this.camera2DAdapter = new Camera2DAdapter(new OrthographicCamera(this.graphicsDevice) { Zoom = .5f });
                    this.leaderBoard = new LeaderBoard(CollisionSystem.NextGameObjectId++);
                    this.leaderBoard.Initialize();

#pragma warning disable SA1118 // Parameter must not span multiple lines
                    var focusedPlayerId = CollisionSystem.NextGameObjectId++;
                    this.focusedPlayer = new Player(
                        focusedPlayerId,
                        ////new Vector2(this.random.Next(minimumSpawnLocation, maximumSpawnLocation), this.random.Next(minimumSpawnLocation, maximumSpawnLocation)),
                        new Vector2(0, 0),
                        this.collisionSystem,
                        new IComponent[]
                        {
                            // Order matters for calls to update and draw
                            new HumanInputControllerComponent(this.camera2DAdapter),
                            new DashComponent(),
                            new PlayerColliderComponent(focusedPlayerId, this.collisionSystem),
                            new ParticleGeneratorComponent(this.random, new[] { "Particles/circle" }, new[] { Color.Orange, Color.OrangeRed, Color.MonoGameOrange }) { MinimumVelocity = new Vector2(-2, -2), MaximumVelocity = new Vector2(2, 2) },
                            new AnimationComponentManager(
                                new AnimationComponent("Dash", "hoodieguy", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true },
                                new AnimationComponent("Stand", "SpriteSheets/PlayerStanding", new FrameProperties(360, 250, TimeSpan.MaxValue, 1)) { IsRendered = true },
                                new AnimationComponent("Walk", "SpriteSheets/PlayerWalking", new FrameProperties(355, 250, TimeSpan.FromSeconds(.05), 15)) { IsRendered = true },
                                new AnimationComponent("Death", "hoodieguyOnFire", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true }),
                            new PlayerInventoryComponent(this.collisionSystem),
                            new DebugComponent(Color.Red, 2)
                        });

                    this.focusedPlayer.Name = $"Player {focusedPlayerId}";
                    this.players.Add(this.focusedPlayer);

                    var playerCount = this.random.Next(300, 500);
                    for (int i = 0; i < playerCount; i++)
                    {
                        var id = CollisionSystem.NextGameObjectId++;
                        var player = new Player(
                            id,
                            new Vector2(this.random.Next(minimumSpawnLocation, maximumSpawnLocation), this.random.Next(minimumSpawnLocation, maximumSpawnLocation)),
                            this.collisionSystem,
                            new IComponent[]
                            {
                                new SimpleAiInputControllerComponent(this.random),
                                new DashComponent(),
                                new PlayerColliderComponent(id, this.collisionSystem),
                                new ParticleGeneratorComponent(this.random, new[] { "Particles/circle" }, new[] { Color.Orange, Color.OrangeRed, Color.MonoGameOrange }) { MinimumVelocity = new Vector2(-2, -2), MaximumVelocity = new Vector2(2, 2) },
                                new AnimationComponentManager(
                                     new AnimationComponent("Dash", "hoodieguy", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true },
                                    new AnimationComponent("Stand", "SpriteSheets/PlayerStanding", new FrameProperties(360, 250, TimeSpan.MaxValue, 1)) { IsRendered = true },
                                    new AnimationComponent("Walk", "SpriteSheets/PlayerWalking", new FrameProperties(355, 250, TimeSpan.FromSeconds(.05), 15)) { IsRendered = true },
                                    new AnimationComponent("Death", "hoodieguyOnFire", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true }),
                                new PlayerInventoryComponent(this.collisionSystem)
                            });
                        player.Name = $"Ai Player {id}";
                        this.players.Add(player);
                    }
#pragma warning restore SA1118

                    this.players.ForEach(player =>
                        {
                            player.Initialize();
                            player.MessageReady += this.GameObjectMessageReady;
                        });
                    this.gameItems.ForEach(item => item.Initialize());

                    // Create a new SpriteBatch, which can be used to draw textures.
                    this.worldSpriteBatch = new SpriteBatchAdapter(new SpriteBatch(this.graphicsDevice));
                    this.screenSpriteBatch = new SpriteBatchAdapter(new SpriteBatch(this.graphicsDevice));

                    this.players.ForEach(player => player.LoadContent(contentManager));
                    this.gameItems.ForEach(item => item.LoadContent(contentManager));
                    this.leaderBoard.LoadContent(contentManager);

                    await this.level.LoadContentAsync(contentManager, progress);
                    progress.Report(100);
                });
        }

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            this.collisionSystem.Update(gameTime);
            this.players.ForEach(o => o.Update(gameTime));
            this.gameItems.ForEach(o => o.Update(gameTime));

            this.camera2DAdapter.Position = this.focusedPlayer.Position;
            this.camera2DAdapter.LookAt(this.focusedPlayer.Position);
            this.leaderBoard.SetPlayers(this.players);
            this.gameItems.RemoveAll(o => o.IsPickedUp);
        }

        /// <summary>
        /// Reports a progress update.
        /// </summary>
        /// <param name="value">The value of the updated progress.</param>
        public void Report(int value)
        {
            // This scene does not preload any scene, so there is nothing to do here.
        }

        /// <summary>
        /// Raises the <see cref="Completed" /> event.
        /// </summary>
        /// <param name="args">A <see cref="CompletedEventArgs" /> that contains the event data.</param>
        protected virtual void OnCompleted(CompletedEventArgs args)
        {
            this.Completed?.Invoke(this, args);
        }

        /// <summary>
        /// Handles the <see cref="IGameObject.MessageReady" /> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="MessageEventArgs" /> that contains the event data.</param>
        private void GameObjectMessageReady(object sender, MessageEventArgs e)
        {
            if ((MessageType)e.Message.MessageType == MessageType.DropCoins)
            {
                const int Range = 200;
                var dropCoinsMessage = (DropCoinsMessage)e.Message;

                var coins = this.gameItemFactory.SpawnRandomCoinItems(
                    dropCoinsMessage.Count,
                    this.collisionSystem,
                    (int)dropCoinsMessage.Location.X - Range,
                    (int)dropCoinsMessage.Location.X + Range,
                    (int)dropCoinsMessage.Location.Y - Range,
                    (int)dropCoinsMessage.Location.Y + Range,
                    true);

                foreach (var coin in coins)
                {
                    coin.Initialize();
                    coin.LoadContent(this.contentManager);
                }

                this.gameItems.AddRange(coins);
            }
        }
    }
}