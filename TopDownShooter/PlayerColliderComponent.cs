﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerColliderComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TopDownShooter
{
    using Microsoft.Xna.Framework;
    using TopDownShooter.Engine;
    using TopDownShooter.Engine.Collisions;
    using TopDownShooter.Engine.Levels;
    using TopDownShooter.Messages;

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
            var player = this.CollisionSystem.GetGameObject(this.GameObjectId) as IPlayer;
            var otherObject = this.CollisionSystem.GetGameObject(other.GameObjectId);

            if (player != null && otherObject != null)
            {
                var item = otherObject as IGameItem;
                if (item != null)
                {
                    // Allow players to pass through IGameItems that they can't pickup.
                    // See if anyone in the gameObject is interested in this item.
                    player.BroadcastMessage(new Message((int)MessageType.ItemPickup, item), gameTime);
                }

                var tile = otherObject as ITile;
                if (tile != null && tile.TileInteractionType == TileInteractionType.Blocking)
                {
                    // TODO: modify velocity correctly based on location of collided object
                    player.Velocity = new Vector2(0, 0);
                }

                var otherPlayer = otherObject as IPlayer;
                if (otherPlayer != null)
                {
                    var dashRequest = new DashStatusRequestMessage();
                    player.BroadcastMessage(dashRequest, gameTime);
                    if (dashRequest.IsDashing && otherPlayer.Health > 0)
                    {
                        player.KillCount++;
                        otherPlayer.Health--;
                    }
                }
            }
        }
    }
}