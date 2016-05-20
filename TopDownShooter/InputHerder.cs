// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputHerder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Herds Inputs...
    /// </summary>
    public class InputHerder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputHerder"/> class.
        /// Creates a new <see cref="InputHerder" /> that can be used to have a simple Composite for managing
        /// inputs among multiple devices.
        /// </summary>
        /// <param name="initialKeyboardState"></param>
        /// <param name="initialMouseState"></param>
        /// <param name="initialGamePadState"></param>
        public InputHerder(KeyboardState initialKeyboardState, MouseState initialMouseState, GamePadState initialGamePadState)
        {
            this.m_CurrentKeyboardState = this.m_PreviousKeyboardState =
                initialKeyboardState;

            this.m_PreviousMouseState = this.m_CurrentMouseState =
                initialMouseState;

            this.m_PreviousGamePadState = this.m_CurrentGamePadState =
                initialGamePadState;
        }

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns></returns>
        public bool MoveDown()
        {
            var result = false;

            result = this.m_CurrentKeyboardState.IsKeyDown(Keys.S) || this.m_CurrentKeyboardState.IsKeyDown(Keys.Down);

            if (!result)
            {
                result = this.m_CurrentGamePadState.DPad.Up == ButtonState.Pressed;
            }

            // TODO: How to handle for mouse?
            return result;
        }

        /// <summary>
        /// Gets whether or not a left move was requested.
        /// </summary>
        /// <returns></returns>
        public bool MoveLeft()
        {
            var result = false;

            result = this.m_CurrentKeyboardState.IsKeyDown(Keys.A) || this.m_CurrentKeyboardState.IsKeyDown(Keys.Left);

            if (!result)
            {
                result = this.m_CurrentGamePadState.DPad.Left == ButtonState.Pressed;
            }

            // TODO: How to handle for mouse?
            return result;
        }

        /// <summary>
        /// Gets whether or not a right move was requested.
        /// </summary>
        /// <returns></returns>
        public bool MoveRight()
        {
            var result = false;

            result = this.m_CurrentKeyboardState.IsKeyDown(Keys.D) || this.m_CurrentKeyboardState.IsKeyDown(Keys.Right);

            if (!result)
            {
                result = this.m_CurrentGamePadState.DPad.Right == ButtonState.Pressed;
            }

            // TODO: How to handle for mouse?
            return result;
        }

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns></returns>
        public bool MoveUp()
        {
            var result = false;

            result = this.m_CurrentKeyboardState.IsKeyDown(Keys.W) || this.m_CurrentKeyboardState.IsKeyDown(Keys.Up);

            if (!result)
            {
                result = this.m_CurrentGamePadState.DPad.Up == ButtonState.Pressed;
            }

            // TODO: How to handle for mouse?
            return result;
        }

        /// <summary>
        /// Gets whether a quit input command has been issued.
        /// </summary>
        /// <returns></returns>
        public bool ShouldQuit()
        {
            return this.m_CurrentKeyboardState.IsKeyDown(Keys.Escape) || this.m_CurrentGamePadState.Buttons.Back == ButtonState.Pressed;
        }

        /// <summary>
        /// Updates the current state for all the inputs.
        /// </summary>
        /// <param name="newKeyboardState"></param>
        /// <param name="newMouseState"></param>
        /// <param name="newGamePadState"></param>
        public void Update(KeyboardState newKeyboardState, MouseState newMouseState, GamePadState newGamePadState)
        {
            this.m_PreviousMouseState = this.m_CurrentMouseState;
            this.m_PreviousKeyboardState = this.m_CurrentKeyboardState;
            this.m_PreviousGamePadState = this.m_CurrentGamePadState;

            this.m_CurrentMouseState = newMouseState;
            this.m_CurrentKeyboardState = newKeyboardState;
            this.m_CurrentGamePadState = newGamePadState;
        }

        #region Private Members

        private KeyboardState m_PreviousKeyboardState;

        private KeyboardState m_CurrentKeyboardState;

        private MouseState m_PreviousMouseState;

        private MouseState m_CurrentMouseState;

        private GamePadState m_PreviousGamePadState;

        private GamePadState m_CurrentGamePadState;

        #endregion
    }
}