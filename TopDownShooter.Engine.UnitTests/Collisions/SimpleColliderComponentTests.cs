// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleColliderComponentTests.cs" company="PlaceholderCompany">
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
    /// Contains unit tests for the <see cref="SimpleColliderComponent" /> class.
    /// </summary>
    [TestClass]
    public class SimpleColliderComponentTests
    {
        /// <summary>
        /// Tests that a collision is not detected with other collider components.
        /// </summary>
        [TestMethod]
        public void DoesNotDetectCollisionsWithAnotherColliderComponent()
        {
            var gameObject42 = new Mock<IGameObject>();
            gameObject42.SetupGet(go => go.ProjectedBounds).Returns(new Rectangle(5, 5, 10, 10));

            var gameObject43 = new Mock<IGameObject>();
            gameObject43.SetupGet(go => go.ProjectedBounds).Returns(new Rectangle(50, 50, 10, 10));

            var collisionSystem = new Mock<ICollisionSystem>();
            collisionSystem.Setup(cs => cs.GetGameObject(It.IsAny<int>())).Returns<int>(id =>
            {
                switch (id)
                {
                    case 42:
                        return gameObject42.Object;

                    case 43:
                        return gameObject43.Object;
                }

                Assert.Fail("This should have returned on of the other game objects");
                return null;
            });

            var colliderComponent43 = new Mock<IColliderComponent>();
            colliderComponent43.SetupGet(cc => cc.GameObjectId).Returns(43);

            var uut = new SimpleColliderComponent(42, collisionSystem.Object);
            Assert.IsFalse(uut.IsCollision(colliderComponent43.Object));
        }

        /// <summary>
        /// Tests that a collision is detected with other collider components.
        /// </summary>
        [TestMethod]
        public void DetectsCollisionsWithAnotherColliderComponent()
        {
            var gameObject42 = new Mock<IGameObject>();
            gameObject42.SetupGet(go => go.ProjectedBounds).Returns(new Rectangle(5, 5, 10, 10));

            var gameObject43 = new Mock<IGameObject>();
            gameObject43.SetupGet(go => go.ProjectedBounds).Returns(new Rectangle(4, 4, 10, 10));

            var collisionSystem = new Mock<ICollisionSystem>();
            collisionSystem.Setup(cs => cs.GetGameObject(It.IsAny<int>())).Returns<int>(id =>
                {
                    switch (id)
                    {
                        case 42:
                            return gameObject42.Object;

                        case 43:
                            return gameObject43.Object;
                    }

                    Assert.Fail("This should have returned on of the other game objects");
                    return null;
                });

            var colliderComponent43 = new Mock<IColliderComponent>();
            colliderComponent43.SetupGet(cc => cc.GameObjectId).Returns(43);

            var uut = new SimpleColliderComponent(42, collisionSystem.Object);
            Assert.IsTrue(uut.IsCollision(colliderComponent43.Object));
        }
    }
}