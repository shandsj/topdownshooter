// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HumanInputControllerComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Controllers
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using MonoGame.Extended;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Herds Inputs...
    /// </summary>
    public class HumanInputControllerComponent : InputControllerComponentBase
    {
        private readonly IGamePadAdapter gamePad;

        private readonly IKeyboardAdapter keyboard;

        private readonly IMouseAdapter mouse;

        private readonly ICamera2DAdapter camera;

        private bool fire;

        private Vector2 movementDirection;

        private float rotation;

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanInputControllerComponent" /> class.
        /// </summary>
        /// <param name="camera">The camera.</param>
        public HumanInputControllerComponent(ICamera2DAdapter camera)
            : this(camera, new KeyboardAdapter(), new MouseAdapter(), new GamePadAdapter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanInputControllerComponent" /> class.
        /// Creates a new <see cref="HumanInputControllerComponent" /> that can be used to have a simple Composite for managing
        /// inputs among multiple devices.
        /// </summary>
        /// <param name="camera">The camera.</param>
        /// <param name="keyboard">The <see cref="IKeyboardAdapter" />.</param>
        /// <param name="mouse">The <see cref="IMouseAdapter" />.</param>
        /// <param name="gamePad">The <see cref="IGamePadAdapter" />.</param>
        /// <remarks>Internal for unit testing.</remarks>
        internal HumanInputControllerComponent(ICamera2DAdapter camera, IKeyboardAdapter keyboard, IMouseAdapter mouse, IGamePadAdapter gamePad)
        {
            this.camera = camera;
            this.keyboard = keyboard;
            this.mouse = mouse;
            this.gamePad = gamePad;
        }

        /// <summary>
        /// Gets the movement direction.
        /// </summary>
        public override Vector2 MovementDirection => this.movementDirection;

        /// <summary>
        /// Gets the rotation angle.
        /// </summary>
        public override float Rotation => this.rotation;

        /// <summary>
        /// Gets a value indicating whether a dash was requested.
        /// </summary>
        /// <returns>True if the action was requested; false otherwise.</returns>
        public override bool Dash()
        {
            return this.mouse.GetState().RightButton == ButtonState.Pressed;
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
        /// <param name="gameTime">The game time.</param>
        public override void Update(IGameObject gameObject, GameTime gameTime)
        {
            var mouseState = this.mouse.GetState();
            this.fire = mouseState.LeftButton == ButtonState.Pressed;
            this.rotation = this.GetMouseRotationAngle(gameObject.Position, mouseState);

            var gamePadState = this.gamePad.GetState(PlayerIndex.One);
            var vector = gamePadState.ThumbSticks.Left;
            if (vector == Vector2.Zero)
            {
                vector = this.GetDPadMovementDirectionalVector(gamePadState.DPad);
            }

            if (vector == Vector2.Zero)
            {
                vector = this.GetKeyboardMovementVector(this.rotation, this.keyboard.GetState());
            }

            this.movementDirection = vector;
            base.Update(gameObject, gameTime);
        }

        /// <summary>
        /// Gets the DPad directional vector.
        /// </summary>
        /// <param name="dpad">The <see cref="GamePadDPad" />.</param>
        /// <returns>The directional vector.</returns>
        private Vector2 GetDPadMovementDirectionalVector(GamePadDPad dpad)
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
        /// Gets the keyboard movement vector.
        /// </summary>
        /// <param name="angle">The rotation angle</param>
        /// <param name="keyboardState">The <see cref="KeyboardState" />.</param>
        /// <returns>The directional vector.</returns>
        private Vector2 GetKeyboardMovementVector(float angle, KeyboardState keyboardState)
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

            return new Vector2(x, y).Rotate(angle);
        }

        /// <summary>
        /// Gets the mouse rotation angle.
        /// </summary>
        /// <param name="playerPosition">The player's position.</param>
        /// <param name="mouseState">The mouse state.</param>
        /// <returns>The rotation angle.</returns>
        private float GetMouseRotationAngle(Vector2 playerPosition, MouseState mouseState)
        {
            return (this.camera.ScreenToWorld(mouseState.Position.ToVector2()) - playerPosition).ToAngle();
        }
    }
}