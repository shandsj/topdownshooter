// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpatialTable.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Collisions
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Provides a way of indexing game objects by location.
    /// </summary>
    public class SpatialTable
    {
        private readonly Dictionary<Vector2, List<IGameObject>> buckets;

        private readonly int cellSize;

        private readonly int height;

        private readonly int width;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpatialTable" /> class.
        /// </summary>
        /// <param name="width">The width of the table.</param>
        /// <param name="height">The height of the table.</param>
        /// <param name="cellSize">The cell size.</param>
        public SpatialTable(int width, int height, int cellSize)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.buckets = new Dictionary<Vector2, List<IGameObject>>();

            for (int i = -width / 2; i < width / 2; i += cellSize)
            {
                for (int j = -height / 2; j < height / 2; j += cellSize)
                {
                    this.buckets.Add(new Vector2(i, j), new List<IGameObject>());
                }
            }
        }

        /// <summary>
        /// Clears the spatial table.
        /// </summary>
        public void Clear()
        {
            this.buckets.Clear();
            for (int i = -this.width / 2; i < this.width / 2; i += this.cellSize)
            {
                for (int j = -this.height / 2; j < this.height / 2; j += this.cellSize)
                {
                    this.buckets.Add(new Vector2(i, j), new List<IGameObject>());
                }
            }
        }

        /// <summary>
        /// Gets the nearby game objects for the specified game object.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <returns>The nearby game objects.</returns>
        public IEnumerable<IGameObject> GetNearby(IGameObject gameObject)
        {
            List<IGameObject> objects = new List<IGameObject>();
            var bucketIds = this.GetCellPositions(gameObject);
            foreach (var item in bucketIds)
            {
                objects.AddRange(this.buckets[item]);
            }

            return objects;
        }

        /// <summary>
        /// Registers the specified game object.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        public void Register(IGameObject gameObject)
        {
            var cellIds = this.GetCellPositions(gameObject);
            foreach (var item in cellIds)
            {
                this.buckets[item].Add(gameObject);
            }
        }

        /// <summary>
        /// Helper function that adds the nearest cell position for the specified position to the specified list.
        /// </summary>
        /// <param name="position">The position to calculate the cell position for.</param>
        /// <param name="cellPositions">The collection of cell positions to add the result to.</param>
        private void CalculateAndAddCellPosition(Vector2 position, List<Vector2> cellPositions)
        {
            var cellPosition = new Vector2(
                (int)position.X / this.cellSize * this.cellSize,
                (int)position.Y / this.cellSize * this.cellSize);

            if (!cellPositions.Contains(cellPosition))
            {
                cellPositions.Add(cellPosition);
            }
        }

        /// <summary>
        /// Gets the cell positions for the specified game object.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <returns>The collection of cell positions.</returns>
        private IEnumerable<Vector2> GetCellPositions(IGameObject gameObject)
        {
            List<Vector2> bucketsObjIsIn = new List<Vector2>();

            Vector2 min = new Vector2(gameObject.Bounds.Left, gameObject.Bounds.Top);
            Vector2 max = new Vector2(gameObject.Bounds.Right, gameObject.Bounds.Bottom);

            // Top Left
            this.CalculateAndAddCellPosition(min, bucketsObjIsIn);

            // Top Right
            this.CalculateAndAddCellPosition(new Vector2(max.X, min.Y), bucketsObjIsIn);

            // Bottom Right
            this.CalculateAndAddCellPosition(new Vector2(max.X, max.Y), bucketsObjIsIn);

            // Bottom Left
            this.CalculateAndAddCellPosition(new Vector2(min.X, max.Y), bucketsObjIsIn);

            return bucketsObjIsIn;
        }
    }
}