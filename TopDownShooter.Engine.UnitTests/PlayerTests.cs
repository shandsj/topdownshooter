// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Moq;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Contains unit tests for the <see cref="Player"/> class.
    /// </summary>
    [TestClass]
    public class PlayerTests
    {
        /// <summary>
        /// Tests that the death animation is setup correctly when the player health is at zero.
        /// </summary>
        [TestMethod]
        public void AnimatesAndRendersDeathAnimationWhenHealthReachesZero()
        {
            int playMethodCallCount = 0;
            var animationComponentManager = new Mock<IAnimationComponentManager>();

            animationComponentManager.Setup((acm) => acm.Play(It.IsAny<string>(), It.IsAny<bool>())).Callback<string, bool>((s, l) =>
                {
                    ++playMethodCallCount;
                    switch (playMethodCallCount)
                    {
                        case 1:
                            Assert.AreEqual(s, "Walk");
                            break;

                        case 2:
                            Assert.AreEqual(s, "Death");
                            break;

                        default:
                            Assert.Fail("This should not have been called");
                            break;
                    }
                });

            int stopMethodCallCount = 0;
            animationComponentManager.Setup(acm => acm.Stop()).Callback(() => stopMethodCallCount++);

            var uut = new Player(1, new Vector2(42, 42), new Mock<ICollisionSystem>().Object, new[] { animationComponentManager.Object });
            uut.Initialize();

            uut.Update(new GameTime());
            Assert.AreEqual(1, uut.Components.Count);
            Assert.AreEqual(0, playMethodCallCount);
            Assert.AreEqual(1, stopMethodCallCount);

            uut.Velocity = new Vector2(42, 42);
            uut.Update(new GameTime());
            Assert.AreEqual(1, uut.Components.Count);
            Assert.AreEqual(1, playMethodCallCount);
            Assert.AreEqual(1, stopMethodCallCount);

            uut.Health = 0;
            uut.Update(new GameTime());
            Assert.AreEqual(1, uut.Components.Count);
            Assert.AreEqual(2, playMethodCallCount);
            Assert.AreEqual(1, stopMethodCallCount);
        }
    }
}