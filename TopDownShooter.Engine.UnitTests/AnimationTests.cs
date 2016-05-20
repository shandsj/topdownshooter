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
    using TopDownShooter.Engine.Fakes;

    /// <summary>
    /// Contains unit tests for the <see cref="Animation" /> class.
    /// </summary>
    [TestClass]
    public class AnimationTests
    {
        /// <summary>
        /// Tests whether the animation is drawn in the sprite batch when draw is called.
        /// </summary>
        [TestMethod]
        public void DrawsInSpriteBatchWhenDrawIsCalled()
        {
            bool wasDrawCalled = false;
            var spriteBatch = new StubISpriteBatchAdapter
            {
                DrawTexture2DVector2NullableOfRectangleColorSingleVector2SingleSpriteEffectsSingle =
                    (texture, position, sourceRectange, color, rotation, origin, scale, effects, layerDepth) =>
                        {
                            wasDrawCalled = true;
                            Assert.AreEqual(new Vector2(42, 42), position);
                            Assert.AreEqual(new Rectangle(0, 0, 10, 10), sourceRectange);
                            Assert.AreEqual(Color.White, color);
                            Assert.AreEqual(42, rotation);
                            Assert.AreEqual(new Vector2(5, 5), origin);
                            Assert.AreEqual(1, scale);
                            Assert.AreEqual(SpriteEffects.FlipVertically, effects);
                            Assert.AreEqual(0, layerDepth);
                        }
            };

            var uut = new Animation("test", new FrameProperties(10, 10, TimeSpan.FromSeconds(1), 5))
            {
                Position = new Vector2(42, 42),
                Rotation = 42,
                SpriteEffect = SpriteEffects.FlipVertically
            };

            uut.Draw(spriteBatch, new GameTime());
            Assert.IsTrue(wasDrawCalled);
        }

        /// <summary>
        /// Tests whether the frame index gets updated when the update method is called.
        /// </summary>
        [TestMethod]
        public void UpdatesFrameIndexWhenUpdateIsCalledAndIsAnimating()
        {
            var uut = new Animation("test", new FrameProperties(10, 10, TimeSpan.FromSeconds(1), 5)) { IsAnimating = true };

            // We should start out on frame 0.
            Assert.AreEqual(0, uut.FrameIndex);

            // After a call to update with elapsed game time of just .5 seconds, we should still be on frame 0.
            uut.Update(new GameTime(TimeSpan.Zero, TimeSpan.FromSeconds(.5)));
            Assert.AreEqual(0, uut.FrameIndex);

            // Adding another .5 seconds should move to frame 1.
            uut.Update(new GameTime(TimeSpan.Zero, TimeSpan.FromSeconds(.5)));
            Assert.AreEqual(1, uut.FrameIndex);

            // Another 1 second should move to frame 2.
            uut.Update(new GameTime(TimeSpan.Zero, TimeSpan.FromSeconds(1)));
            Assert.AreEqual(2, uut.FrameIndex);
        }
    }
}