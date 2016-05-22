// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using TopDownShooter.Engine;

    /// <summary>
    /// Simple player class.
    /// </summary>
    public class Player : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
#pragma warning disable SA1118 // Parameter must not span multiple lines
            : this(new Vector2(1600, 1600), new[]
            {
                new AnimationComponent("hoodieguy", new FrameProperties(76, 140, TimeSpan.FromSeconds(.1), 2)) { IsLooping = true }
            })
#pragma warning restore SA1118 // Parameter must not span multiple lines
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="position">The position of the player.</param>
        /// <param name="components">A collection of components.</param>
        /// <remarks>Internal for unit testing.</remarks>
        internal Player(Vector2 position, IEnumerable<IComponent> components)
        {
            this.Position = position;

            foreach (var component in components)
            {
                this.Components.Add(component);
            }
        }
    }
}