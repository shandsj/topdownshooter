// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestGameObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests
{
    /// <summary>
    /// Defines a test game object.
    /// </summary>
    public class TestGameObject : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestGameObject"/> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        public TestGameObject(int id)
            : base(id)
        {
        }

        /// <summary>
        /// Gets the width of the game object.
        /// </summary>
        public override int Width { get; }

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public override int Height { get; }
    }
}