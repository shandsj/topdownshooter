// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShortRangeProjectile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Projectiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Defines a bullet projectile game object.
    /// </summary>
    public class ShortRangeProjectile : ProjectileBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShortRangeProjectile"/> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="components">The collection of components.</param>
        /// <param name="position">The position of the game object.</param>
        /// <param name="direction">The direction of the bullet projectile, as a unit vector.</param>
        public ShortRangeProjectile(int id, Vector2 position, Vector2 direction, ICollisionSystem collisionSystem, IEnumerable<IComponent> components)
            : base(id, position, direction, collisionSystem, components)
        {
        }

        /// <summary>
        /// Gets the maximum range of this projectile.
        /// </summary>
        public override int MaximumRange => 1000;

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public override int Height => 32;

        /// <summary>
        /// Gets the speed of this projectile.
        /// </summary>
        public override float Speed => 25f;

        /// <summary>
        /// Gets the width of the game object.
        /// </summary>
        public override int Width => 32;
    }
}