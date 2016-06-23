// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BulletProjectileTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.UnitTests.Projectiles
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Moq;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Projectiles;

    /// <summary>
    /// Contains unit tests for the <see cref="TopDownShooter.Projectiles.LongRangeProjectile" /> class.
    /// </summary>
    [TestClass]
    public class BulletProjectileTests
    {
        /// <summary>
        /// Tests that the velocity vector is normalized from the direction passed in during construction.
        /// </summary>
        [TestMethod]
        public void NormalizesVelocityVectorWhenInitialized()
        {
            var uut = new LongRangeProjectile(42, 43, Vector2.Zero, new Vector2(0, 42), new Mock<ICollisionSystem>().Object, new IComponent[] { });
            uut.Initialize();
            Assert.AreEqual(50f, uut.Speed);
            Assert.AreEqual(new Vector2(0, 50), uut.Velocity);
        }
    }
}