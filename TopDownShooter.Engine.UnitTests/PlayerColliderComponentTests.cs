// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerColliderComponentTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Moq;

    /// <summary>
    /// Contains unit tests for the <see cref="PlayerColliderComponent" /> class.
    /// </summary>
    [TestClass]
    public class PlayerColliderComponentTests
    {
        /// <summary>
        /// Tests that the velocity on the parent game object is modified when colliding with a tile.
        /// </summary>
        [TestMethod]
        public void ModifiesVelocityWhenCollidingWithATile()
        {
            bool wasSetVelocityCalled = false;

            var tile = new Mock<ITile>();
            tile.SetupSet(t => t.Velocity = It.IsAny<Vector2>()).Callback<Vector2>(vector =>
                {
                    wasSetVelocityCalled = true;
                    Assert.AreEqual(new Vector2(0, 0), vector);
                });

            var collisionSystem = new Mock<ICollisionSystem>();
            collisionSystem.Setup(cs => cs.GetGameObject(It.IsAny<int>())).Returns(tile.Object);

            var uut = new PlayerColliderComponent(42, collisionSystem.Object);
            uut.Collide(new Mock<IColliderComponent>().Object);
            Assert.IsTrue(wasSetVelocityCalled);
        }

        /// <summary>
        /// Tests that collisions are check when the component is updated and the parent game object has a non-zero velocity.
        /// </summary>
        [TestMethod]
        public void ChecksForCollisionsWhenUpdatedAndGameObjectHasNonZeroVelocity()
        {
            var collisionSystem = new Mock<ICollisionSystem>();
            var uut = new PlayerColliderComponent(42, collisionSystem.Object);

            bool wasCheckCollisionsCalled = false;
            collisionSystem.Setup(cs => cs.CheckCollisions(It.IsAny<IColliderComponent>())).Callback<IColliderComponent>(cc =>
                {
                    wasCheckCollisionsCalled = true;
                    Assert.AreSame(uut, cc);
                });

            var gameObject = new Mock<IGameObject>();
            gameObject.SetupGet(go => go.Velocity).Returns(new Vector2(42, 42));
            uut.Update(gameObject.Object, new GameTime());
            Assert.IsTrue(wasCheckCollisionsCalled);
        }
    }
}