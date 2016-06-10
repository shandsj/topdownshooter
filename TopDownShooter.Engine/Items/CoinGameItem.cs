// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoinGameItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Items
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Bullet Game item implementation
    /// </summary>
    public class CoinGameItem : GameItem
    {
        private readonly ICollisionSystem collisionSystem;

        private IAnimationComponentManager animationComponentManager;

        private IColliderComponent colliderComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinGameItem" /> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="position">The position of the player.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="components">A collection of components.</param>
        /// <param name="isImmuneToPickup">A value indicating whether the coin game item will be immune to pickup for a short time.</param>
        public CoinGameItem(int id, Vector2 position, ICollisionSystem collisionSystem, IEnumerable<IComponent> components, bool isImmuneToPickup)
            : base(id, components)
        {
            this.Position = position;
            this.collisionSystem = collisionSystem;
            this.IsImmuneToPickup = isImmuneToPickup;

            this.animationComponentManager = this.Get<IAnimationComponentManager>();
            this.colliderComponent = this.Get<IColliderComponent>();
        }

        /// <summary>
        /// Gets a description for this item.
        /// </summary>
        public override string Description => "Coins";

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public override int Height => this.animationComponentManager?.FrameProperties.Height ?? 0;

        /// <summary>
        /// Gets a value indicating whether the collider is registered with the collision system.
        /// </summary>
        public bool IsColliderRegistered { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the coin game item is immune to pickup.
        /// </summary>
        public bool IsImmuneToPickup { get; private set; }

        /// <summary>
        /// Gets the length of time a coin game item can be immune to pickup.
        /// </summary>
        public TimeSpan PickupImmuneTime => TimeSpan.FromSeconds(1);

        /// <summary>
        /// Gets the time this game item was spawned.
        /// </summary>
        public TimeSpan? SpawnTime { get; private set; }

        /// <summary>
        /// Gets the Width of the game object.
        /// </summary>
        public override int Width => this.animationComponentManager?.FrameProperties.Width ?? 0;

        /// <summary>
        /// Initializes the game object.
        /// </summary>
        public override void Initialize()
        {
            if (!this.IsImmuneToPickup)
            {
                this.RegisterCollider();
            }

            base.Initialize();
        }

        /// <summary>
        /// Should be called when another component will be
        /// taking ownership of this item.
        /// </summary>
        /// <returns><see cref="IGameItem" /> reference.</returns>
        /// <remarks>
        /// <see cref="IGameItem" /> here could be used to return an Inventory friendly
        /// viewable GameItem here.
        /// </remarks>
        public override IGameItem Pickup()
        {
            this.collisionSystem.Unregister(this.Id);

            this.animationComponentManager = null;
            this.colliderComponent = null;

            return base.Pickup();
        }

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.SpawnTime == null)
            {
                this.SpawnTime = gameTime.TotalGameTime;
            }

            if (this.IsImmuneToPickup)
            {
                if ((gameTime.TotalGameTime - this.SpawnTime) > this.PickupImmuneTime)
                {
                    this.IsImmuneToPickup = false;
                    this.RegisterCollider();
                }
            }
        }

        /// <summary>
        /// Registers the game item with the collision system.
        /// </summary>
        private void RegisterCollider()
        {
            this.collisionSystem.Register(this.Id, this, this.colliderComponent);
            this.IsColliderRegistered = true;
        }
    }
}