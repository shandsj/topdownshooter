// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IScene.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Defines an interface for a scene.
    /// </summary>
    public interface IScene : IProgress<int>
    {
        /// <summary>
        /// Raised when the scene is completed.
        /// </summary>
        event EventHandler<CompletedEventArgs> Completed;

        /// <summary>
        /// Destroyes the game object.
        /// </summary>
        void Destroy();

        /// <summary>
        /// Draws the scene with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        void Draw(GameTime gameTime);

        /// <summary>
        /// Initializes the game object.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Asynchronously oads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        /// <param name="progress">The <see cref="IProgress{Int32}"/> to report progress.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task LoadContentAsync(IContentManagerAdapter contentManager, IProgress<int> progress);

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        void Update(GameTime gameTime);
    }
}