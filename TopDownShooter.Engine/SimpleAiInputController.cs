// <copyright file="SimpleAiInputController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Controller for controlling a simple AI.
    /// </summary>
    public class SimpleAiInputController : IInputController, IDisposable
    {
        private bool moveLeft;
        private bool moveRight;
        private bool moveUp;
        private bool moveDown;

        private Random random;
        private bool disposedValue = false; // To detect redundant calls

        private List<Task> taskList;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAiInputController"/> class.
        /// </summary>
        public SimpleAiInputController()
        {
            this.random = new Random((int)DateTime.Now.Ticks);
            this.Initalize();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SimpleAiInputController"/> class.
        /// </summary>
        ~SimpleAiInputController()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(false);
        }

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move down.</returns>
        public bool MoveDown()
        {
            return this.moveDown;
        }

        /// <summary>
        /// Gets whether or not a left move was requested.
        /// </summary>
        /// <returns>True to move left.</returns>
        public bool MoveLeft()
        {
            return this.moveLeft;
        }

        /// <summary>
        /// Gets whether or not a right move was requested.
        /// </summary>
        /// <returns>True to move right.</returns>
        public bool MoveRight()
        {
            return this.moveRight;
        }

        /// <summary>
        /// Gets whether or not a up move was requested.
        /// </summary>
        /// <returns>True to move up.</returns>
        public bool MoveUp()
        {
            return this.moveUp;
        }

        /// <summary>
        /// Updates based on the current gametime.
        /// </summary>
        /// <param name="gameTime">The current <see cref="GameTime"/></param>
        public void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// This code added to correctly implement the disposable pattern.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up any resources allocated during execution
        /// </summary>
        /// <param name="disposing">Gets whether this object is disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.taskList.ForEach(o => o.Dispose());
                    this.taskList.Clear();
                }

                this.disposedValue = true;
            }
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
                        if (this.moveDown == true)
                        {
                            continue;
                        }

                        this.SleepRandomly();

                        this.moveUp = !this.moveUp;
                    }
                }),

                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        if (this.moveUp == true)
                        {
                            continue;
                        }

                        this.SleepRandomly();

                        this.moveDown = !this.moveDown;
                    }
                }),

                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        if (this.moveRight == true)
                        {
                            continue;
                        }

                        this.SleepRandomly();

                        this.moveLeft = !this.moveLeft;
                    }
                }),

                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        if (this.moveLeft == true)
                        {
                            continue;
                        }

                        this.SleepRandomly();

                        this.moveRight = !this.moveRight;
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
