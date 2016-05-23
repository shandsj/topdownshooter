// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine;

    /// <summary>
    /// Simple player class.
    /// </summary>
    public class Player : GameObject
    {
        private readonly AnimationComponent animationComponent;

        private readonly ColliderComponentBase colliderComponent;

        private readonly ICollisionSystem collisionSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player" /> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="position">The position of the player.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="animationComponent">The <see cref="AnimationComponent" />.</param>
        /// <param name="colliderComponent">The <see cref="ColliderComponentBase" />.</param>
        /// <param name="components">A collection of components.</param>
        public Player(int id, Vector2 position, ICollisionSystem collisionSystem, AnimationComponent animationComponent, ColliderComponentBase colliderComponent, IEnumerable<IComponent> components)
            : base(id)
        {
            this.Position = position;
            this.collisionSystem = collisionSystem;
            this.animationComponent = animationComponent;
            this.colliderComponent = colliderComponent;

            this.collisionSystem.Register(id, this, this.colliderComponent);
            foreach (var component in components)
            {
                this.Components.Add(component);
            }
        }

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