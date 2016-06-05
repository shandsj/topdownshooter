// <copyright file="AnimationComponentManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Implementation for the <see cref="IAnimationComponentManager"/>
    /// </summary>
    public class AnimationComponentManager : IAnimationComponentManager
    {
        private IAnimationComponent currentAnimationComponent;
        private Dictionary<string, IAnimationComponent> animationComponents;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationComponentManager"/> class.
        /// </summary>
        /// <param name="animationComponents">The collection of <see cref="IAnimationComponent"/>
        /// for this manager to manage</param>
        public AnimationComponentManager(IEnumerable<IAnimationComponent> animationComponents)
        {
            // TODO: Move to initialize?
            this.animationComponents = new Dictionary<string, IAnimationComponent>();
            foreach (var component in animationComponents)
            {
                this.animationComponents.Add(component.Label, component);
            }

            this.currentAnimationComponent = animationComponents?.FirstOrDefault();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationComponentManager"/> class.
        /// </summary>
        /// <param name="animationComponents">The collection of <see cref="IAnimationComponent"/>
        /// for this manager to manage</param>
        public AnimationComponentManager(params IAnimationComponent[] animationComponents)
            : this(animationComponents.AsEnumerable())
        {
        }

        /// <summary>
        /// Gets the current animations <see cref="FrameProperties"/>
        /// </summary>
        public FrameProperties FrameProperties => this.currentAnimationComponent.FrameProperties;

        /// <summary>
        /// Draws the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="camera">The <see cref="ICamera2DAdapter"/>.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="time">The game time.</param>
        public void Draw(IGameObject gameObject, ICamera2DAdapter camera, ISpriteBatchAdapter spriteBatch, GameTime time)
        {
            if (this.currentAnimationComponent != null &&
                this.currentAnimationComponent.IsRendered)
            {
                this.currentAnimationComponent.Draw(gameObject, camera, spriteBatch, time);
            }
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public void LoadContent(IContentManagerAdapter contentManager)
        {
            foreach (var component in this.animationComponents.Values)
            {
                component.LoadContent(contentManager);
            }
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Destroys the component.
        /// </summary>
        public void Destroy()
        {
            foreach (var component in this.animationComponents.Values)
            {
                component.Destroy();
            }

            this.animationComponents.Clear();
            this.currentAnimationComponent = null;
        }

        /// <summary>
        /// Plays an animation with the given <paramref name="animationLabel"/>
        /// </summary>
        /// <param name="animationLabel">Labeled animation to play.</param>
        /// <param name="isLooping">Toggles whether this animation will play in loop mode</param>
        public void Play(string animationLabel, bool isLooping = true)
        {
            if (this.currentAnimationComponent.Label != animationLabel)
            {
                this.StopAndUnloadCurrentAnimation();

                // Do we want to gracefully handle a key not found? Or just let it
                // throw and have the dev deal with it.
                this.currentAnimationComponent = this.animationComponents[animationLabel];
            }

            this.currentAnimationComponent.IsAnimating = true;
            this.currentAnimationComponent.IsLooping = isLooping;
            this.currentAnimationComponent.IsRendered = true;
        }

        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="message">The message object.</param>
        public void ReceiveMessage(IGameObject gameObject, ComponentMessage message)
        {
            if (message.MessageType == MessageType.Movement)
            {
                Vector2 vector = (Vector2)message.MessageDetails;
                this.currentAnimationComponent.Rotation = (float)Math.Atan2(vector.Y, vector.X) + (float)(Math.PI / 2);
            }
        }

        /// <summary>
        /// Stops the currently playing animation.
        /// </summary>
        public void Stop()
        {
            this.currentAnimationComponent.IsAnimating = false;
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="time">The game time.</param>
        public void Update(IGameObject gameObject, GameTime time)
        {
            this.currentAnimationComponent?.Update(gameObject, time);
        }

        /// <summary>
        /// Helper method to stop and unload the current animation
        /// that is loaded.
        /// </summary>
        private void StopAndUnloadCurrentAnimation()
        {
            if (this.currentAnimationComponent != null)
            {
                this.currentAnimationComponent.Reset();
                this.currentAnimationComponent.IsAnimating = false;
                this.currentAnimationComponent.IsRendered = false;
            }
        }
    }
}