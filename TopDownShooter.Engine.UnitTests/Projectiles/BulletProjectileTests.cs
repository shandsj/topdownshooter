// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BulletProjectileTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests.Projectiles
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Moq;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Projectiles;

    /// <summary>
    /// Contains unit tests for the <see cref="BulletProjectile" /> class.
    /// </summary>
    [TestClass]
    public class BulletProjectileTests
    {
        /// <summary>
        /// Tests that the velocity vector is normalized from the direction passed in during construction.
        /// </summary>
        [TestMethod]
        public void NormalizesVelocityVectorWhenConstructed()
        {
            var uut = new BulletProjectile(42, 43, Vector2.Zero, new Vector2(0, 42), new Mock<ICollisionSystem>().Object, new IComponent[] { });

            Assert.AreEqual(50f, uut.Speed);
            Assert.AreEqual(new Vector2(0, 50), uut.Velocity);
        }
    }
}