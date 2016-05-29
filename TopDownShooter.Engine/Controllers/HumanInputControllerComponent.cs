﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HumanInputControllerComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Controllers
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Herds Inputs...
    /// </summary>
    public class HumanInputControllerComponent : InputControllerComponentBase
    {
        private readonly IKeyboardAdapter keyboard;
        private KeyboardState previousKeyboardState;
        private KeyboardState currentKeyboardState;
        private MouseState previousMouseState;
        private MouseState currentMouseState;
        private GamePadState previousGamePadState;
        private GamePadState currentGamePadState;

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanInputControllerComponent"/> class.
        /// </summary>
        public HumanInputControllerComponent()
            : this(new KeyboardAdapter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanInputControllerComponent"/> class.
        /// Creates a new <see cref="HumanInputControllerComponent" /> that can be used to have a simple Composite for managing
        /// inputs among multiple devices.
        /// </summary>
        /// <param name="keyboard">The <see cref="IKeyboardAdapter"/>.</param>
        /// <remarks>Internal for unit testing.</remarks>
        internal HumanInputControllerComponent(IKeyboardAdapter keyboard)
        {
            this.keyboard = keyboard;
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public override void Initialize()
        {
        }

        /// <summary>
        /// Destroys the component.
        /// </summary>
        public override void Destroy()
        {
        }

        /// <summary>
        /// Gets a value indicating whether a fire was requested.
        /// </summary>
        /// <returns>True if the action was requested; false otherwise.</returns>
        public override bool Fire()
        {
            return this.currentMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move down.</returns>
        public override bool MoveDown()
        {
            var result = false;

            result = this.currentKeyboardState.IsKeyDown(Keys.S) || this.currentKeyboardState.IsKeyDown(Keys.Down);

            if (!result)
            {
                result = this.currentGamePadState.DPad.Up == ButtonState.Pressed;
            }

            // TODO: How to handle for mouse?
            return result;
        }

        /// <summary>
        /// Gets whether or not a left move was requested.
        /// </summary>
        /// <returns>True to move left.</returns>
        public override bool MoveLeft()
        {
            var result = false;

            result = this.currentKeyboardState.IsKeyDown(Keys.A) || this.currentKeyboardState.IsKeyDown(Keys.Left);

            if (!result)
            {
                result = this.currentGamePadState.DPad.Left == ButtonState.Pressed;
            }

            // TODO: How to handle for mouse?
            return result;
        }

        /// <summary>
        /// Gets whether or not a right move was requested.
        /// </summary>
        /// <returns>True to move right.</returns>
        public override bool MoveRight()
        {
            var result = false;

            result = this.currentKeyboardState.IsKeyDown(Keys.D) || this.currentKeyboardState.IsKeyDown(Keys.Right);

            if (!result)
            {
                result = this.currentGamePadState.DPad.Right == ButtonState.Pressed;
            }

            // TODO: How to handle for mouse?
            return result;
        }

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move up.</returns>
        public override bool MoveUp()
        {
            var result = false;

            result = this.currentKeyboardState.IsKeyDown(Keys.W) || this.currentKeyboardState.IsKeyDown(Keys.Up);

            if (!result)
            {
                result = this.currentGamePadState.DPad.Up == ButtonState.Pressed;
            }

            // TODO: How to handle for mouse?
            return result;
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="time">The game time.</param>
        public override void Update(IGameObject gameObject, GameTime time)
        {
            this.previousMouseState = this.currentMouseState;
            this.previousKeyboardState = this.currentKeyboardState;
            this.previousGamePadState = this.currentGamePadState;

            this.currentMouseState = Mouse.GetState();
            this.currentKeyboardState = this.keyboard.GetState();
            this.currentGamePadState = GamePad.GetState(PlayerIndex.One);

            base.Update(gameObject, time);
        }
    }
}