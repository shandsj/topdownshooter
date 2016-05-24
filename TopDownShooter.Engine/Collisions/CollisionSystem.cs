// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollisionSystem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine.Collisions
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a system to handle collisions.
    /// </summary>
    public class CollisionSystem : ICollisionSystem
    {
        private readonly Dictionary<int, IColliderComponent> colliders = new Dictionary<int, IColliderComponent>();
        private readonly Dictionary<int, IGameObject> gameObjects = new Dictionary<int, IGameObject>();

        static CollisionSystem()
        {
            NextGameObjectId = 1;
        }

        /// <summary>
        /// Gets or sets the next available game object identifier.
        /// </summary>
        /// TODO: refactor this and put it somewhere else.
        public static int NextGameObjectId { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="IColliderComponent"/> objects registered with this <see cref="ICollisionSystem"/>.
        /// </summary>
        public IEnumerable<IColliderComponent> Colliders => this.colliders.Values.ToArray();

        /// <summary>
        /// Gets the collection of <see cref="IGameObject"/> objects registered with this <see cref="ICollisionSystem"/>.
        /// </summary>
        public IEnumerable<IGameObject> GameObjects => this.gameObjects.Values.ToArray();

        /// <summary>
        /// Checks the collisions for the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="collider">The rigid body.</param>
        public void CheckCollisions(IColliderComponent collider)
        {
            // Don't check collisions for a collider and itself.
            var otherColliders = this.colliders.Values.Where(o => o != collider);

            foreach (var other in otherColliders)
            {
                if (collider.IsCollision(other))
                {
                    // Not sure if this is fully right, but a player was only colliding
                    // with the bullet, not the other way around. This meant the BullectColliderComponent's
                    // Collide method was never being called.
                    other.Collide(collider);
                    collider.Collide(other);
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="IGameObject" /> with the specified identifer.
        /// </summary>
        /// <param name="id">Game object identifier.</param>
        /// <returns>The <see cref="IGameObject" />.</returns>
        public IGameObject GetGameObject(int id)
        {
            IGameObject gameObject;
            if (this.gameObjects.TryGetValue(id, out gameObject))
            {
                return gameObject;
            }

            return null;
        }

        /// <summary>
        /// Registers the specifieid <see cref="IGameObject" /> and <see cref="IColliderComponent" /> with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the game object.</param>
        /// <param name="gameObject">The game object.</param>
        /// <param name="collider">The collider component.</param>
        public void Register(int id, IGameObject gameObject, IColliderComponent collider)
        {
            this.gameObjects[id] = gameObject;
            this.colliders[id] = collider;
        }

        /// <summary>
        /// Unregisters the game object and collider with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the game object.</param>
        public void Unregister(int id)
        {
            this.gameObjects.Remove(id);
            this.colliders.Remove(id);
        }
    }
}