// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LeaderBoardTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Moq;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Contains unit tests for the <see cref="LeaderBoard" /> class.
    /// </summary>
    [TestClass]
    public class LeaderBoardTests
    {
        /// <summary>
        /// Tests that the player name and kill count is rendered in the sprite batch.
        /// </summary>
        [TestMethod]
        public void DrawsPlayerNameAndKillCountInSpriteBatchWhenDrawing()
        {
            var player1 = new Mock<IPlayer>();
            player1.SetupGet(p => p.Name).Returns("Player1");
            player1.SetupGet(p => p.KillCount).Returns(5);

            var player2 = new Mock<IPlayer>();
            player2.SetupGet(p => p.Name).Returns("Player2");
            player2.SetupGet(p => p.KillCount).Returns(10);

            var players = new[] { player1.Object, player2.Object };

            var font = new Mock<SpriteFont>();
            font.Setup(f => f.MeasureString(It.IsAny<string>())).Returns(new Vector2(10, 10));

            int drawStringMethodCallCount = 0;
            var spriteBatch = new Mock<ISpriteBatchAdapter>();
            spriteBatch.Setup(sb => sb.DrawString(
                It.IsAny<SpriteFont>(),
                It.IsAny<string>(),
                It.IsAny<Vector2>(),
                It.IsAny<Color>())).Callback<SpriteFont, string, Vector2, Color>((f, t, p, c) =>
                    {
                        drawStringMethodCallCount++;

                        Assert.AreSame(font.Object, f);
                        Assert.AreEqual(Color.Black, c);

                        switch (drawStringMethodCallCount)
                        {
                            case 1:
                                Assert.AreEqual("Player2 - 10", t);
                                break;

                            case 2:
                                Assert.AreEqual("Player1 - 5", t);
                                break;

                            default:
                                Assert.Fail("This should not have been called");
                                break;
                        }
                    });

            var uut = new LeaderBoard(42, null, font.Object);
            uut.SetPlayers(players);
            uut.Draw(new Mock<ICamera2DAdapter>().Object, spriteBatch.Object, new GameTime());
            Assert.AreEqual(2, drawStringMethodCallCount);
        }

        /// <summary>
        /// Tests that the player name font is loaded when loading content.
        /// </summary>
        [TestMethod]
        public void LoadsPlayerNameSpriteFontWhenLoadingContent()
        {
            bool wasLoadCalled = false;
            var contentManager = new Mock<IContentManagerAdapter>();
            contentManager.Setup(cm => cm.Load<SpriteFont>(It.IsAny<string>())).Callback<string>(assetName =>
                {
                    wasLoadCalled = true;
                    Assert.AreEqual("Fonts/PlayerName", assetName);
                });

            var uut = new LeaderBoard(42, new IComponent[0]);
            uut.LoadContent(contentManager.Object);
            Assert.IsTrue(wasLoadCalled);
        }
    }
}