using DemonSlayer.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static DemonSlayer.Scenes.ActionScene;

namespace DemonSlayer.Components
{
    /// <summary>
    /// Represents a fireball projectile in the game.
    /// </summary>
    internal class FireBall
    {
        public static List<FireBall> fireBalls = new List<FireBall>();

        public Vector2 position;
        private int speed = 1000;
        public int frameIndex = 0;
        public int radius = 18;
        private Direction direction;
        private bool collided = false;
        public Texture2D fireBallSprite;
        public List<Rectangle> frames;
        private Vector2 dimension;
        private int delayCounter;
        private int delay = 5;
        public float rotation;
        public Vector2 fireBallOffset = Vector2.Zero;
        public bool Collided { get { return collided; } set { collided = value; } }


        /// <summary>
        /// Constructs a new fireball instance.
        /// </summary>
        /// <param name="game">The Game instance.</param>
        /// <param name="newPosition">The starting position of the fireball.</param>
        /// <param name="newDirection">The direction of the fireball.</param>
        public FireBall(Game game, Vector2 newPosition, Direction newDirection)
        {
            Game1 g = (Game1)game;
            position = newPosition;
            direction = newDirection;
            fireBallSprite = g.Content.Load<Texture2D>("images/hero/FireBall");
            createFrames();
        }

        /// <summary>
        /// Creates frames for animation of the fireball.
        /// </summary>
        public void createFrames()
        {
            frames = new List<Rectangle>();

            for (int i = 0; i < 8; i++)
            {
                dimension = new Vector2(fireBallSprite.Width / 5, fireBallSprite.Height / 1);
                for (int j = 0; j < 1; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        int x = k * (int)dimension.X;
                        int y = j * (int)dimension.Y;

                        Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                        frames.Add(r);

                    }
                }
            }
        }

        /// <summary>
        /// Updates the fireball's position and animation frame.
        /// </summary>
        /// <param name="gameTime">game's timing state.</param>
        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (direction)
            {
                case Direction.Down:
                    position.Y += speed * dt;
                    rotation = MathHelper.ToRadians(90f);
                    fireBallOffset = new Vector2(0, 110);
                    break;
                case Direction.Up:
                    position.Y -= speed * dt;
                    rotation = MathHelper.ToRadians(-90f);
                    fireBallOffset = new Vector2(150, -10);
                    break;
                case Direction.Left:
                    position.X -= speed * dt;
                    rotation = MathHelper.ToRadians(180f);
                    fireBallOffset = new Vector2(100, 100);
                    break;
                case Direction.Right:
                    position.X += speed * dt;
                    rotation = MathHelper.ToRadians(0f);
                    fireBallOffset = new Vector2(0,0);
                    break;
            }
            SwitchFrames();
        }

        /// <summary>
        /// Switches frames for the fireball's animation.
        /// </summary>
        public void SwitchFrames()
        {
            delayCounter++;
            if (delayCounter >= delay)
            {
                frameIndex++;
                if (frameIndex > 5)
                {
                    frameIndex = 0;
                }
                delayCounter = 0;
            }
        }
    }
}
