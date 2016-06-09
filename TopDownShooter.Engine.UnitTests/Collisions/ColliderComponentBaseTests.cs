﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColliderComponentBaseTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests.Collisions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Moq;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Contains unit tests for the <see cref="ColliderComponentBase" /> class.
    /// </summary>
    [TestClass]
    public class ColliderComponentBaseTests
    {
        /// <summary>
        /// Tests that the base collider component checks for collisions on a moving game object.
        /// </summary>
        [TestMethod]
        public void ChecksForCollisionsWhenVelocityIsNotZero()
        {
            var collisionSystem = new Mock<ICollisionSystem>();
            var uut = new TestColliderComponent(42, collisionSystem.Object);
            collisionSystem.Setup(cs => cs.CheckCollisions(It.IsAny<IColliderComponent>(), It.IsAny<GameTime>()))
                .Callback<IColliderComponent, GameTime>((cc, gameTime) =>
                {
                    Assert.AreSame(uut, cc);
                });

            var gameObject = new Mock<IGameObject>();
            uut.Update(gameObject.Object, new GameTime());
            collisionSystem.Verify(o => o.CheckCollisions(It.IsAny<IColliderComponent>(), It.IsAny<GameTime>()), Times.Never);

            gameObject.Setup(go => go.Velocity).Returns(new Vector2(42, 42));
            uut.Update(gameObject.Object, new GameTime());
            collisionSystem.Verify(o => o.CheckCollisions(It.IsAny<IColliderComponent>(), It.IsAny<GameTime>()), Times.Once);
        }
    }
}