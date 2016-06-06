// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SceneControllerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests
{
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
        /// Tests that the content is loaded when preloading a scene.
        /// </summary>
        [TestMethod]
        public void LoadsContentWhenPreloadingAScene()
        {
            var contentManager = new Mock<IContentManagerAdapter>();
            var uut = new SceneController(contentManager.Object);

            var scene = new Mock<IScene>();
            scene.Setup(o => o.LoadContentAsync(contentManager.Object, uut));
            uut.PreloadAsync(scene.Object);

            scene.Verify(o => o.LoadContentAsync(contentManager.Object, uut), Times.AtLeastOnce);
        }
    }
}