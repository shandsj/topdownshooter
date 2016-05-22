// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnimationTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Moq;

    /// <summary>
    /// Contains unit tests for the <see cref="AnimationComponent" /> class.
    /// </summary>
    [TestClass]
    public class AnimationComponentTests
    {
        /// <summary>
        /// Tests whether the animation is drawn in the sprite batch when draw is called.
        /// </summary>
        [TestMethod]
        public void DrawsInSpriteBatchWhenDrawIsCalled()
        {
            bool wasDrawCalled = false;
            var spriteBatch = new Mock<ISpriteBatchAdapter>();

            // Setup our draw method to take any parameters
            // after the method has been executed, call the
            // Callback method associated with it.

            // Note that the Callback method is superfluous for
            // this particular case as we are just using it to
            // verify a method was called. The Verify() call below
            // ensures that the method was called. It is just an example
            // on how to execute code after a Mock method has been
            // called.
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
            gameObject.SetupGet(property => property.Position).Returns(new Vector2(42, 42));

            var uut = new AnimationComponent("test", new FrameProperties(10, 10, TimeSpan.FromSeconds(1), 5))
            {
                Rotation = 42,
                SpriteEffect = SpriteEffects.FlipVertically
            };

            // Do the thing!
            uut.Draw(gameObject.Object, spriteBatch.Object, new GameTime());

            // Verify all our parameters as we expected them to be passed
            // through to the draw method.
            spriteBatch.Verify(method => method.Draw(
                It.IsAny<Texture2D>(),
                It.Is<Vector2>(pos => pos.Equals(new Vector2(42, 42))),
                It.Is<Rectangle?>(rect => rect.Value.Equals(new Rectangle(0, 0, 10, 10))),
                It.Is<Color>(color => color.Equals(Color.White)),
                It.Is<float>(rotation => rotation == 42),
                It.Is<Vector2>(origin => origin.Equals(new Vector2(5, 5))),
                It.Is<float>(scale => scale == 1),
                It.Is<SpriteEffects>(sp => sp == SpriteEffects.FlipVertically),
                It.Is<float>(layerDepth => layerDepth == 0)));

            // Verify our method was called.
            Assert.IsTrue(wasDrawCalled);
        }

        /// <summary>
        /// Tests whether the frame index gets updated when the update method is called.
        /// </summary>
        [TestMethod]
        public void UpdatesFrameIndexWhenUpdateIsCalledAndIsAnimating()
        {
            var uut = new AnimationComponent("test", new FrameProperties(10, 10, TimeSpan.FromSeconds(1), 5)) { IsAnimating = true };

            // We should start out on frame 0.
            Assert.AreEqual(0, uut.FrameIndex);

            // After a call to update with elapsed game time of just .5 seconds, we should still be on frame 0.
            uut.Update(new Mock<IGameObject>().Object, new GameTime(TimeSpan.Zero, TimeSpan.FromSeconds(.5)));
            Assert.AreEqual(0, uut.FrameIndex);

            // Adding another .5 seconds should move to frame 1.
            uut.Update(new Mock<IGameObject>().Object, new GameTime(TimeSpan.Zero, TimeSpan.FromSeconds(.5)));
            Assert.AreEqual(1, uut.FrameIndex);

            // Another 1 second should move to frame 2.
            uut.Update(new Mock<IGameObject>().Object, new GameTime(TimeSpan.Zero, TimeSpan.FromSeconds(1)));
            Assert.AreEqual(2, uut.FrameIndex);
        }
    }
}