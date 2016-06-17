// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectileBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Projectiles
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Defines a base class for projectiles.
    /// </summary>
    public abstract class ProjectileBase : GameObject
    {
        private readonly IColliderComponent colliderComponent;

        private readonly ICollisionSystem collisionSystem;

        private readonly Vector2 initialPosition;

        private Vector2 direction;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileBase" /> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="components">The collection of components.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="direction">The direction of the bullet projectile, as a unit vector.</param>
        protected ProjectileBase(int id, Vector2 position, Vector2 direction, ICollisionSystem collisionSystem, IEnumerable<IComponent> components)
            : base(id, components)
        {
            this.Position = position;
            this.direction = direction;
            this.collisionSystem = collisionSystem;

            this.initialPosition = this.Position;
            this.colliderComponent = this.Components.OfType<IColliderComponent>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the maximum range of this projectile.
        /// </summary>
        public abstract int MaximumRange { get; }

        /// <summary>
        /// Gets the speed of this projectile.
        /// </summary>
        public abstract float Speed { get; }

        /// <summary>
        /// Destroyes the game object.
        /// </summary>
        public override void Destroy()
        {
            this.collisionSystem.Unregister(this.Id);

            base.Destroy();
        }

        /// <summary>
        /// Initializes the game object.
        /// </summary>
        public override void Initialize()
        {
            this.direction.Normalize();
            this.Velocity = this.direction * this.Speed;

            this.collisionSystem.Register(this.Id, this, this.colliderComponent);

            base.Initialize();
        }

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Vector2.Distance(this.Position, this.initialPosition) > this.MaximumRange)
            {
                this.Destroy();
            }
        }
    }
}