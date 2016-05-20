// <copyright file="CameraTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Contains unit tests for the <see cref="Camera"/> class.
    /// </summary>
    [TestClass]
    public class CameraTests
    {
        /// <summary>
        /// Tests that the transform matrix is calculated.
        /// </summary>
        [TestMethod]
        public void CalculatesTransformMatrix()
        {
            var uut = new Camera(new Viewport(5, 5, 10, 10)) { Zoom = 1 };
            Assert.AreEqual(new Rectangle(5, 5, 10, 10), uut.Bounds);

            var expected = new Matrix(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(5, 5, 0, 1));
            Assert.AreEqual(expected, uut.TransformMatrix);
        }
    }
}
