// <copyright file="SimpleAiInputControllerComponentTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Moq;

    /// <summary>
    /// Class for testing the <see cref="SimpleAiInputControllerComponent"/>
    /// </summary>
    [TestClass]
    public class SimpleAiInputControllerComponentTests
    {
        /// <summary>
        /// Tests whether the animation is drawn in the sprite batch when draw is called.
        /// </summary>
        [TestMethod]
        public void AffectsGameObjectPostionWhenDrawIsCalled()
        {
            var spriteBatch = new Mock<ISpriteBatchAdapter>();

            int positionCheckCounts = 0;
            int positionSetCounts = 0;

            var gameObject = new Mock<IGameObject>();
            gameObject.SetupGet(property => property.Velocity).Returns(new Vector2(8, 8));
            gameObject.SetupGet(property => property.Position).Returns(new Vector2(64, 64)).Callback(() =>
            {
                positionCheckCounts++;
            });
            gameObject.SetupSet(property => property.Position = It.IsAny<Vector2>()).Callback(() =>
            {
                positionSetCounts++;
            });

            var inputController = new SimpleAiInputControllerComponent();

            inputController.Draw(gameObject.Object, spriteBatch.Object, new GameTime());

            // Position is checked twice, once for x, once for y
            Assert.AreEqual(positionCheckCounts, 2);

            // Position is only set once
            Assert.AreEqual(positionSetCounts, 1);
        }

        /// <summary>
        /// The <see cref="SimpleAiInputControllerComponent"/> should not utilize the Draw method
        /// on the <see cref="ISpriteBatchAdapter"/>. It only affects position and velocity.
        /// </summary>
        [TestMethod]
        public void EnsureDrawDoesNotUtilizeSpriteBatch()
        {
            bool wasDrawCalled = false;
            var spriteBatch = new Mock<ISpriteBatchAdapter>();
            spriteBatch.Setup(method => method.Draw(
                It.IsAny<Texture2D>(),
                It.IsAny<Vector2>(),
                It.IsAny<Rectangle?>(),
                It.IsAny<Color>(),
                It.IsAny<float>(),
                It.IsAny<Vector2>(),
                It.IsAny<float>(),
                It.IsAny<SpriteEffects>(),
                It.IsAny<float>()))
            .Callback(() => wasDrawCalled = true);

            var gameObject = new Mock<IGameObject>();
            gameObject.SetupGet(property => property.Velocity).Returns(new Vector2(8, 8));
            gameObject.SetupGet(property => property.Position).Returns(new Vector2(42, 42));
            gameObject.SetupSet(property => property.Position = It.IsAny<Vector2>());

            var inputController = new SimpleAiInputControllerComponent();

            inputController.Draw(gameObject.Object, spriteBatch.Object, new GameTime());

            // Verify that this controller does not mess with the sprite batch.
            Assert.IsFalse(wasDrawCalled);
        }
    }
}
