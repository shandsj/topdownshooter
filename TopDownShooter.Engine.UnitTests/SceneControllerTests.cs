// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SceneControllerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TopDownShooter.Engine.Adapters;

    /// <summary>
    /// Contains unit tests for the <see cref="SceneController" /> class.
    /// </summary>
    [TestClass]
    public class SceneControllerTests
    {
        /// <summary>
        /// Tests that the previous active scene is destroyed when switching.
        /// </summary>
        [TestMethod]
        public void DestroysPreviousActiveSceneWhenSwitchingScenes()
        {
            bool wasDestroyCalled = false;
            var scene = new Mock<IScene>();
            scene.Setup(s => s.Destroy()).Callback(() => { wasDestroyCalled = true; });

            var uut = new SceneController(new Mock<IContentManagerAdapter>().Object);
            uut.Switch(scene.Object);
            Assert.IsFalse(wasDestroyCalled);

            uut.Switch(new Mock<IScene>().Object);
            Assert.IsTrue(wasDestroyCalled);
        }

        /// <summary>
        /// Tests that the content is unloaded when switching scenes.
        /// </summary>
        [TestMethod]
        public void UnloadsContentWhenSwitchingScenes()
        {
            bool wasUnloadCalled = false;
            var contentManager = new Mock<IContentManagerAdapter>();
            contentManager.Setup(s => s.Unload()).Callback(() => { wasUnloadCalled = true; });

            var uut = new SceneController(contentManager.Object);
            uut.Switch(new Mock<IScene>().Object);
            Assert.IsTrue(wasUnloadCalled);
        }

        /// <summary>
        /// Tests that the scene is initialized and content is loaded when switching scenes.
        /// </summary>
        [TestMethod]
        public void InitializesAndLoadsContentWhenSwitchingScenes()
        {
            bool wasInitializeCalled = false;
            bool wasLoadContentCalled = false;
            var scene = new Mock<IScene>();
            scene.Setup(s => s.Initialize()).Callback(() => { wasInitializeCalled = true; });
            scene.Setup(s => s.LoadContentAsync(It.IsAny<IContentManagerAdapter>(), new Mock<IProgress<int>>().Object)).Callback(() => { wasLoadContentCalled = true; });

            var uut = new SceneController(new Mock<IContentManagerAdapter>().Object);
            uut.Switch(scene.Object);
            Assert.IsTrue(wasInitializeCalled);
            Assert.IsTrue(wasLoadContentCalled);
        }
    }
}