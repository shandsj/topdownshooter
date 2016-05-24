// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BulletProjectile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines a bullet projectile game object.
    /// </summary>
    public class BulletProjectile : GameObject
    {
        private readonly IColliderComponent colliderComponent;

        private readonly ICollisionSystem collisionSystem;

        private readonly float speed = 20f;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletProjectile" /> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="components">The collection of components.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="direction">The direction of the bullet projectile, as a unit vector.</param>
        public BulletProjectile(int id, Vector2 position, Vector2 direction, ICollisionSystem collisionSystem, IEnumerable<IComponent> components)
            : base(id, components)
        {
            this.Position = position;
            this.collisionSystem = collisionSystem;

            this.Velocity = direction * this.speed;

            this.colliderComponent = this.Components.OfType<IColliderComponent>().FirstOrDefault();
            this.collisionSystem.Register(id, this, this.colliderComponent); // TODO: Make sure to unregister and destroy
        }

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public override int Height => 32;

        /// <summary>
        /// Gets the width of the game object.
        /// </summary>
        public override int Width => 32;
    }
}