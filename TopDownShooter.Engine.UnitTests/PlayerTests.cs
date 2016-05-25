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
            var walkAnimation = new Mock<IAnimationComponent>();
            walkAnimation.SetupGet(ac => ac.Label).Returns("Walk");

            bool wasIsAnimatingSet = false;
            bool wasIsRenderedSet = false;
            var deathAnimation = new Mock<IAnimationComponent>();
            deathAnimation.SetupGet(ac => ac.Label).Returns("Death");
            deathAnimation.SetupSet(ac => ac.IsAnimating = It.IsAny<bool>()).Callback(() => wasIsAnimatingSet = true);
            deathAnimation.SetupSet(ac => ac.IsRendered = It.IsAny<bool>()).Callback(() => wasIsRenderedSet = true);

            var uut = new Player(1, new Vector2(42, 42), new Mock<ICollisionSystem>().Object, new[] { walkAnimation.Object, deathAnimation.Object });
            uut.Update(new GameTime());

            Assert.IsFalse(wasIsRenderedSet);
            Assert.IsFalse(wasIsAnimatingSet);
            Assert.AreEqual(2, uut.Components.Count);

            uut.Health = 0;
            uut.Update(new GameTime());

            Assert.IsTrue(wasIsAnimatingSet);
            Assert.IsTrue(wasIsRenderedSet);
            Assert.AreEqual(1, uut.Components.Count);
            Assert.AreSame(deathAnimation.Object, uut.Components.First());
        }
    }
}