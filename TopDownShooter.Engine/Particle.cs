// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Particle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Defines a particle.
    /// </summary>
    public class Particle : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class.
        /// </summary>
        /// <param name="id">The id of the game object.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="angularVelocity">The angular velocity.</param>
        /// <param name="color">The color.</param>
        /// <param name="size">The size.</param>
        /// <param name="ttl">The time to live.</param>
        public Particle(int id, Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, float size, int ttl)
            : base(id)
        {
            this.Texture = texture;
            this.Position = position;
            this.Velocity = velocity;
            this.Angle = angle;
            this.AngularVelocity = angularVelocity;
            this.Color = color;
            this.Size = size;
            this.TTL = ttl;
        }

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        public float Angle { get; set; }

        /// <summary>
        /// Gets or sets the angular velocity.
        /// </summary>
        public float AngularVelocity { get; set; }

       /// <summary>
       /// Gets or sets the color.
       /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the particle has collided with another object.
        /// </summary>
        public bool HasCollided { get; set; }

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public override int Height { get; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public float Size { get; set; }

        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Gets or sets the time to live.
        /// </summary>
        public int TTL { get; set; }

        /// <summary>
        /// Gets the width of the game object.
        /// </summary>
        public override int Width { get; }

        /// <summary>
        /// Draws the game object with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="camera">The <see cref="ICamera2DAdapter"/>.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(ICamera2DAdapter camera, ISpriteBatchAdapter spriteBatch, GameTime gameTime)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, this.Texture.Width, this.Texture.Height);
            Vector2 origin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);

            spriteBatch.Draw(this.Texture, this.Position, sourceRectangle, this.Color, this.Angle, origin, this.Size, SpriteEffects.None, 0f);

            base.Draw(camera, spriteBatch, gameTime);
        }

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            this.TTL--;
            this.Position += this.Velocity;
            this.Angle += this.AngularVelocity;

            base.Update(gameTime);
        }
    }
}