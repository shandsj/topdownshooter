using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter {

    /// <summary>
    ///     Herds Inputs... 
    /// </summary>
    public class InputHerder {

        #region Private Members

        private KeyboardState m_PreviousKeyboardState;
        private KeyboardState m_CurrentKeyboardState;

        private MouseState m_PreviousMouseState;
        private MouseState m_CurrentMouseState;

        private GamePadState m_PreviousGamePadState;
        private GamePadState m_CurrentGamePadState;

        #endregion

        /// <summary>
        ///     Creates a new <see cref="InputHerder"/> that can be used to have a simple Composite for managing 
        ///     inputs among multiple devices.
        /// </summary>
        /// <param name="initialKeyboardState"></param>
        /// <param name="initialMouseState"></param>
        /// <param name="initialGamePadState"></param>
        public InputHerder(KeyboardState initialKeyboardState, MouseState initialMouseState, GamePadState initialGamePadState) {
            m_CurrentKeyboardState = 
                m_PreviousKeyboardState = 
                initialKeyboardState;

            m_PreviousMouseState =
                m_CurrentMouseState = 
                initialMouseState;

            m_PreviousGamePadState =
              m_CurrentGamePadState =
              initialGamePadState;
        }

        /// <summary>
        ///     Updates the current state for all the inputs.
        /// </summary>
        /// <param name="newKeyboardState"></param>
        /// <param name="newMouseState"></param>
        /// <param name="newGamePadState"></param>
        public void Update(KeyboardState newKeyboardState, MouseState newMouseState, GamePadState newGamePadState) {
            m_PreviousMouseState = m_CurrentMouseState;
            m_PreviousKeyboardState = m_CurrentKeyboardState;
            m_PreviousGamePadState = m_CurrentGamePadState;

            m_CurrentMouseState = newMouseState;
            m_CurrentKeyboardState = newKeyboardState;
            m_CurrentGamePadState = newGamePadState;
        }

        /// <summary>
        ///     Gets whether a quit input command has been issued.
        /// </summary>
        /// <returns></returns>
        public bool ShouldQuit () {
            return m_CurrentKeyboardState.IsKeyDown(Keys.Escape) || m_CurrentGamePadState.Buttons.Back == ButtonState.Pressed;
        }

        /// <summary>
        ///     Gets whether or not a left move was requested.
        /// </summary>
        /// <returns></returns>
        public bool MoveLeft() {
            var result = false;

            result = (m_CurrentKeyboardState.IsKeyDown(Keys.A) || m_CurrentKeyboardState.IsKeyDown(Keys.Left));

            if(!result) {
                result = m_CurrentGamePadState.DPad.Left == ButtonState.Pressed;
            }

            // TODO: How to handle for mouse?

            return result;
        }

        /// <summary>
        ///     Gets whether or not a right move was requested.
        /// </summary>
        /// <returns></returns>
        public bool MoveRight() {
            var result = false;

            result = (m_CurrentKeyboardState.IsKeyDown(Keys.D) || m_CurrentKeyboardState.IsKeyDown(Keys.Right));

            if (!result) {
                result = m_CurrentGamePadState.DPad.Right == ButtonState.Pressed;
            }

            // TODO: How to handle for mouse?

            return result;
        }

        /// <summary>
        ///     Gets whether or not a up move was requested.
        /// </summary>
        /// <returns></returns>
        public bool MoveUp() {
            var result = false;

            result = (m_CurrentKeyboardState.IsKeyDown(Keys.W) || m_CurrentKeyboardState.IsKeyDown(Keys.Up));

            if (!result) {
                result = m_CurrentGamePadState.DPad.Up == ButtonState.Pressed;
            }

            // TODO: How to handle for mouse?

            return result;
        }

        /// <summary>
        ///     Gets whether or not a up move was requested.
        /// </summary>
        /// <returns></returns>
        public bool MoveDown() {
            var result = false;

            result = (m_CurrentKeyboardState.IsKeyDown(Keys.S) || m_CurrentKeyboardState.IsKeyDown(Keys.Down));

            if (!result) {
                result = m_CurrentGamePadState.DPad.Up == ButtonState.Pressed;
            }

            // TODO: How to handle for mouse?

            return result;
        }
    }
}
