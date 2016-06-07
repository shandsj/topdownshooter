// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerColliderComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Levels;

    /// <summary>
    /// Defines a collider component for player objects.
    /// </summary>
    public class PlayerColliderComponent : SimpleColliderComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerColliderComponent" /> class.
        /// </summary>
        /// <param name="gameObjectId">The parent game object identifier.</param>
        /// <param name="collisionSystem">The <see cref="ICollisionSystem" />.</param>
        public PlayerColliderComponent(int gameObjectId, ICollisionSystem collisionSystem)
            : base(gameObjectId, collisionSystem)
        {
        }

        /// <summary>
        /// Performs a collision with the specified <see cref="IColliderComponent" />.
        /// </summary>
        /// <param name="other">The other collider component.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Collide(IColliderComponent other, GameTime gameTime)
        {
            var gameObject = this.CollisionSystem.GetGameObject(this.GameObjectId);
            var otherObject = this.CollisionSystem.GetGameObject(other.GameObjectId);

            if (gameObject != null && otherObject != null)
            {
                IGameItem item = otherObject as IGameItem;

                if (item == null)
                {
                    // TODO: modify velocity correctly based on location of collided object
                    gameObject.Velocity = new Vector2(0, 0);
                }
                else
                {
                    // Allow players to pass through IGameItems that they can't pickup.
                    // See if anyone in the gameObject is interested in this item.
                    gameObject.BroadcastMessage(new ComponentMessage(MessageType.ItemPickup, item), gameTime);
                }
            }
        }
    }
}