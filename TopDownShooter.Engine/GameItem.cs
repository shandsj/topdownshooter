// <copyright file="GameItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Collisions;

    /// <summary>
    /// Base implementation for a simple <see cref="IGameItem"/>. The primary difference
    /// between <see cref="IGameObject"/> and <see cref="IGameItem"/> is that <see cref="IGameItem"/>s
    /// can be placed in <see cref="InventoryComponentBase"/>
    /// </summary>
    public abstract class GameItem : GameObject, IGameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameItem" /> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        protected GameItem(int id)
            : this(id, new IComponent[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameItem" /> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="components">The collection of components.</param>
        protected GameItem(int id, IEnumerable<IComponent> components)
            : base(id, components)
        {
        }

        /// <summary>
        /// Gets a value indicating whether this game item has been picked up.
        /// </summary>
        /// <remarks>For use when managing a collection of these in the world, which ones
        /// can be removed from the update and draw loops</remarks>
        public bool IsPickedUp { get; private set; }

        /// <summary>
        /// Should be called when another component will be
        /// taking ownership of this item.
        /// </summary>
        /// <returns><see cref="IGameItem"/> reference.</returns>
        /// <remarks><see cref="IGameItem"/> here could be used to return an Inventory friendly
        /// viewable GameItem here.</remarks>
        public virtual IGameItem Pickup()
        {
            var animationComponent = this.Get<IAnimationComponentManager>();
            var colliderComponent = this.Get<IColliderComponent>();

            animationComponent?.Destroy();
            colliderComponent?.Destroy();

            if (animationComponent != null)
            {
                this.Components.Remove(animationComponent);
            }

            if (colliderComponent != null)
            {
                this.Components.Remove(colliderComponent);
            }

            this.IsPickedUp = true;
            return this;
        }
    }
}