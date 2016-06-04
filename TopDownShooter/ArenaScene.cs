// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArenaScene.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using TiledSharp;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Controllers;
    using TopDownShooter.Engine.Levels;
    using TopDownShooter.Engine.Projectiles;

    /// <summary>
    /// Defines the scene for the arena
    /// </summary>
    public class ArenaScene : IScene
    {
        private readonly GraphicsDevice graphicsDevice;

        private readonly Random random = new Random((int)DateTime.Now.Ticks);

        private ICamera camera;

        private ICollisionSystem collisionSystem;

        private Player focusedPlayer;

        private LeaderBoard leaderBoard;

        private Level level;

        private List<Player> players;

        // Items for pickup. This probably needs to go into
        // some type of world state manager or something.
        private List<IGameObject> gameItems;

        private ISpriteBatchAdapter screenSpriteBatch;

        private ISpriteBatchAdapter worldSpriteBatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArenaScene"/> class.
        /// </summary>
        /// <param name="graphicsDevice">The <see cref="GraphicsDevice"/>.</param>
        public ArenaScene(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        /// <summary>
        /// Raised when the scene is completed.
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// Destroyes the game object.
        /// </summary>
        public void Destroy()
        {
            this.OnCompleted();
            //// TODO: destroy all game objects.
        }

        /// <summary>
        /// Draws the scene with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            this.worldSpriteBatch.Begin(transformMatrix: this.camera.TransformMatrix);
            this.level.Draw(this.camera, this.worldSpriteBatch, gameTime);
            this.players.ForEach(o => o.Draw(this.camera, this.worldSpriteBatch, gameTime));
            this.gameItems.ForEach(o => o.Draw(this.camera, this.worldSpriteBatch, gameTime));
            this.worldSpriteBatch.End();

            this.screenSpriteBatch.Begin();
            this.leaderBoard.Draw(this.camera, this.screenSpriteBatch, gameTime);
            this.screenSpriteBatch.End();
        }

        /// <summary>
        /// Initializes the game object.
        /// </summary>
        public void Initialize()
        {
            this.collisionSystem = new CollisionSystem();

            this.players = new List<Player>();
            this.gameItems = new List<IGameObject>();
            this.gameItems.AddRange(new GameObjectFactory().SpawnRandomBulletItems(10, this.collisionSystem, 100, 1500, 100, 1500));

            this.camera = new Camera(this.graphicsDevice.Viewport) { Zoom = .5f };
            this.level = new Level(CollisionSystem.NextGameObjectId++, this.collisionSystem, new TmxMapAdapter(new TmxMap("Content/TmxFiles/DefaultLevel.tmx")));
            this.leaderBoard = new LeaderBoard(CollisionSystem.NextGameObjectId++);
            this.leaderBoard.Initialize();

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
                    new PlayerColliderComponent(focusedPlayerId, this.collisionSystem),
                    new AnimationComponentManager(
                        new AnimationComponent("Walk", "hoodieguy", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true, IsAnimating = true, IsRendered = true },
                        new AnimationComponent("Death", "hoodieguyOnFire", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true }),
                    new PlayerInventoryComponent(this.collisionSystem)
                });

            this.focusedPlayer.Name = $"Player {focusedPlayerId}";
            this.players.Add(this.focusedPlayer);

            int spawn = 1600;

            // TODO: Uncomment when wall collision logic is finished
            for (int i = 0; i < 5; i++)
            {
                spawn -= 200;
                var id = CollisionSystem.NextGameObjectId++;
                var player = new Player(
                    id,
                    new Vector2(spawn, spawn),
                    this.collisionSystem,
                    new IComponent[]
                    {
                        new SimpleAiInputControllerComponent(this.random),
                        new PlayerColliderComponent(id, this.collisionSystem),
                        new AnimationComponentManager(
                            new AnimationComponent("Walk", "hoodieguy", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true, IsAnimating = true, IsRendered = true },
                            new AnimationComponent("Death", "hoodieguyOnFire", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true }),
                        new PlayerInventoryComponent(this.collisionSystem)
                    });
                player.Name = $"Ai Player {id}";
                this.players.Add(player);
            }
#pragma warning restore SA1118

            this.players.ForEach(player => player.Initialize());
            this.gameItems.ForEach(item => item.Initialize());
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public void LoadContent(IContentManagerAdapter contentManager)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.worldSpriteBatch = new SpriteBatchAdapter(new SpriteBatch(this.graphicsDevice));
            this.screenSpriteBatch = new SpriteBatchAdapter(new SpriteBatch(this.graphicsDevice));

            this.level.LoadContent(contentManager);
            this.players.ForEach(player => player.LoadContent(contentManager));
            this.gameItems.ForEach(item => item.LoadContent(contentManager));
            this.leaderBoard.LoadContent(contentManager);
        }

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            this.players.ForEach(o => o.Update(gameTime));
            this.gameItems.ForEach(o => o.Update(gameTime));
            this.camera.Position = this.focusedPlayer.Position;
            this.leaderBoard.SetPlayers(this.players);

            this.gameItems.RemoveAll(o => (o as IGameItem).IsPickedUp);
        }

        /// <summary>
        /// Raises the <see cref="Completed"/> event.
        /// </summary>
        protected virtual void OnCompleted()
        {
            this.Completed?.Invoke(this, EventArgs.Empty);
        }
    }
}