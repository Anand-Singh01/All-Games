using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Windows.Forms;

namespace DemonSlayer.Components
{
    /// <summary>
    /// Handles the control and timing of game elements such as enemy movement and timing logic.
    /// </summary>
    internal class Controller
    {
        // Timer for enemies
        public static Double timer = 2D;
        public static Double maxTime = 2D;
        static Random random = new Random();

        /// <summary>
        /// Updates the game elements based on elapsed time and triggers enemies.
        /// </summary>
        /// <param name="gameTime">game's timing state.</param>
        /// <param name="spriteSheet">The texture sprite sheet used for enemies.</param>
        public static void update(GameTime gameTime, Texture2D spriteSheet)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (timer <= 0)
            {
                int side = random.Next(4);

                switch (side)
                {
                    case 0:
                        Skull.ghosts.Add(new Skull(new Vector2(-500, random.Next(-500, 2000)), spriteSheet));
                        break;
                    case 1:
                        Skull.ghosts.Add(new Skull(new Vector2(2000, random.Next(-500, 2000)), spriteSheet));
                        break;
                    case 2:
                        Skull.ghosts.Add(new Skull(new Vector2(random.Next(-500, 2000), -500), spriteSheet));
                        break;
                    case 3:
                        Skull.ghosts.Add(new Skull(new Vector2(random.Next(-500, 2000), 2000), spriteSheet));
                        break;
                }
                Skull.ghosts.Add(new Skull(new Vector2(100, 100), spriteSheet));
                timer = maxTime;

                if (maxTime > 0.5)
                {
                    maxTime -= 0.1D;
                }
            }

        }

    }
}
