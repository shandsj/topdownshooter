// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter
{
    using System.Diagnostics.Contracts;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using TopDownShooter.Engine;

    /// <summary>
    /// Simple player class.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Fixed speed for now.
        /// </summary>
        private readonly float m_PlayerSpeed = 8f;

        /// <summary>
        /// Gets the current height.
        /// </summary>
        public int Height
        {
            get
            {
                return this.PlayerAnimation?.FrameProperties.Height ?? 0;
            }
        }

        /// <summary>
        /// Simple indicator used to toggle when to start/stop
        /// the animation.
        /// </summary>
        public bool IsMoving
        {
            get
            {
                return this.PlayerAnimation.IsAnimating;
            }

            set
            {
                if (this.PlayerAnimation.IsAnimating != value)
                {
                    this.PlayerAnimation.IsAnimating = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="Animation" /> associated with this <see cref="Player" />
        /// </summary>
        public Animation PlayerAnimation { get; set; }

        /// <summary>
        /// Gets the player speed.
        /// </summary>
        /// <remarks>Currently fixed.</remarks>
        public float PlayerSpeed
        {
            get
            {
                return this.m_PlayerSpeed;
            }
        }

        /// <summary>
        /// Gets or sets the current <see cref="Vector2" /> position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets the current Width.
        /// </summary>
        public int Width
        {
            get
            {
                return this.PlayerAnimation?.FrameProperties.Width ?? 0;
            }
        }

        /// <summary>
        /// Draws this <see cref="Animation" />.
        /// </summary>
        /// <param name="spriteBatch">
        /// The <see cref="SpriteBatch" />.
        /// </param>
        /// <param name="gameTime">
        /// The game time.
        /// </param>
        public void Draw(ISpriteBatchAdapter spriteBatch, GameTime gameTime)
        {
            Contract.Assert(spriteBatch != null);
            Contract.Assert(gameTime != null);

            this.PlayerAnimation.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize(Animation playerAnimation, Vector2 initialPosition)
        {
            Contract.Assert(playerAnimation != null);
            Contract.Assert(initialPosition != null);

            this.PlayerAnimation = playerAnimation;
            this.Position = initialPosition;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            this.PlayerAnimation.Position = this.Position;
            this.PlayerAnimation.Update(gameTime);
        }
    }
}