// <copyright file="PlayerInventoryComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Adapters;
    using Collisions;
    using Items;
    using Microsoft.Xna.Framework;
    using Projectiles;

    /// <summary>
    /// Inventory storage for an <see cref="IPlayer"/>
    /// </summary>
    public class PlayerInventoryComponent : InventoryComponentBase
    {
        // For now just farming off fire requests to this, all the components that
        // could respond to fire events should be passed in
        private BulletProjectileGeneratorComponent bulletProjectileGeneratorComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInventoryComponent"/> class.
        /// </summary>
        /// <param name="collisionSystem">Collision system reference.</param>
        public PlayerInventoryComponent(ICollisionSystem collisionSystem)
        {
            this.bulletProjectileGeneratorComponent = new BulletProjectileGeneratorComponent(collisionSystem);
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public override void LoadContent(IContentManagerAdapter contentManager)
        {
            base.LoadContent(contentManager);
            this.bulletProjectileGeneratorComponent.LoadContent(contentManager);
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="time">The game time.</param>
        public override void Update(IGameObject gameObject, GameTime time)
        {
            base.Update(gameObject, time);
            this.bulletProjectileGeneratorComponent.Update(gameObject, time);
        }

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        public override void Draw(IGameObject gameObject, ISpriteBatchAdapter spriteBatch, GameTime time)
        {
            base.Draw(gameObject, spriteBatch, time);
            this.bulletProjectileGeneratorComponent.Draw(gameObject, spriteBatch, time);
        }

        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="message">The message object.</param>
        public override void ReceiveMessage(IGameObject gameObject, ComponentMessage message)
        {
            base.ReceiveMessage(gameObject, message);

            if (message.MessageType == MessageType.Fire)
            {
                var bulletObject = this.Inventory.OfType<BulletGameItem>().FirstOrDefault();
                if (bulletObject != null)
                {
                    // For now just forwarding it on to the bullet projectile generator component
                    // so that we don't have to manage all the bullet items ourselves. Would this
                    // be relegated to more of a global projectile component?
                    this.bulletProjectileGeneratorComponent.ReceiveMessage(gameObject, message);
                    this.Inventory.Remove(bulletObject);
                }
            }
            else if (message.MessageType == MessageType.ItemPickup)
            {
                IGameItem item = message.MessageDetails as IGameItem;

                // can only have one bulllet item at a time in this inventory.
                if (item != null && !this.Inventory.OfType<BulletGameItem>().Any())
                {
                    this.Inventory.Add(item.Pickup());
                }
            }
        }
    }
}