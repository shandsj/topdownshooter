// <copyright file="SimpleAiInputControllerComponentTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine.UnitTests.Controllers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Moq;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Controllers;

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
        public void AffectsGameObjectVelocityWhenUpdateIsCalled()
        {
            int velocitySetCounts = 0;

            var gameObject = new Mock<IGameObject>();
            gameObject.SetupSet(property => property.Velocity = It.IsAny<Vector2>()).Callback(() =>
            {
                velocitySetCounts++;
            });

            var inputController = new SimpleAiInputControllerComponent(new Random());

            inputController.Update(gameObject.Object, new GameTime());

            // Velocity is only set once
            Assert.AreEqual(velocitySetCounts, 1);
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

            var inputController = new SimpleAiInputControllerComponent(new Random());

            inputController.Draw(gameObject.Object, spriteBatch.Object, new GameTime());

            // Verify that this controller does not mess with the sprite batch.
            Assert.IsFalse(wasDrawCalled);
        }
    }
}
