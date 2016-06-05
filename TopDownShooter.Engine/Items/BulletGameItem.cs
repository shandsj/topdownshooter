﻿// <copyright file="BulletGameItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Collisions;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Bullet Game item implementation
    /// </summary>
    public class BulletGameItem : GameItem
    {
        private ICollisionSystem collisionSystem;
        private IAnimationComponentManager animationComponentManager;
        private IColliderComponent colliderComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletGameItem"/> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="position">The position of the player.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="components">A collection of components.</param>
        public BulletGameItem(int id, Vector2 position, ICollisionSystem collisionSystem, IEnumerable<IComponent> components)
            : base(id, components)
        {
            this.Position = position;

            this.collisionSystem = collisionSystem;
            this.animationComponentManager = this.Get<IAnimationComponentManager>();
            this.colliderComponent = this.Get<IColliderComponent>();
        }

        /// <summary>
        /// Gets a description for this item.
        /// </summary>
        public override string Description {
            get {
                return "Bullets";
            }
        }

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public override int Height => this.animationComponentManager?.FrameProperties.Height ?? 0;

        /// <summary>
        /// Gets the Width of the game object.
        /// </summary>
        public override int Width => this.animationComponentManager?.FrameProperties.Width ?? 0;

        /// <summary>
        /// Initializes the game object.
        /// </summary>
        public override void Initialize()
        {
            this.collisionSystem.Register(this.Id, this, this.colliderComponent);

            base.Initialize();
        }

        /// <summary>
        /// Should be called when another component will be
        /// taking ownership of this item.
        /// </summary>
        /// <returns><see cref="IGameItem"/> reference.</returns>
        /// <remarks><see cref="IGameItem"/> here could be used to return an Inventory friendly
        /// viewable GameItem here.</remarks>
        public override IGameItem Pickup()
        {
            this.collisionSystem.Unregister(this.Id);

            this.animationComponentManager = null;
            this.colliderComponent = null;

            return base.Pickup();
        }
    }
}