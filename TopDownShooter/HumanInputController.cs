// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HumanInputController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using Engine;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Herds Inputs...
    /// </summary>
    public class HumanInputController : IInputController
    {
        private KeyboardState previousKeyboardState;
        private KeyboardState currentKeyboardState;
        private MouseState previousMouseState;
        private MouseState currentMouseState;
        private GamePadState previousGamePadState;
        private GamePadState currentGamePadState;

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanInputController"/> class.
        /// Creates a new <see cref="HumanInputController" /> that can be used to have a simple Composite for managing
        /// inputs among multiple devices.
        /// </summary>
        public HumanInputController()
        {
        }

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move down.</returns>
        public bool MoveDown()
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
        public bool MoveLeft()
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
        public bool MoveRight()
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
        public bool MoveUp()
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
        /// Updates based on the current gametime.
        /// </summary>
        /// <param name="gameTime">The current gametime.</param>
        public void Update(GameTime gameTime)
        {
            this.previousMouseState = this.currentMouseState;
            this.previousKeyboardState = this.currentKeyboardState;
            this.previousGamePadState = this.currentGamePadState;

            this.currentMouseState = Mouse.GetState();
            this.currentKeyboardState = Keyboard.GetState();
            this.currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }
    }
}