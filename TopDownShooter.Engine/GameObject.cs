// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Adapters;

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
            : this(id, new IComponent[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject" /> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="components">The collection of components.</param>
        protected GameObject(int id, IEnumerable<IComponent> components)
        {
            this.Id = id;
            this.Components = new List<IComponent>(components);
        }

        /// <summary>
        /// Gets the bounds of the game object.
        /// </summary>
        public virtual Rectangle Bounds => new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Width, this.Height);

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
        /// Gets the projected position, based off the <see cref="IGameObject.Position"/> and the <see cref="IGameObject.Velocity"/>.
        /// </summary>
        public virtual Vector2 ProjectedPosition => this.Position + this.Velocity;

        /// <summary>
        /// Gets the projected bounds, based off the <see cref="IGameObject.Position"/> and the <see cref="IGameObject.Velocity"/>.
        /// </summary>
        public virtual Rectangle ProjectedBounds => new Rectangle((int)this.ProjectedPosition.X, (int)this.ProjectedPosition.Y, this.Width, this.Height);

        /// <summary>
        /// Gets the width of the game object.
        /// </summary>
        public abstract int Width { get; }

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public abstract int Height { get; }

        /// <summary>
        /// Gets or sets the velocity of the entity.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Draws the game object with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="camera">The <see cref="ICamera"/>.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void Draw(ICamera camera, ISpriteBatchAdapter spriteBatch, GameTime gameTime)
        {
            foreach (var component in this.Components)
            {
                component.Draw(this, camera, spriteBatch, gameTime);
            }
        }

        /// <summary>
        /// Initializes the game object.
        /// </summary>
        public virtual void Initialize()
        {
            foreach (var component in this.Components)
            {
                component.Initialize();
            }
        }

        /// <summary>
        /// Destroyes the game object.
        /// </summary>
        public virtual void Destroy()
        {
            foreach (var component in this.Components)
            {
                component.Destroy();
            }
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
        /// Broadcasts a message to all components.
        /// </summary>
        /// <param name="message">The message to broadcast.</param>
        public void BroadcastMessage(ComponentMessage message)
        {
            foreach (var component in this.Components)
            {
                component.ReceiveMessage(this, message);
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

            this.Position += this.Velocity;
        }

        /// <summary>
        /// Returns the first <see cref="IComponent"/> of a requested type.
        /// </summary>
        /// <typeparam name="TComponent">Component type to find.</typeparam>
        /// <returns>TComponent if found, null otherwise.</returns>
        /// <remarks>To throw or not to throw? That is the question.</remarks>
        protected TComponent Get<TComponent>()
        {
            return this.Components.OfType<TComponent>().FirstOrDefault();
        }

        /// <summary>
        /// Returns the all <see cref="IComponent"/>s of a requested type.
        /// </summary>
        /// <typeparam name="TComponent">Component type to find.</typeparam>
        /// <returns><see cref="IEnumerable{TComponent}"/> if found, null otherwise.</returns>
        /// <remarks>To throw or not to throw? That is the question.</remarks>
        protected IEnumerable<TComponent> GetAll<TComponent>()
        {
            return this.Components.OfType<TComponent>();
        }
    }
}