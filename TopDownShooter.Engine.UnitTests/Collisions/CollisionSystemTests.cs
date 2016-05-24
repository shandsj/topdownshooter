// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollisionSystemTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests.Collisions
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TopDownShooter.Engine.Collisions;

    /// <summary>
    /// Contains unit tests for the <see cref="CollisionSystem" /> class.
    /// </summary>
    [TestClass]
    public class CollisionSystemTests
    {
        /// <summary>
        /// Tests that a collider is notified when a collision is detected.
        /// </summary>
        [TestMethod]
        public void CallsCollideWhenACollisionIsDetected()
        {
            var other = new Mock<IColliderComponent>();

            bool wasCollideCalled = false;
            var collider = new Mock<IColliderComponent>();
            collider.Setup(c => c.IsCollision(It.IsAny<IColliderComponent>())).Returns(true);
            collider.Setup(c => c.Collide(It.IsAny<IColliderComponent>())).Callback<IColliderComponent>(c =>
                {
                    wasCollideCalled = true;
                    Assert.AreEqual(other.Object, c);
                });

            var uut = new CollisionSystem();
            uut.Register(42, new Mock<IGameObject>().Object, other.Object);
            uut.CheckCollisions(collider.Object);

            Assert.IsTrue(wasCollideCalled);
        }

        /// <summary>
        /// Tests that game objects can be retrieved with their identifier.
        /// </summary>
        [TestMethod]
        public void CanGetGameObjects()
        {
            var gameObject = new Mock<IGameObject>();
            var colliderComponent = new Mock<IColliderComponent>();

            var uut = new CollisionSystem();
            uut.Register(42, gameObject.Object, colliderComponent.Object);

            var actual = uut.GetGameObject(42);
            Assert.AreSame(gameObject.Object, actual);
        }

        /// <summary>
        /// Tests that game objects and collider components can be registered and unregistered.
        /// </summary>
        [TestMethod]
        public void RegistersAndUnregistersGameObjectsAndColliderComponents()
        {
            var gameObject = new Mock<IGameObject>();
            var colliderComponent = new Mock<IColliderComponent>();

            var uut = new CollisionSystem();
            uut.Register(42, gameObject.Object, colliderComponent.Object);

            Assert.AreEqual(1, uut.Colliders.Count());
            Assert.AreSame(colliderComponent.Object, uut.Colliders.First());
            Assert.AreEqual(1, uut.GameObjects.Count());
            Assert.AreSame(gameObject.Object, uut.GameObjects.First());

            uut.Unregister(42);
            Assert.AreEqual(0, uut.Colliders.Count());
            Assert.AreEqual(0, uut.GameObjects.Count());
        }
    }
}