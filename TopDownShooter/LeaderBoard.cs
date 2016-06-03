// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LeaderBoard.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Defines a leader board game object.
    /// </summary>
    public class LeaderBoard : GameObject
    {
        private SpriteFont font;

        private List<IPlayer> players = new List<IPlayer>();

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaderBoard"/> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        public LeaderBoard(int id)
            : this(id, new IComponent[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaderBoard"/> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="components">The collection of <see cref="IComponent"/> objects.</param>
        public LeaderBoard(int id, IEnumerable<IComponent> components)
            : this(id, components, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaderBoard"/> class.
        /// </summary>
        /// <param name="id">The game object identifier.</param>
        /// <param name="components">The collection of <see cref="IComponent"/> objects.</param>
        /// <param name="font">The <see cref="SpriteFont"/> to use.</param>
        /// <remarks>Internal for unit testing.</remarks>
        internal LeaderBoard(int id, IEnumerable<IComponent> components, SpriteFont font)
                        : base(id, components)
        {
            this.font = font;
        }

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        public override int Height { get; }

        /// <summary>
        /// Gets the width of the game object.
        /// </summary>
        public override int Width { get; }

        /// <summary>
        /// Draws the game object with the specified sprite batch adapter and game time.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch adapter.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(ISpriteBatchAdapter spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);

            int index = 0;
            foreach (var player in this.players)
            {
                var text = $"{player.Name} - {player.KillCount}";
                var textSize = this.font.MeasureString(text);
                spriteBatch.DrawString(
                    this.font,
                    text,
                    new Vector2(spriteBatch.GraphicsDevice.Viewport.Width - textSize.X, (index * textSize.Y) + 10),
                    Color.Black);

                index++;
            }
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
        /// Sets the collection of <see cref="IPlayer"/> objects to update from.
        /// </summary>
        /// <param name="value">The collection of <see cref="IPlayer"/> objects.</param>
        public void SetPlayers(IEnumerable<IPlayer> value)
        {
            this.players = new List<IPlayer>(value);
            this.players.Sort((p1, p2) => p2.KillCount - p1.KillCount);
        }
    }
}