// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LevelTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests.Levels
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Moq;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Levels;

    /// <summary>
    /// Contains unit tests for the <see cref="Level"/> class.
    /// </summary>
    [TestClass]
    public class LevelTests
    {
        /// <summary>
        /// Tests that the level only draws the tiles in the camera bounds.
        /// </summary>
        [TestMethod]
        public void DrawsOnlyTilesInTheCameraBounds()
        {
            var camera = new Mock<ICamera2DAdapter>();
            camera.Setup(o => o.GetBoundingRectangle()).Returns(new Rectangle(0, 0, 10, 10));
            camera.Setup(o => o.Zoom).Returns(1f);
            camera.Setup(o => o.ScreenToWorld(It.IsIn(0f, 5f, 10f), It.IsIn(0f, 5f, 10f)));

            var spriteBatch = new Mock<ISpriteBatchAdapter>();

            var tmxMap = new Mock<ITmxMapAdapter>();
            tmxMap.Setup(o => o.TileHeight).Returns(5);
            tmxMap.Setup(o => o.TileWidth).Returns(5);

            var tile = new Mock<ITile>();
            tile.Setup(o => o.Draw(camera.Object, spriteBatch.Object, It.IsAny<GameTime>()));

            var tileCollection = new Mock<ITileCollection>();
            tileCollection.Setup(o => o.GetTile(It.IsAny<Vector2>())).Returns(tile.Object);

            var uut = new Level(1, new Mock<ICollisionSystem>().Object, tmxMap.Object, new Mock<ITileFactory>().Object, tileCollection.Object);
            uut.Draw(camera.Object, spriteBatch.Object, new GameTime());

            tile.Verify(o => o.Draw(camera.Object, spriteBatch.Object, It.IsAny<GameTime>()), Times.Exactly(9));
            camera.Verify(o => o.ScreenToWorld(It.IsIn(0f, 5f, 10f), It.IsIn(0f, 5f, 10f)), Times.Exactly(9));
        }
    }
}