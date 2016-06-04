// <copyright file="PlayerInventoryComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine.Inventory
{
    using System.Linq;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Items;
    using TopDownShooter.Engine.Projectiles;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Graphics;
    /// <summary>
    /// Inventory storage for an <see cref="IPlayer"/>
    /// </summary>
    public class PlayerInventoryComponent : InventoryComponentBase
    {
        // For now just farming off fire requests to this, all the components that
        // could respond to fire events should be passed in
        private BulletProjectileGeneratorComponent bulletProjectileGeneratorComponent;
        private SpriteFont font;

        private Dictionary<string, int> itemCounts;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInventoryComponent"/> class.
        /// </summary>
        /// <param name="collisionSystem">Collision system reference.</param>
        public PlayerInventoryComponent(ICollisionSystem collisionSystem)
        {
            this.itemCounts = new Dictionary<string, int>();
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

            this.font = contentManager.Load<SpriteFont>("Fonts/PlayerName");
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

            this.itemCounts = this.Inventory.GroupBy(obj => obj.Description)
                .ToDictionary(group => group.Key, count => count.Count());
        }

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="camera">The <see cref="ICamera2DAdapter"/>.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        public override void Draw(IGameObject gameObject, ICamera2DAdapter camera, ISpriteBatchAdapter spriteBatch, GameTime time)
        {
            base.Draw(gameObject, camera, spriteBatch, time);
            this.bulletProjectileGeneratorComponent.Draw(gameObject, camera, spriteBatch, time);

            // TODO: Investigate how to make a HUD for the currently
            //       focused player. For now, don't draw if their name
            //       contains ai. HACK.
            if ((gameObject as Player).Name.Contains("Ai"))
            {
                return;
            }

            int index = 0;
            foreach (var kvp in this.itemCounts)
            {
                var text = $"{kvp.Key}: {kvp.Value}";
                var textSize = this.font.MeasureString(text);
                spriteBatch.DrawString(
                    this.font,
                    text,
                    new Vector2(camera.Position.X - camera.Origin.X + 10, (camera.Position.Y - camera.Origin.Y) + ((index * textSize.Y) + 10)),
                    Color.Black);

                index++;
            }
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
                if (item != null)
                {
                    var isBullet = item as BulletGameItem != null;
                    if ((isBullet && !this.Inventory.OfType<BulletGameItem>().Any())
                        || !isBullet)
                    {
                        this.Inventory.Add(item.Pickup());
                    }
                }
            }
        }
    }
}