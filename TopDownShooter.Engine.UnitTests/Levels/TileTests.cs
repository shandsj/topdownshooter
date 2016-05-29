// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TileTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests.Levels
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Moq;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Levels;

    /// <summary>
    /// Contains unit tests for the <see cref="Tile" /> class.
    /// </summary>
    [TestClass]
    public class TileTests
    {
        /// <summary>
        /// Tests that the tile registers with the collision system when initialized with a blocking tile interaction type.
        /// </summary>
        [TestMethod]
        public void RegistersWithCollisionSystemWhenInitializedWithABlockingTileInteractionType()
        {
            var collisionSystem = new Mock<ICollisionSystem>();
            var uut = new Tile(42, collisionSystem.Object, TileInteractionType.Blocking, null, Vector2.Zero, Point.Zero, 42, 42, Vector2.Zero);

            bool wasRegisterCalled = false;
            collisionSystem.Setup(cs => cs.Register(
                It.IsAny<int>(),
                It.IsAny<IGameObject>(),
                It.IsAny<IColliderComponent>()))
                .Callback<int, IGameObject, IColliderComponent>(
                    (id, gameObject, colliderComponent) =>
                        {
                            wasRegisterCalled = true;
                            Assert.AreEqual(42, id);
                            Assert.AreEqual(uut, gameObject);
                            Assert.IsInstanceOfType(colliderComponent, typeof(ColliderComponentBase));
                        });

            uut.Initialize();
            Assert.IsTrue(wasRegisterCalled);
        }

        /// <summary>
        /// Tests that the tile draws in the sprite batch.
        /// </summary>
        [TestMethod]
        public void DrawsInTheSpriteBatchWhenDrawIsCalled()
        {
            bool wasDrawCalled = false;
            var spriteBatch = new Mock<ISpriteBatchAdapter>();
            spriteBatch.Setup(sb => sb.Draw(
                It.IsAny<Texture2D>(),
                It.IsAny<Vector2>(),
                It.IsAny<Rectangle?>(),
                It.IsAny<Color>(),
                It.IsAny<float>(),
                It.IsAny<Vector2>(),
                It.IsAny<float>(),
                It.IsAny<SpriteEffects>(),
                It.IsAny<float>()))
                .Callback<Texture2D, Vector2, Rectangle?, Color, float, Vector2, float, SpriteEffects, float>(
                    (texture, position, rectangle, color, rotation, origin, scale, effect, layerDepth) =>
                        {
                            wasDrawCalled = true;

                            // Only because we can't mock a Texture2D :(
                            Assert.IsNull(texture);

                            Assert.AreEqual(new Vector2(42, 42), position);
                            Assert.AreEqual(new Rectangle(10, 10, 42, 42), rectangle);
                            Assert.AreEqual(Color.White, color);
                            Assert.AreEqual(0f, rotation);
                            Assert.AreEqual(Vector2.Zero, origin);
                            Assert.AreEqual(1f, scale);
                            Assert.AreEqual(SpriteEffects.None, effect);
                            Assert.AreEqual(0f, layerDepth);
                        });

            var uut = new Tile(42, new Mock<ICollisionSystem>().Object, TileInteractionType.Blocking, null, new Vector2(42,42), Point.Zero, 42, 42, new Vector2(10,10));
            uut.Draw(spriteBatch.Object, new GameTime());
            Assert.IsTrue(wasDrawCalled);
        }
    }
}