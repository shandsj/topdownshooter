// <copyright file="SimpleAiInputControllerComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Controller for controlling a simple AI.
    /// </summary>
    public class SimpleAiInputControllerComponent : InputControllerComponentBase
    {
        private Vector2 direction;

        private bool fire;

        private Random random;

        private List<Task> taskList;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAiInputControllerComponent"/> class.
        /// </summary>
        public SimpleAiInputControllerComponent()
        {
            this.random = new Random((int)DateTime.Now.Ticks);
            this.Initalize();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SimpleAiInputControllerComponent"/> class.
        /// </summary>
        ~SimpleAiInputControllerComponent()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(false);
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
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move down.</returns>
        public override bool MoveDown()
        {
            return this.direction.Y > 0;
        }

        /// <summary>
        /// Gets whether or not a left move was requested.
        /// </summary>
        /// <returns>True to move left.</returns>
        public override bool MoveLeft()
        {
            return this.direction.X < 0;
        }

        /// <summary>
        /// Gets whether or not a right move was requested.
        /// </summary>
        /// <returns>True to move right.</returns>
        public override bool MoveRight()
        {
            return this.direction.X > 0;
        }

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move up.</returns>
        public override bool MoveUp()
        {
            return this.direction.Y < 0;
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
        /// Initalizes our tasks for movement changes
        /// </summary>
        private void Initalize()
        {
            this.taskList = new List<Task>();
            this.taskList.AddRange(new Task[]
            {
                Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            this.SleepRandomly();
                            this.direction = new Vector2(this.random.Next(-1, 2), this.random.Next(-1, 2));
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
            });
        }

        /// <summary>
        /// Sleeps a random amount of time between 0 and 1.5 seconds.
        /// </summary>
        /// <remarks>No one will get passed this sweet ai!</remarks>
        private void SleepRandomly()
        {
            int randomTimer = this.random.Next(0, 1500);
            System.Threading.Thread.Sleep(randomTimer);
        }
    }
}
