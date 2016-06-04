// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerInventoryComponentTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Inventory;

    /// <summary>
    /// Tests Container for <see cref="PlayerInventoryComponent" />
    /// </summary>
    [TestClass]
    public class PlayerInventoryComponentTests
    {
        /// <summary>
        /// Tests that an Item is picked up by listening for a Component Message
        /// </summary>
        [TestMethod]
        public void TestCollisionTriggersBroadcast()
        {
            var mockItem = new Mock<IGameItem>();
            var wasBroadcastFired = false;

            var mockPlayerObject = new Mock<IGameObject>();
            mockPlayerObject.Setup(o => o.BroadcastMessage(It.IsAny<ComponentMessage>())).Callback((ComponentMessage message) =>
                {
                    wasBroadcastFired = true;
                    Assert.AreEqual(MessageType.ItemPickup, message.MessageType);
                    Assert.AreEqual(mockItem.Object, message.MessageDetails);
                });

            var collisionSystem = new Mock<ICollisionSystem>();

            collisionSystem.Setup(col => col.GetGameObject(It.Is<int>(o => o == 43))).Returns(mockItem.Object);
            collisionSystem.Setup(col => col.GetGameObject(It.Is<int>(o => o == 42))).Returns(mockPlayerObject.Object);

            var uut = new PlayerColliderComponent(42, collisionSystem.Object);
            var itemCollider = new SimpleColliderComponent(43, collisionSystem.Object);

            uut.Collide(itemCollider);

            Assert.AreEqual(true, wasBroadcastFired);
        }

        /// <summary>
        /// Tests that an Item is picked up by listening for a Component Message
        /// </summary>
        [TestMethod]
        public void TestPickupOnlyOneBullet()
        {
            var collisionSystem = new Mock<ICollisionSystem>();

            IEnumerable<IGameItem> gameObjects = new GameItemFactory()
                .SpawnRandomBulletItems(3, collisionSystem.Object, 0, 0, 0, 0);

            var playerInventoryComponent = new PlayerInventoryComponent(collisionSystem.Object);

            foreach (var gameObject in gameObjects)
            {
                playerInventoryComponent.ReceiveMessage(new Mock<IGameObject>().Object, new ComponentMessage(MessageType.ItemPickup, gameObject));
            }

            Assert.AreEqual(1, gameObjects.Count(obj => (obj as IGameItem).IsPickedUp));
            Assert.AreEqual(2, gameObjects.Count(obj => !(obj as IGameItem).IsPickedUp));
        }

        /// <summary>
        /// Tests that an Item is picked up by listening for a Component Message
        /// </summary>
        [TestMethod]
        public void TestSingleItemIsPickedUp()
        {
            var mockItem = new Mock<IGameItem>();
            var isPickedUp = false;

            mockItem.Setup(item => item.Pickup()).Returns(() =>
                {
                    isPickedUp = true;
                    return mockItem.Object;
                });

            var playerInventoryComponent = new PlayerInventoryComponent(new Mock<ICollisionSystem>().Object);
            playerInventoryComponent.ReceiveMessage(new Mock<IGameObject>().Object, new ComponentMessage(MessageType.ItemPickup, mockItem.Object));

            Assert.AreEqual(true, isPickedUp);
        }
    }
}