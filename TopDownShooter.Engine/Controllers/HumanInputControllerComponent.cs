// --------------------------------------------------------------------------------------------------------------------
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
        private readonly IMouseAdapter mouse;
        private readonly IGamePadAdapter gamePad;
        private bool fire = false;
        private Vector2 direction;

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanInputControllerComponent" /> class.
        /// </summary>
        public HumanInputControllerComponent()
            : this(new KeyboardAdapter(), new MouseAdapter(), new GamePadAdapter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanInputControllerComponent" /> class.
        /// Creates a new <see cref="HumanInputControllerComponent" /> that can be used to have a simple Composite for managing
        /// inputs among multiple devices.
        /// </summary>
        /// <param name="keyboard">The <see cref="IKeyboardAdapter" />.</param>
        /// <param name="mouse">The <see cref="IMouseAdapter"/>.</param>
        /// <param name="gamePad">The <see cref="IGamePadAdapter"/>.</param>
        /// <remarks>Internal for unit testing.</remarks>
        internal HumanInputControllerComponent(IKeyboardAdapter keyboard, IMouseAdapter mouse, IGamePadAdapter gamePad)
        {
            this.keyboard = keyboard;
            this.mouse = mouse;
            this.gamePad = gamePad;
        }

        /// <summary>
        /// Gets the direction vector.
        /// </summary>
        public override Vector2 Direction => this.direction;

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
            return this.fire;
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public override void Initialize()
        {
        }

        /// <summary>
        /// Updates the component with the specified game object and game time.
        /// </summary>
        /// <param name="gameObject">The game object to update.</param>
        /// <param name="time">The game time.</param>
        public override void Update(IGameObject gameObject, GameTime time)
        {
            this.fire = this.mouse.GetState().LeftButton == ButtonState.Pressed;

            var gamePadState = this.gamePad.GetState(PlayerIndex.One);
            var vector = gamePadState.ThumbSticks.Left;
            if (vector == Vector2.Zero)
            {
                vector = this.GetDPadDirectionalVector(gamePadState.DPad);
            }

            if (vector == Vector2.Zero)
            {
                vector = this.GetKeyboardDirectionalVector(this.keyboard.GetState());
            }

            this.direction = vector;
            base.Update(gameObject, time);
        }

        /// <summary>
        /// Gets the DPad directional vector.
        /// </summary>
        /// <param name="dpad">The <see cref="GamePadDPad"/>.</param>
        /// <returns>The directional vector.</returns>
        private Vector2 GetDPadDirectionalVector(GamePadDPad dpad)
        {
            int x = 0;
            int y = 0;

            if (dpad.Up == ButtonState.Pressed)
            {
                y = -1;
            }

            if (dpad.Down == ButtonState.Pressed)
            {
                y = 1;
            }

            if (dpad.Right == ButtonState.Pressed)
            {
                x = 1;
            }

            if (dpad.Left == ButtonState.Pressed)
            {
                x = -1;
            }

            return new Vector2(x, y);
        }

        /// <summary>
        /// Gets the keyboard directional vector.
        /// </summary>
        /// <param name="keyboardState">The <see cref="KeyboardState"/>.</param>
        /// <returns>The directional vector.</returns>
        private Vector2 GetKeyboardDirectionalVector(KeyboardState keyboardState)
        {
            int x = 0;
            int y = 0;

            foreach (var key in keyboardState.GetPressedKeys())
            {
                switch (key)
                {
                    case Keys.Up:
                    case Keys.W:
                        y = -1;
                        break;

                    case Keys.Down:
                    case Keys.S:
                        y = 1;
                        break;

                    case Keys.Left:
                    case Keys.A:
                        x = -1;
                        break;

                    case Keys.Right:
                    case Keys.D:
                        x = 1;
                        break;
                }
            }

            return new Vector2(x, y);
        }
    }
}