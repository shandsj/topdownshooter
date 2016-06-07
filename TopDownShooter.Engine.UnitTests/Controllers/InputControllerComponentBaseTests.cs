// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputControllerComponentBaseTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Moq;
    using TopDownShooter.Engine.Controllers;

    /// <summary>
    /// Contains unit tests for the <see cref="InputControllerComponentBase" /> class.
    /// </summary>
    [TestClass]
    public class InputControllerComponentBaseTests
    {
        /// <summary>
        /// Tests that the fire message is broadcasted when fire is pressed.
        /// </summary>
        [TestMethod]
        public void BroadcastsFireMessageWhenFireIsPressedAndUpdated()
        {
            bool wasBroadcastMessageCalled = false;
            var gameObject = new Mock<IGameObject>();
            gameObject.Setup(go => go.BroadcastMessage(It.IsAny<ComponentMessage>(), It.IsAny<GameTime>())).Callback<ComponentMessage>(message =>
                {
                    wasBroadcastMessageCalled = true;
                    Assert.AreEqual(MessageType.Fire, message.MessageType);
                });

            var uut = new TestInputControllerComponent();
            uut.Update(gameObject.Object, new GameTime());
            Assert.IsTrue(wasBroadcastMessageCalled);
        }
    }
}