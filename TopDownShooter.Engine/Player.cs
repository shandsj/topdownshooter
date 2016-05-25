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
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Simple player class.
    /// </summary>
    public class Player : GameObject
    {
        private readonly IColliderComponent colliderComponent;

        private readonly ICollisionSystem collisionSystem;

        private IAnimationComponent walkAnimationComponent;
        private IAnimationComponent deathAnimationComponent;

        private bool hasCreatedDeathAnimation;

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
            this.walkAnimationComponent = this.Components.OfType<IAnimationComponent>().FirstOrDefault((a) => a.Label == "Walk");
            this.deathAnimationComponent = this.Components.OfType<IAnimationComponent>().FirstOrDefault((a) => a.Label == "Death");
            this.colliderComponent = this.Components.OfType<IColliderComponent>().FirstOrDefault();

            this.collisionSystem.Register(id, this, this.colliderComponent);
        }

        /// <summary>
        /// Gets the bounds offset via the size of the <see cref="AnimationComponent.FrameProperties" />
        /// </summary>
        public override Rectangle Bounds => new Rectangle((int)this.Position.X - (this.Width / 2), (int)this.Position.Y - (this.Height / 2), this.Width, this.Height);

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public override int Height => this.walkAnimationComponent.FrameProperties.Height;

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the projected bounds, based off the <see cref="IGameObject.Position" /> and the <see cref="IGameObject.Velocity" />.
        /// </summary>
        public override Rectangle ProjectedBounds => new Rectangle((int)this.ProjectedPosition.X - (this.Width / 2), (int)this.ProjectedPosition.Y - (this.Height / 2), this.Width, this.Height);

        /// <summary>
        /// Gets the width of the game object.
        /// </summary>
        public override int Width => this.walkAnimationComponent.FrameProperties.Width;

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
                this.deathAnimationComponent.IsAnimating = true;
                this.deathAnimationComponent.IsRendered = true;
                this.Components.Clear();
                this.Components.Add(this.deathAnimationComponent);
            }
        }
    }
}