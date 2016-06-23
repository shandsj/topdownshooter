// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BulletProjectileColliderComponentTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.UnitTests.Projectiles
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Levels;
    using TopDownShooter.Projectiles;

    /// <summary>
    /// Contains unit tests for the <see cref="TopDownShooter.Projectiles.ProjectileColliderComponent" /> class.
    /// </summary>
    [TestClass]
    public class BulletProjectileColliderComponentTests
    {
        /// <summary>
        /// Tests that the player health is depleted and the collider is deregistered from the collision system when a player collision occurs.
        /// </summary>
        [TestMethod]
        public void UpdatesGameStateWhenCollidingWithPlayer()
        {
            var otherPlayer = new Mock<IPlayer>();
            otherPlayer.Setup(o => o.Health).Returns(1);
            otherPlayer.SetupSet(o => o.Health = 0);

            var projectileParent = new Mock<IPlayer>();
            projectileParent.Setup(o => o.KillCount).Returns(0);
            projectileParent.SetupSet(o => o.KillCount = 1);

            var collisionSystem = new Mock<ICollisionSystem>();
            collisionSystem.Setup(o => o.GetGameObject(43)).Returns(projectileParent.Object);
            collisionSystem.Setup(o => o.GetGameObject(44)).Returns(otherPlayer.Object);

            var otherCollider = new Mock<IColliderComponent>();
            otherCollider.Setup(o => o.GameObjectId).Returns(44);

            var uut = new ProjectileColliderComponent(42, 43, collisionSystem.Object);
            uut.Collide(otherCollider.Object, new Microsoft.Xna.Framework.GameTime());

            otherPlayer.VerifySet(o => o.Health = 0);
            projectileParent.VerifySet(o => o.KillCount = 1);
        }

        /// <summary>
        /// Tests that the collider unregisters from the collider system when colliding with a tile.
        /// </summary>
        [TestMethod]
        public void DestroysGameObjectmWhenCollidingWithTile()
        {
            var tile = new Mock<ITile>();
            tile.Setup(o => o.Destroy());

            var collisionSystem = new Mock<ICollisionSystem>();
            collisionSystem.Setup(cs => cs.GetGameObject(It.IsAny<int>())).Returns(tile.Object);

            var uut = new ProjectileColliderComponent(42, 43, collisionSystem.Object);
            uut.Collide(new Mock<IColliderComponent>().Object, new Microsoft.Xna.Framework.GameTime());

            tile.Verify(o => o.Destroy());
        }
    }
}