using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooter.Engine;

namespace TopDownShooter {

    /// <summary>
    ///     Simple player class.
    /// </summary>
    public class Player {

        /// <summary>
        ///     Fixed speed for now.
        /// </summary>
        private readonly float m_PlayerSpeed = 8f;

        /// <summary>
        ///     Gets the <see cref="Animation"/> associated with this <see cref="Player"/>
        /// </summary>
        public Animation PlayerAnimation { get; set; }

        /// <summary>
        ///     Gets the player speed.
        /// </summary>
        /// <remarks>Currently fixed.</remarks>
        public float PlayerSpeed { get { return m_PlayerSpeed; } }

        /// <summary>
        ///     Gets the current <see cref="Vector2"/> position.
        /// </summary>
        public Vector2 Position { get; set;  }

        /// <summary>
        ///     Gets the current Width.
        /// </summary>
        public int Width {
            get {
                return PlayerAnimation?.FrameProperties.Width ?? 0;
            }
        }

        /// <summary>
        ///     Gets the current height.
        /// </summary>
        public int Height {
            get {
                return PlayerAnimation?.FrameProperties.Height ?? 0;
            }
        }

        /// <summary>
        ///     Simple indicator used to toggle when to start/stop 
        ///     the animation.
        /// </summary>
        public bool IsMoving {
            get { return PlayerAnimation.IsAnimating; }
            set {
                if (PlayerAnimation.IsAnimating != value) {
                    PlayerAnimation.IsAnimating = value;
                }
            }
        }
        
        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        public void Initialize(Animation playerAnimation, Vector2 initialPosition) {
            Contract.Assert(playerAnimation != null);
            Contract.Assert(initialPosition != null);

            PlayerAnimation = playerAnimation;
            Position = initialPosition;
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime) {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);
        }

        /// <summary>
        ///     Draws this <see cref="Animation" />.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch" />.
        /// </param>
        /// <param name="gameTime">
        ///     The game time.
        /// </param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            Contract.Assert(spriteBatch != null);
            Contract.Assert(gameTime != null);

            PlayerAnimation.Draw(spriteBatch, gameTime);
        }
    }
}