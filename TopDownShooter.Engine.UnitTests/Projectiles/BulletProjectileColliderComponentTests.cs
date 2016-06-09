// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BulletProjectileColliderComponentTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests.Projectiles
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Levels;
    using TopDownShooter.Engine.Projectiles;

    /// <summary>
    /// Contains unit tests for the <see cref="BulletProjectileColliderComponent" /> class.
    /// </summary>
    [TestClass]
    public class BulletProjectileColliderComponentTests
    {
        /// <summary>
        /// Tests that the player health is depleted and the collider is deregistered from the collision system when a player collision occurs.
        /// </summary>
        [TestMethod]
        public void DepletesPlayerHealthAndDeregistersFromCollisionSystemWhenCollidingWithPlayer()
        {
            bool wasSetHealthCalled = false;
            var player = new Mock<IPlayer>();
            player.SetupGet(p => p.Health).Returns(1);
            player.SetupSet(p => p.Health = It.IsAny<int>()).Callback<int>(health =>
                {
                    wasSetHealthCalled = true;
                    Assert.AreEqual(0, health);
                });

            bool wasUnregisterCalled = false;
            var collisionSystem = new Mock<ICollisionSystem>();

            // Set up the collision system to return a false bullet parent different than the collided player for
            // construction of the uut.
            collisionSystem.Setup(cs => cs.GetGameObject(It.IsAny<int>())).Returns(new Mock<IPlayer>().Object);
            collisionSystem.Setup(cs => cs.Unregister(It.IsAny<int>())).Callback<int>(id =>
                {
                    wasUnregisterCalled = true;
                    Assert.AreEqual(43, id);
                });

            var otherCollider = new Mock<IColliderComponent>();
            otherCollider.Setup(c => c.GameObjectId).Returns(42);

            var uut = new BulletProjectileColliderComponent(43, 44, collisionSystem.Object);

            // Now set up the collision system to return the player constructed above.
            collisionSystem.Setup(cs => cs.GetGameObject(It.IsAny<int>())).Callback<int>(id => { Assert.AreEqual(42, id); }).Returns(player.Object);

            uut.Collide(otherCollider.Object, new Microsoft.Xna.Framework.GameTime());

            Assert.IsTrue(wasSetHealthCalled);
            Assert.IsTrue(wasUnregisterCalled);
        }

        /// <summary>
        /// Tests that the collider unregisters from the collider system when colliding with a tile.
        /// </summary>
        [TestMethod]
        public void UnregistersFromColliderSystemWhenCollidingWithTile()
        {
            bool wasUnregisterCalled = false;
            var tile = new Mock<ITile>();
            var collisionSystem = new Mock<ICollisionSystem>();
            collisionSystem.Setup(cs => cs.GetGameObject(It.IsAny<int>())).Returns(tile.Object);
            collisionSystem.Setup(cs => cs.Unregister(It.IsAny<int>())).Callback<int>((id) =>
                {
                    wasUnregisterCalled = true;
                    Assert.AreEqual(42, id);
                });

            var uut = new BulletProjectileColliderComponent(42, 43, collisionSystem.Object);
            uut.Collide(new Mock<IColliderComponent>().Object, new Microsoft.Xna.Framework.GameTime());
            Assert.IsTrue(wasUnregisterCalled);
        }
    }
}