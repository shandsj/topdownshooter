// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BulletProjectileGeneratorComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Projectiles
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Defines a component for generating bullet projectiles.
    /// </summary>
    public class BulletProjectileGeneratorComponent : IComponent
    {
        private readonly List<IGameObject> bullets = new List<IGameObject>();

        private readonly ICollisionSystem collisionSystem;

        private readonly TimeSpan cooldownTime = TimeSpan.FromSeconds(.5);

        private readonly IGameObjectFactory factory;

        private IContentManagerAdapter contentManager;

        private bool isDestroyed;

        private DateTime lastFireTime = DateTime.Now;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletProjectileGeneratorComponent" /> class.
        /// </summary>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" /> to use for bullet projectiles.</param>
        public BulletProjectileGeneratorComponent(ICollisionSystem collisionSystem)
            : this(collisionSystem, new GameObjectFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletProjectileGeneratorComponent" /> class.
        /// </summary>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" /> to use for bullet projectiles.</param>
        /// <param name="factory">The <see cref="IGameObjectFactory" /> used to create bullet projectiles.</param>
        internal BulletProjectileGeneratorComponent(ICollisionSystem collisionSystem, IGameObjectFactory factory)
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

            foreach (var bullet in this.bullets)
            {
                bullet.Destroy();
            }

            this.bullets.Clear();
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
            foreach (var bullet in this.bullets)
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
        public void ReceiveMessage(IGameObject gameObject, ComponentMessage message)
        {
            if (this.isDestroyed)
            {
                return;
            }

            if (message.MessageType == MessageType.Fire && DateTime.Now - this.lastFireTime > this.cooldownTime)
            {
                var bullet = this.factory.CreateBulletProjectile(CollisionSystem.NextGameObjectId++, gameObject.Id, gameObject.Position, gameObject.Velocity, this.collisionSystem);
                bullet.Initialize();
                bullet.LoadContent(this.contentManager);

                this.bullets.Add(bullet);

                this.lastFireTime = DateTime.Now;
            }
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="time">The game time.</param>
        public void Update(IGameObject gameObject, GameTime time)
        {
            foreach (var bullet in this.bullets)
            {
                bullet.Update(time);
            }
        }
    }
}