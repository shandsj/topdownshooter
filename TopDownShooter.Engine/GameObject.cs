// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines an entity.
    /// </summary>
    public abstract class GameObject : IGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject" /> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        protected GameObject(int id)
        {
            this.Id = id;
            this.Components = new List<IComponent>();
        }

        /// <summary>
        /// Gets the bounds of the game object.
        /// </summary>
        public virtual Rectangle Bounds { get; }

        /// <summary>
        /// Gets the collection of components.
        /// </summary>
        public IList<IComponent> Components { get; }

        /// <summary>
        /// Gets the identifier for this game object.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets or sets the position of the entity.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the velocity of the entity.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Draws the game object with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void Draw(ISpriteBatchAdapter spriteBatch, GameTime gameTime)
        {
            foreach (var component in this.Components)
            {
                component.Draw(this, spriteBatch, gameTime);
            }
        }

        /// <summary>
        /// Initializes the game object.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public virtual void LoadContent(IContentManagerAdapter contentManager)
        {
            foreach (var component in this.Components)
            {
                component.LoadContent(contentManager);
            }
        }

        /// <summary>
        /// Unloads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public virtual void UnloadContent(IContentManagerAdapter contentManager)
        {
            foreach (var component in this.Components)
            {
                component.UnloadContent(contentManager);
            }
        }

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in this.Components)
            {
                component.Update(this, gameTime);
            }
        }
    }
}