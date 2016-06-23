// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerColliderComponentTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Moq;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Levels;

    /// <summary>
    /// Contains unit tests for the <see cref="TopDownShooter.PlayerColliderComponent" /> class.
    /// </summary>
    [TestClass]
    public class PlayerColliderComponentTests
    {
        /// <summary>
        /// Tests that the velocity on the parent game object is modified when colliding with a tile.
        /// </summary>
        [TestMethod]
        public void ModifiesVelocityWhenCollidingWithABlockingTile()
        {
            var player = new Mock<IPlayer>();
            player.SetupSet(o => o.Velocity = Vector2.Zero);
            var tile = new Mock<ITile>();
            tile.Setup(o => o.TileInteractionType).Returns(TileInteractionType.Blocking);

            var collisionSystem = new Mock<ICollisionSystem>();
            collisionSystem.Setup(cs => cs.GetGameObject(It.IsAny<int>())).Returns<int>(id =>
                {
                    if (id == 42)
                    {
                        return player.Object;
                    }

                    return tile.Object;
                });

            var uut = new PlayerColliderComponent(42, collisionSystem.Object);
            uut.Collide(new Mock<IColliderComponent>().Object, new Microsoft.Xna.Framework.GameTime());

            player.VerifySet(o => o.Velocity = Vector2.Zero);
        }

        /// <summary>
        /// Tests that collisions are check when the component is updated and the parent game object has a non-zero velocity.
        /// </summary>
        [TestMethod]
        public void ChecksForCollisionsWhenUpdatedAndGameObjectHasNonZeroVelocity()
        {
            var collisionSystem = new Mock<ICollisionSystem>();
            var uut = new PlayerColliderComponent(42, collisionSystem.Object);
            collisionSystem.Setup(cs => cs.CheckCollisions(uut, It.IsAny<GameTime>()));

            var gameObject = new Mock<IGameObject>();
            gameObject.SetupGet(go => go.Velocity).Returns(new Vector2(42, 42));
            uut.Update(gameObject.Object, new GameTime());

            collisionSystem.Verify(o => o.CheckCollisions(uut, It.IsAny<GameTime>()), Times.Once);
        }
    }
}