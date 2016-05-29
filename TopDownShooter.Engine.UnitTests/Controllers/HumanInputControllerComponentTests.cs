﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HumanInputControllerComponentTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.UnitTests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Moq;
    using TopDownShooter.Engine.Adapters;
    using TopDownShooter.Engine.Controllers;

    /// <summary>
    /// Contains unit tests for the <see cref="HumanInputControllerComponent"/> class.
    /// </summary>
    [TestClass]
    public class HumanInputControllerComponentTests
    {
        /// <summary>
        /// Tests that the controller moves down when the down key or S keys are down.
        /// </summary>
        [TestMethod]
        public void MovesDownWhenSOrDownKeysAreDown()
        {
            var keyboard = new Mock<IKeyboardAdapter>();
            var uut = new HumanInputControllerComponent(keyboard.Object);

            keyboard.Setup(k => k.GetState()).Returns(default(KeyboardState));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsFalse(uut.MoveDown());

            keyboard.Setup(k => k.GetState()).Returns(new KeyboardState(Keys.Down));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsTrue(uut.MoveDown());

            keyboard.Setup(k => k.GetState()).Returns(default(KeyboardState));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsFalse(uut.MoveDown());

            keyboard.Setup(k => k.GetState()).Returns(new KeyboardState(Keys.S));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsTrue(uut.MoveDown());
        }

        /// <summary>
        /// Tests that the controller moves left when the A key or Left keys are down.
        /// </summary>
        [TestMethod]
        public void MovesLeftWhenAOrLeftKeysAreDown()
        {
            var keyboard = new Mock<IKeyboardAdapter>();
            var uut = new HumanInputControllerComponent(keyboard.Object);

            keyboard.Setup(k => k.GetState()).Returns(default(KeyboardState));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsFalse(uut.MoveLeft());

            keyboard.Setup(k => k.GetState()).Returns(new KeyboardState(Keys.Left));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsTrue(uut.MoveLeft());

            keyboard.Setup(k => k.GetState()).Returns(default(KeyboardState));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsFalse(uut.MoveLeft());

            keyboard.Setup(k => k.GetState()).Returns(new KeyboardState(Keys.A));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsTrue(uut.MoveLeft());
        }

        /// <summary>
        /// Tests that the controller moves right when the right key or D keys are down.
        /// </summary>
        [TestMethod]
        public void MovesRightWhenDOrRightKeysAreDown()
        {
            var keyboard = new Mock<IKeyboardAdapter>();
            var uut = new HumanInputControllerComponent(keyboard.Object);

            keyboard.Setup(k => k.GetState()).Returns(default(KeyboardState));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsFalse(uut.MoveRight());

            keyboard.Setup(k => k.GetState()).Returns(new KeyboardState(Keys.Right));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsTrue(uut.MoveRight());

            keyboard.Setup(k => k.GetState()).Returns(default(KeyboardState));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsFalse(uut.MoveRight());

            keyboard.Setup(k => k.GetState()).Returns(new KeyboardState(Keys.D));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsTrue(uut.MoveRight());
        }

        /// <summary>
        /// Tests that the controller moves up when the up key or W keys are down.
        /// </summary>
        [TestMethod]
        public void MovesUpWhenWOrUpKeysAreDown()
        {
            var keyboard = new Mock<IKeyboardAdapter>();
            var uut = new HumanInputControllerComponent(keyboard.Object);

            keyboard.Setup(k => k.GetState()).Returns(default(KeyboardState));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsFalse(uut.MoveUp());

            keyboard.Setup(k => k.GetState()).Returns(new KeyboardState(Keys.Up));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsTrue(uut.MoveUp());

            keyboard.Setup(k => k.GetState()).Returns(default(KeyboardState));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsFalse(uut.MoveUp());

            keyboard.Setup(k => k.GetState()).Returns(new KeyboardState(Keys.W));
            uut.Update(new Mock<IGameObject>().Object, new GameTime());
            Assert.IsTrue(uut.MoveUp());
        }
    }
}