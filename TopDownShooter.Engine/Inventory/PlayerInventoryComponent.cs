// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerInventoryComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Inventory
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Items;
    using TopDownShooter.Engine.Messages;
    using TopDownShooter.Engine.Projectiles;

    /// <summary>
    /// Inventory storage for an <see cref="IPlayer" />
    /// </summary>
    public class PlayerInventoryComponent : InventoryComponentBase
    {
        // For now just farming off fire requests to this, all the components that
        // could respond to fire events should be passed in
        private readonly ProjectileGeneratorComponent projectileGeneratorComponent;

        private SpriteFont font;

        private Dictionary<string, int> itemCounts;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInventoryComponent" /> class.
        /// </summary>
        /// <param name="collisionSystem">Collision system reference.</param>
        public PlayerInventoryComponent(ICollisionSystem collisionSystem)
        {
            this.itemCounts = new Dictionary<string, int>();
            this.projectileGeneratorComponent = new ProjectileGeneratorComponent(collisionSystem);
        }

        /// <summary>
        /// Gets the cost for firing a short range projectile.
        /// </summary>
        public int ShortRangeProjectileCost => 10;

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="camera">The <see cref="ICamera2DAdapter" />.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        public override void Draw(IGameObject gameObject, ICamera2DAdapter camera, ISpriteBatchAdapter spriteBatch, GameTime time)
        {
            base.Draw(gameObject, camera, spriteBatch, time);
            this.projectileGeneratorComponent.Draw(gameObject, camera, spriteBatch, time);

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
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public override void LoadContent(IContentManagerAdapter contentManager)
        {
            base.LoadContent(contentManager);
            this.projectileGeneratorComponent.LoadContent(contentManager);

            this.font = contentManager.Load<SpriteFont>("Fonts/PlayerName");
        }

        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="message">The message object.</param>
        /// <param name="gameTime">The game time.</param>
        public override void ReceiveMessage(IGameObject gameObject, Message message, GameTime gameTime)
        {
            base.ReceiveMessage(gameObject, message, gameTime);

            switch (message.MessageType)
            {
                case MessageType.Fire:
                {
                    var gameItems = new List<IGameItem>();
                    var projectileType = ProjectileType.Invalid;
                    var gameItem = this.Inventory.OfType<LongRangeGameItem>().FirstOrDefault();
                    if (gameItem != null)
                    {
                        projectileType = ProjectileType.LongRange;
                        gameItems.Add(gameItem);
                    }
                    else if (this.Inventory.OfType<CoinGameItem>().Count() > this.ShortRangeProjectileCost)
                    {
                        projectileType = ProjectileType.ShortRange;
                        var coinItems = this.Inventory.OfType<CoinGameItem>().Take(this.ShortRangeProjectileCost);
                        gameItems.AddRange(coinItems);
                    }

                    if (projectileType != ProjectileType.Invalid)
                    {
                        // For now just forwarding it on to the bullet projectile generator component
                        // so that we don't have to manage all the bullet items ourselves. Would this
                        // be relegated to more of a global projectile component?
                        var fireMessage = (FireMessage)message;
                        fireMessage.ProjectileType = projectileType;
                        this.projectileGeneratorComponent.ReceiveMessage(gameObject, message, gameTime);
                        gameItems.ForEach(this.RemoveGameItem);
                    }

                    break;
                }

                case MessageType.ItemPickup:
                    IGameItem item = message.MessageDetails as IGameItem;

                    // can only have one bulllet item at a time in this inventory.
                    if (item != null)
                    {
                        var isBullet = item as LongRangeGameItem != null;
                        if ((isBullet && !this.Inventory.OfType<LongRangeGameItem>().Any())
                            || !isBullet)
                        {
                            this.AddGameItem(item.Pickup());
                        }
                    }

                    break;

                case MessageType.DropCoins:
                    var dropCoinsMessage = (DropCoinsMessage)message;
                    var coins = this.Inventory.OfType<CoinGameItem>();
                    coins = coins.Take(dropCoinsMessage.Count);
                    foreach (var coin in coins)
                    {
                        this.RemoveGameItem(coin);
                    }

                    break;
            }
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Update(IGameObject gameObject, GameTime gameTime)
        {
            base.Update(gameObject, gameTime);
            this.projectileGeneratorComponent.Update(gameObject, gameTime);

            this.itemCounts = this.Inventory.GroupBy(obj => obj.Description)
                .ToDictionary(group => group.Key, count => count.Count());
        }
    }
}