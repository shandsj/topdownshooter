// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameObjectTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Moq;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Contains unit tests for the <see cref="GameObject" /> class.
    /// </summary>
    [TestClass]
    public class GameObjectTests
    {
        /// <summary>
        /// Tests that the game object delegates draw calls to a component.
        /// </summary>
        [TestMethod]
        public void DelegatesDrawCallsToComponents()
        {
            var uut = new TestGameObject(1);
            var spriteBatch = new Mock<ISpriteBatchAdapter>().Object;
            var camera = new Mock<ICamera2DAdapter>().Object;
            var gameTime = new GameTime(TimeSpan.FromSeconds(42), TimeSpan.FromSeconds(5));
            var component = new Mock<IComponent>();
            component.Setup(o => o.Draw(uut, camera, spriteBatch, gameTime));

            uut.Components.Add(component.Object);
            uut.Draw(camera, spriteBatch, gameTime);

            component.Verify(o => o.Draw(uut, camera, spriteBatch, gameTime), Times.Once);
        }

        /////// <summary>
        /////// Tests that the game object delegates unload content calls to a component.
        /////// </summary>
        ////[TestMethod]
        ////public void DelegatesUnloadContentCallsToComponents()
        ////{
        ////    var conentManager = new Mock<IContentManagerAdapter>().Object;

        ////    bool wasMethodCalled = false;
        ////    var component = new Mock<IComponent>();
        ////    component.Setup(method => method.UnloadContent(It.IsAny<IContentManagerAdapter>()))
        ////        .Callback<IContentManagerAdapter>(cm =>
        ////        {
        ////            wasMethodCalled = true;
        ////            Assert.AreSame(conentManager, cm);
        ////        });

        ////    var uut = new TestGameObject(1);
        ////    uut.Components.Add(component.Object);
        ////    uut.UnloadContent(conentManager);

        ////    Assert.IsTrue(wasMethodCalled);
        ////}

        /// <summary>
        /// Tests that the game object delegates load content calls to a component.
        /// </summary>
        [TestMethod]
        public void DelegatesLoadContentCallsToComponents()
        {
            var conentManager = new Mock<IContentManagerAdapter>().Object;

            bool wasMethodCalled = false;
            var component = new Mock<IComponent>();
            component.Setup(method => method.LoadContent(It.IsAny<IContentManagerAdapter>()))
                .Callback<IContentManagerAdapter>(cm =>
                    {
                        wasMethodCalled = true;
                        Assert.AreSame(conentManager, cm);
                    });

            var uut = new TestGameObject(1);
            uut.Components.Add(component.Object);
            uut.LoadContent(conentManager);

            Assert.IsTrue(wasMethodCalled);
        }

        /// <summary>
        /// Tests that the game object delegates update calls to a component.
        /// </summary>
        [TestMethod]
        public void DelegatesUpdateCallsToComponents()
        {
            var uut = new TestGameObject(1);
            var gameTime = new GameTime(TimeSpan.FromSeconds(42), TimeSpan.FromSeconds(5));

            bool wasMethodCalled = false;
            var component = new Mock<IComponent>();
            component.Setup(method => method.Update(It.IsAny<IGameObject>(), It.IsAny<GameTime>()))
                .Callback<IGameObject, GameTime>((go, gt) =>
                    {
                        wasMethodCalled = true;
                        Assert.AreSame(uut, go);
                        Assert.AreEqual(gameTime, gt);
                    });

            uut.Components.Add(component.Object);
            uut.Update(gameTime);

            Assert.IsTrue(wasMethodCalled);
        }
    }
}