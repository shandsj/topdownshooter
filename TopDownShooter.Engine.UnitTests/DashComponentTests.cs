// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DashComponentTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Moq;

    /// <summary>
    /// Contains unit tests for the <see cref="DashComponent" /> class.
    /// </summary>
    [TestClass]
    public class DashComponentTests
    {
        /// <summary>
        /// Tests that a dash is started when a dash message is received.
        /// </summary>
        [TestMethod]
        public void StartsDashingWhenReceivingADashMessage()
        {
            var gameTime = new GameTime(TimeSpan.FromSeconds(10), TimeSpan.Zero);
            var uut = new DashComponent();
            Assert.IsFalse(uut.IsDashing(gameTime));

            uut.ReceiveMessage(
                new Mock<IGameObject>().Object,
                new Message(MessageType.Dash),
                gameTime);

            Assert.IsTrue(uut.IsDashing(new GameTime(TimeSpan.FromSeconds(10.1), TimeSpan.Zero)));
        }

        /// <summary>
        /// Tests that the game object's velocity is updated when Update() is called and a dash is occuring.
        /// </summary>
        [TestMethod]
        public void UpdatesVelocityWhenDashingAndUpdating()
        {
            var gameTime = new GameTime(TimeSpan.FromSeconds(10), TimeSpan.Zero);
            var uut = new DashComponent();

            var gameObject = new Mock<IGameObject>();
            gameObject.Setup(o => o.Velocity).Returns(new Vector2(1, 1));
            gameObject.SetupSet(o => o.Velocity = new Vector2(5, 5));

            uut.TryStartDash(gameObject.Object, gameTime);

            uut.Update(gameObject.Object, new GameTime(TimeSpan.FromSeconds(10.1), TimeSpan.Zero));

            Assert.AreEqual(5f, uut.SpeedFactor);
            gameObject.VerifySet(o => o.Velocity = new Vector2(5, 5));
        }
    }
}