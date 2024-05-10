using DemonSlayer.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DemonSlayer.Components
{
    /// <summary>
    /// The Skull class represents ghost entities within the game. It manages their positions, 
    /// movements, and animations. This class is responsible for updating the movement of the 
    /// skull towards the player's position unless the player is dead. The ghosts list stores 
    /// instances of the Skull class, presumably representing multiple ghost entities within 
    /// the game world.
    /// </summary>
    internal class Skull
    {
        // Static list containing instances of the Skull class, representing ghosts
        public static List<Skull> ghosts = new List<Skull>();

        // Position of the skull in the game world
        public Vector2 position = new Vector2(0, 0);

        // Speed of movement for the skull
        private int speed = 50;

        // SpriteAnimation instance for ghost animation
        public SpriteAnimation ghostAnimation;

        // Radius of the skull (used for collision detection)
        public int radius = 30;

        // Boolean indicating if the skull is dead
        private bool dead = false;
        public bool Dead { get { return dead; } set { dead = value; } }

        // Constructor for Skull class, initializes the position and animation
        public Skull(Vector2 newPosition, Texture2D skullSpriteSheet)
        {
            position = newPosition;
            ghostAnimation = new SpriteAnimation(skullSpriteSheet, 10, 6);
        }

        // Update method for the Skull class, handles skull movement and animation
        public void update(GameTime gameTime, Vector2 playerPosition, bool isPlayerDead)
        {
            ghostAnimation.Position = new Vector2(position.X - 48, position.Y - 66);
            ghostAnimation.Update(gameTime);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!isPlayerDead)
            {
                // Calculate movement direction towards the player
                Vector2 moveDirection = playerPosition - position;
                moveDirection.Normalize();

                // Move the skull towards the player
                position += moveDirection * speed * dt;
            }
        }
    }
}
