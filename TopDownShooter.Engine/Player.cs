// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using System.Collections.Generic;
    using System.Linq;
    using Messages;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Simple player class.
    /// </summary>
    public class Player : GameObject, IPlayer
    {
        private readonly IAnimationComponentManager animationComponentManager;

        private readonly IColliderComponent colliderComponent;

        private readonly ICollisionSystem collisionSystem;

        private SpriteFont font;

        private bool hasCreatedDeathAnimation;

        private ParticleGeneratorComponent particleGeneratorComponent;

        private DashComponent dashComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player" /> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="position">The position of the player.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        /// <param name="components">A collection of components.</param>
        public Player(int id, Vector2 position, ICollisionSystem collisionSystem, IEnumerable<IComponent> components)
            : base(id, components)
        {
            this.Health = 1;

            this.Position = position;
            this.collisionSystem = collisionSystem;
            this.dashComponent = this.Components.OfType<DashComponent>().FirstOrDefault();
            this.animationComponentManager = this.Components.OfType<IAnimationComponentManager>().FirstOrDefault();
            this.colliderComponent = this.Components.OfType<IColliderComponent>().FirstOrDefault();
            this.particleGeneratorComponent = this.Components.OfType<ParticleGeneratorComponent>().FirstOrDefault();

            this.collisionSystem.Register(id, this, this.colliderComponent);
        }

        /// <summary>
        /// Gets the bounds offset via the size of the <see cref="AnimationComponent.FrameProperties" />
        /// </summary>
        public override Rectangle Bounds => new Rectangle((int)this.Position.X - (this.Width / 2), (int)this.Position.Y - (this.Height / 2), this.Width, this.Height);

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public override int Height => this.animationComponentManager.FrameProperties.Height;

        /// <summary>
        /// Gets or sets the number of kills this player has performed.
        /// </summary>
        public int KillCount { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the projected bounds, based off the <see cref="IGameObject.Position" /> and the <see cref="IGameObject.Velocity" />.
        /// </summary>
        public override Rectangle ProjectedBounds => new Rectangle((int)this.ProjectedPosition.X - (this.Width / 2), (int)this.ProjectedPosition.Y - (this.Height / 2), this.Width, this.Height);

        /// <summary>
        /// Gets the width of the game object.
        /// </summary>
        public override int Width => this.animationComponentManager.FrameProperties.Width;

        /// <summary>
        /// Draws the game object with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="camera">The <see cref="ICamera2DAdapter"/>.</param>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(ICamera2DAdapter camera, ISpriteBatchAdapter spriteBatch, GameTime gameTime)
        {
            base.Draw(camera, spriteBatch, gameTime);

            spriteBatch.DrawString(
                this.font,
                string.Format("{0} - {1}", this.Name, this.KillCount),
                this.Position - new Vector2(this.Width / 2f, (this.Height / 2f) + 20),
                Color.Black);
        }

        /// <summary>
        /// Loads the content from the specified content manager adapter.
        /// </summary>
        /// <param name="contentManager">The content manager adapter.</param>
        public override void LoadContent(IContentManagerAdapter contentManager)
        {
            base.LoadContent(contentManager);

            this.font = contentManager.Load<SpriteFont>("Fonts/PlayerName");
        }

        /// <summary>
        /// Updates the game object with the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            this.particleGeneratorComponent.IsEmitting = this.dashComponent.IsDashing(gameTime);

            if (this.hasCreatedDeathAnimation)
            {
                return;
            }

            if (this.Health == 0 && !this.hasCreatedDeathAnimation)
            {
                this.Velocity = new Vector2(0, 0);
                this.hasCreatedDeathAnimation = true;
                this.animationComponentManager.Play("Death");

                this.Components.Clear();
                this.Components.Add(this.animationComponentManager);
            }
            else if (this.dashComponent.IsDashing(gameTime))
            {
                this.animationComponentManager.Play("Dash");
            }
            else if (this.Velocity.Equals(Vector2.Zero))
            {
                this.animationComponentManager.Play("Stand");
            }
            else
            {
                this.animationComponentManager.Play("Walk");
            }

            base.Update(gameTime);
        }
    }
}