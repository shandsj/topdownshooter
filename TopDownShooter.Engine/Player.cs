// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Adapters;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Simple player class.
    /// </summary>
    public class Player : GameObject
    {
        private AnimationComponent animationComponent;

        private readonly IColliderComponent colliderComponent;

        private readonly ICollisionSystem collisionSystem;

        private IContentManagerAdapter contentManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player" /> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="position">The position of the player.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="components">A collection of components.</param>
        public Player(int id, Vector2 position, ICollisionSystem collisionSystem, IEnumerable<IComponent> components)
            : base(id, components)
        {
            this.Health = 1;

            this.Position = position;
            this.collisionSystem = collisionSystem;
            this.animationComponent = this.Components.OfType<AnimationComponent>().FirstOrDefault();
            this.colliderComponent = this.Components.OfType<IColliderComponent>().FirstOrDefault();

            this.collisionSystem.Register(id, this, this.colliderComponent);
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public override void LoadContent(IContentManagerAdapter contentManager)
        {
            base.LoadContent(contentManager);

            this.contentManager = contentManager;
        }

        private bool hasCreatedDeathAnimation = false;

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Health == 0 && !this.hasCreatedDeathAnimation)
            {
                this.hasCreatedDeathAnimation = true;
                this.Components.Clear();

                this.animationComponent = new AnimationComponent("hoodieguyOnFire", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true, IsAnimating = true };
                this.animationComponent.Initialize();
                this.animationComponent.LoadContent(this.contentManager);
                this.Components.Add(this.animationComponent);
            }
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets the bounds offset via the size of the <see cref="AnimationComponent.FrameProperties"/>
        /// </summary>
        public override Rectangle Bounds => new Rectangle((int)this.Position.X - (int)(this.Width / 2), (int)this.Position.Y - (int)(this.Height / 2), this.Width, this.Height);

        /// <summary>
        /// Gets the projected bounds, based off the <see cref="IGameObject.Position"/> and the <see cref="IGameObject.Velocity"/>.
        /// </summary>
        public override Rectangle ProjectedBounds => new Rectangle((int)this.ProjectedPosition.X - (int)(this.Width / 2), (int)this.ProjectedPosition.Y - (int)(this.Height / 2), this.Width, this.Height);

        /// <summary>
        /// Gets the width of the game object.
        /// </summary>
        public override int Width => this.animationComponent.FrameProperties.Width;

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public override int Height => this.animationComponent.FrameProperties.Height;
    }
}