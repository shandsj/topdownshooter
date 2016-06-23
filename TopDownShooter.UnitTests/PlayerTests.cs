// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Moq;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Contains unit tests for the <see cref="TopDownShooter.Player"/> class.
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
            var animationComponentManager = new Mock<IAnimationComponentManager>();
            animationComponentManager.Setup(o => o.Play("Walk", It.IsAny<bool>()));
            animationComponentManager.Setup(o => o.Play("Death", It.IsAny<bool>()));

            var components = new IComponent[]
            {
                new Mock<IDashComponent>().Object,
                new Mock<IParticleGeneratorComponent>().Object,
                animationComponentManager.Object
            };

            var uut = new Player(1, new Vector2(42, 42), new Mock<ICollisionSystem>().Object, components);
            uut.Initialize();

            uut.Update(new GameTime());
            animationComponentManager.Verify(o => o.Play("Walk", It.IsAny<bool>()), Times.Never);
            animationComponentManager.Verify(o => o.Play("Death", It.IsAny<bool>()), Times.Never);

            uut.Velocity = new Vector2(42, 42);
            uut.Update(new GameTime());
            animationComponentManager.Verify(o => o.Play("Walk", It.IsAny<bool>()), Times.Once);
            animationComponentManager.Verify(o => o.Play("Death", It.IsAny<bool>()), Times.Never);

            uut.Health = 0;
            uut.Update(new GameTime());
            animationComponentManager.Verify(o => o.Play("Walk", It.IsAny<bool>()), Times.Once);
            animationComponentManager.Verify(o => o.Play("Death", It.IsAny<bool>()), Times.Once);
        }
    }
}