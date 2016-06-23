// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleAiInputControllerComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine;

    /// <summary>
    /// Controller for controlling a simple AI.
    /// </summary>
    public class SimpleAiInputControllerComponent : InputControllerComponentBase
    {
        private readonly Random random;

        private Vector2 direction;

        private bool fire;

        private List<Task> taskList;

        private bool dash;

        private float rotation;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAiInputControllerComponent" /> class.
        /// </summary>
        /// <param name="random">The <see cref="Random" /> object.</param>
        public SimpleAiInputControllerComponent(Random random)
        {
            this.random = random;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SimpleAiInputControllerComponent" /> class.
        /// </summary>
        ~SimpleAiInputControllerComponent()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the direction vector.
        /// </summary>
        public override Vector2 MovementDirection => this.direction;

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        public override float Rotation => this.rotation;

        /// <summary>
        /// Destroys the component.
        /// </summary>
        public override void Destroy()
        {
            this.Dispose();
        }

        /// <summary>
        /// Gets a value indicating whether a fire was requested.
        /// </summary>
        /// <returns>True if the action was requested; false otherwise.</returns>
        public override bool Fire()
        {
            return this.fire;
        }

        /// <summary>
        /// Gets a value indicating whether a dash was requested.
        /// </summary>
        /// <returns>True if the action was requested; false otherwise.</returns>
        public override bool Dash()
        {
            return this.dash;
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public override void Initialize()
        {
            this.taskList = new List<Task>();
            this.taskList.AddRange(new[]
            {
                Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            this.SleepRandomly();
                            this.direction = new Vector2((float)this.random.NextDouble(-1, 1), (float)this.random.NextDouble(-1, 1));
                        }
                    }),
                                Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            this.SleepRandomly();
                            this.rotation = (float)this.random.NextDouble(0, Math.PI * 2);
                        }
                    }),
                Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            this.SleepRandomly();

                            this.fire = !this.fire;
                        }
                    }),
                Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            this.SleepRandomly();

                            this.dash = !this.dash;
                        }
                    })
            });
        }

        /// <summary>
        /// Cleans up any resources allocated during execution
        /// </summary>
        protected override void ManagedDispose()
        {
            this.taskList.ForEach(o => o.Dispose());
            this.taskList.Clear();
        }

        /// <summary>
        /// Sleeps a random amount of time between 0 and 1.5 seconds.
        /// </summary>
        /// <remarks>No one will get passed this sweet ai!</remarks>
        private void SleepRandomly()
        {
            int randomTimer = this.random.Next(0, 1500);
            Thread.Sleep(randomTimer);
        }
    }
}