﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryComponentBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Base class for collecting inventory items
    /// </summary>
    public abstract class InventoryComponentBase : IComponent
    {
        /// <summary>
        /// The internal storage for game objects.
        /// </summary>
        private List<IGameItem> gameItems = new List<IGameItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryComponentBase" /> class.
        /// </summary>
        protected InventoryComponentBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryComponentBase" /> class.
        /// </summary>
        /// <param name="gameObjects">Starting items to load this inventory with</param>
        protected InventoryComponentBase(IEnumerable<IGameItem> gameObjects)
        {
            this.gameItems.AddRange(gameObjects);
        }

        /// <summary>
        /// Gets the collection of game items in the inventory.
        /// </summary>
        public IEnumerable<IGameItem> Inventory => this.gameItems.ToArray();

        /// <summary>
        /// Destroys the component.
        /// </summary>
        public void Destroy()
        {
            foreach (var gameObject in this.gameItems)
            {
                gameObject.Destroy();
            }

            this.gameItems.Clear();
        }

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="camera">The <see cref="ICamera2DAdapter" />.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        public virtual void Draw(IGameObject gameObject, ICamera2DAdapter camera, ISpriteBatchAdapter spriteBatch, GameTime time)
        {
            // TODO: Draw information about current state of the inventory.
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public virtual void Initialize()
        {
            foreach (var gameObject in this.gameItems)
            {
                gameObject.Initialize();
            }
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public virtual void LoadContent(IContentManagerAdapter contentManager)
        {
            foreach (var gameObject in this.gameItems)
            {
                gameObject.LoadContent(contentManager);
            }
        }

        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="message">The message object.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void ReceiveMessage(IGameObject gameObject, Message message, GameTime gameTime)
        {
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(IGameObject gameObject, GameTime gameTime)
        {
            // TODO: Update item counts for if we decide to draw an inventory summary
        }

        /// <summary>
        /// Adds the specified game item.
        /// </summary>
        /// <param name="item">The game item.</param>
        public void AddGameItem(IGameItem item)
        {
            this.gameItems.Add(item);
        }

        /// <summary>
        /// Adds the specified collection of game items.
        /// </summary>
        /// <param name="items">The collection of game items.</param>
        public void AddGameItemRange(IEnumerable<IGameItem> items)
        {
            this.gameItems.AddRange(items);
        }

        /// <summary>
        /// Removes the specified game item.
        /// </summary>
        /// <param name="item">The game item.</param>
        public void RemoveGameItem(IGameItem item)
        {
            this.gameItems.Remove(item);
        }

        /// <summary>
        /// Removes the specified collection of game items.
        /// </summary>
        /// <param name="items">The collection of game items.</param>
        public void RemoveGameItemRange(IEnumerable<IGameItem> items)
        {
            foreach (var item in items)
            {
                this.gameItems.Remove(item);
            }
        }
    }
}