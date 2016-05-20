using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TopDownShooter.Engine.UnitTests
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Contains unit tests for the <see cref="Animation"/> class.
    /// </summary>
    [TestClass]
    public class AnimationTests
    {
        /// <summary>
        /// Tests whether the frame index gets updated when the update method is called.
        /// </summary>
        [TestMethod]
        public void UpdatesFrameIndexWhenUpdateIsCalledAndIsAnimating()
        {
            var uut = new Animation("test", new FrameProperties(10, 10, TimeSpan.FromSeconds(1), 5)) { IsAnimating = true };

            // We should start out on frame 0.
            Assert.AreEqual(0, uut.FrameIndex);

            // After a call to update with elapsed game time of just .5 seconds, we should still be on frame 0.
            uut.Update(new GameTime(TimeSpan.Zero, TimeSpan.FromSeconds(.5)));
            Assert.AreEqual(0, uut.FrameIndex);

            // Adding another .5 seconds should move to frame 1.
            uut.Update(new GameTime(TimeSpan.Zero, TimeSpan.FromSeconds(.5)));
            Assert.AreEqual(1, uut.FrameIndex);

            // Another 1 second should move to frame 2.
            uut.Update(new GameTime(TimeSpan.Zero, TimeSpan.FromSeconds(1)));
            Assert.AreEqual(2, uut.FrameIndex);
        }
    }
}
