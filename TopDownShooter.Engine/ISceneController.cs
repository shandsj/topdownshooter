// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISceneController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines an interface for a scene controller.
    /// </summary>
    public interface ISceneController
    {
        /// <summary>
        /// Gets the active scene.
        /// </summary>
        IScene ActiveScene { get; }

        /// <summary>
        /// Draws the scene with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        void Draw(GameTime gameTime);

        /// <summary>
        /// Switches to the specified scene by destorying the previous scene, and initializing and loading content for the specified scene.
        /// </summary>
        /// <param name="scene">The new active scene.</param>
        void Switch(IScene scene);

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        void Update(GameTime gameTime);
    }
}