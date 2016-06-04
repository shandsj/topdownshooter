// <copyright file="IGameItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for game objects that can be picked up and placed into
    /// an inventory.
    /// </summary>
    public interface IGameItem : IGameObject
    {
        /// <summary>
        /// Gets a description for this item.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets a value indicating whether this game item has been picked up.
        /// </summary>
        /// <remarks>For use when managing a collection of these in the world, which ones
        /// can be removed from the update and draw loops</remarks>
        bool IsPickedUp { get; }

        /// <summary>
        /// Should be called when another component will be
        /// taking ownership of this item.
        /// </summary>
        /// <returns><see cref="IGameItem"/> reference.</returns>
        /// <remarks><see cref="IGameItem"/> here could be used to return an Inventory friendly
        /// viewable GameItem here.</remarks>
        IGameItem Pickup();
    }
}