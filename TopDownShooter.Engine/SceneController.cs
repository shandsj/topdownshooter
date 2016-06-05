// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SceneController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Provides a default implementation of the <see cref="ISceneController" /> interface.
    /// </summary>
    public class SceneController : ISceneController
    {
        private readonly IContentManagerAdapter contentManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneController" /> class.
        /// </summary>
        /// <param name="contentManager">The <see cref="IContentManagerAdapter" />.</param>
        public SceneController(IContentManagerAdapter contentManager)
        {
            this.contentManager = contentManager;
        }

        /// <summary>
        /// Gets the active scene.
        /// </summary>
        public IScene ActiveScene { get; private set; }

        /// <summary>
        /// Draws the scene with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            this.ActiveScene?.Draw(gameTime);
        }

        /// <summary>
        /// Asyncronously preloads the specified initialized scene.
        /// </summary>
        /// <param name="scene">The initialized scene.</param>
        /// <returns>An awaitable task.</returns>
        public Task PreloadAsync(IScene scene)
        {
            return scene.LoadContentAsync(this.contentManager, this);
        }

        /// <summary>
        /// Switches to the specified scene by destorying the previous scene, and initializing and loading content for the specified scene.
        /// </summary>
        /// <param name="scene">The new active scene.</param>
        public void Switch(IScene scene)
        {
            this.ActiveScene?.Destroy();
            ////this.contentManager.Unload();

            this.ActiveScene = scene;
        }

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            this.ActiveScene?.Update(gameTime);
        }

        /// <summary>
        /// Reports a progress update.
        /// </summary>
        /// <param name="value">The value of the updated progress.</param>
        public void Report(int value)
        {
            this.ActiveScene?.Report(value);
        }
    }
}