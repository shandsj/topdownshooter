// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectileGeneratorComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Projectiles
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using MonoGame.Extended;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Messages;

    /// <summary>
    /// Defines a component for generating bullet projectiles.
    /// </summary>
    public class ProjectileGeneratorComponent : IComponent
    {
        private readonly List<IGameObject> projectiles = new List<IGameObject>();

        private readonly ICollisionSystem collisionSystem;

        private readonly TimeSpan cooldownTime = TimeSpan.FromSeconds(.5);

        private readonly IGameObjectFactory factory;

        private IContentManagerAdapter contentManager;

        private bool isDestroyed;

        private DateTime lastFireTime = DateTime.Now;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileGeneratorComponent" /> class.
        /// </summary>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" /> to use for bullet projectiles.</param>
        public ProjectileGeneratorComponent(ICollisionSystem collisionSystem)
            : this(collisionSystem, new GameObjectFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileGeneratorComponent" /> class.
        /// </summary>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" /> to use for bullet projectiles.</param>
        /// <param name="factory">The <see cref="IGameObjectFactory" /> used to create bullet projectiles.</param>
        internal ProjectileGeneratorComponent(ICollisionSystem collisionSystem, IGameObjectFactory factory)
        {
            this.collisionSystem = collisionSystem;
            this.factory = factory;
        }

        /// <summary>
        /// Destroys the component.
        /// </summary>
        public void Destroy()
        {
            this.isDestroyed = true;

            foreach (var bullet in this.projectiles)
            {
                bullet.Destroy();
            }

            this.projectiles.Clear();
        }

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="camera">The <see cref="ICamera2DAdapter"/>.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        public void Draw(IGameObject gameObject, ICamera2DAdapter camera, ISpriteBatchAdapter spriteBatch, GameTime time)
        {
            foreach (var bullet in this.projectiles)
            {
                bullet.Draw(camera, spriteBatch, time);
            }
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public void LoadContent(IContentManagerAdapter contentManager)
        {
            this.contentManager = contentManager;
        }

        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="message">The message object.</param>
        /// <param name="gameTime">The game time.</param>
        public void ReceiveMessage(IGameObject gameObject, Message message, GameTime gameTime)
        {
            if (this.isDestroyed)
            {
                return;
            }

            if ((MessageType)message.MessageType == MessageType.Fire && DateTime.Now - this.lastFireTime > this.cooldownTime)
            {
                IGameObject projectile = null;
                var fireMessage = (FireMessage)message;
                switch (fireMessage.ProjectileType)
                {
                    case ProjectileType.ShortRange:
                        projectile = this.factory.CreateShortRangeProjectile(
                            CollisionSystem.NextGameObjectId++,
                            gameObject.Id,
                            gameObject.Position,
                            new Vector2(0, -1).Rotate(gameObject.Rotation),
                            this.collisionSystem);
                        break;

                    case ProjectileType.LongRange:
                        projectile = this.factory.CreateLongRangeProjectile(
                            CollisionSystem.NextGameObjectId++,
                            gameObject.Id,
                            gameObject.Position,
                            new Vector2(0, -1).Rotate(gameObject.Rotation),
                            this.collisionSystem);
                        break;
                }

                if (projectile != null)
                {
                    projectile.Initialize();
                    projectile.LoadContent(this.contentManager);
                    this.projectiles.Add(projectile);
                    this.lastFireTime = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="gameTime">The game time.</param>
        public void Update(IGameObject gameObject, GameTime gameTime)
        {
            foreach (var projectile in this.projectiles)
            {
                projectile.Update(gameTime);
            }
        }
    }
}