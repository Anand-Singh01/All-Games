using DemonSlayer.Components;
using DemonSlayer.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System.Collections.Generic;
using System.Windows.Forms.Design.Behavior;

namespace DemonSlayer.Components
{
    /// <summary>
    /// Represents the main character controlled by the player in the game.
    /// Handles the movement, actions, and interactions of the hero with the game environment.
    /// Manages the hero's animation frames based on player input and game events.
    /// </summary>
    internal class Hero : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D[] _spriteSheets;
        private Vector2 position;
        private List<Rectangle> frames;
        private Dictionary<int, List<Rectangle>> walkingFrames = new Dictionary<int, List<Rectangle>>();
        private int frameIndex = 0;
        private ActionScene.Direction _direction;
        private int delay;
        private int delayCounter;
        private Vector2 dimension;
        private int _rows;
        private int _columns;
        private Game1 g;
        private KeyboardState _oldState;
        public static bool dead = false;
        public SpriteFont font;
        public static int highScore;

        public static List<int> highScores = new List<int>();
        public Vector2 Position { get => position; set => position = value; }
        Texture2D skull;

        /// <summary>
        /// Constructs a new instance of the Hero class.
        /// </summary>
        /// <param name="game">The game instance.</param>
        /// <param name="sb">The SpriteBatch used for rendering.</param>
        /// <param name="spriteSheets">Array of sprite sheets representing the hero's animations.</param>
        /// <param name="position">The starting position of the hero.</param>
        /// <param name="delay">The delay used for frame switching.</param>
        /// <param name="rows">Number of rows in the sprite sheet.</param>
        /// <param name="columns">Number of columns in the sprite sheet.</param>
        /// <param name="direction">Initial direction of the hero.</param>
        public Hero(Game game, SpriteBatch sb, Texture2D[] spriteSheets, Vector2 position, int delay,
            int rows, int columns, ActionScene.Direction direction) : base(game)
        {
            g = (Game1)game;
            this.sb = sb;
            _spriteSheets = spriteSheets;
            this.position = position;
            this.delay = delay;
            _rows = rows;
            _columns = columns;
            _direction = direction;
            skull = g.Content.Load<Texture2D>("images/hero/skull");
            font = g.Content.Load<SpriteFont>("fonts/HilightFont");
            CreateFrames();
        }

        /// <summary>
        /// Generates frames for different directions based on loaded sprite sheets.
        ///Divides sprite sheets into frames and organizes them according to specified rows and columns.
        ///Populates a dictionary with walking frames for various directions.
        /// </summary>
        private void CreateFrames()
        {
            frames = new List<Rectangle>();

            for (int i = 0; i < _spriteSheets.Length; i++)
            {
                dimension = new Vector2(_spriteSheets[i].Width / _columns, _spriteSheets[i].Height / _rows);
                for (int j = 0; j < _rows; j++)
                {
                    for (int k = 0; k < _columns; k++)
                    {
                        int x = k * (int)dimension.X;
                        int y = j * (int)dimension.Y;

                        Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                        frames.Add(r);
                    }
                }
                walkingFrames.Add(i, frames);
                frames = new List<Rectangle>();
            }
        }

        /// <summary>
        /// Updates the hero's movement, actions, and interactions.
        /// Handles collision detection, firing projectiles, and gameplay events.
        /// </summary>
        /// <param name="gameTime">game's timing state.</param>
        public override void Update(GameTime gameTime)
        {
            Controller.update(gameTime, skull);

            KeyboardState ks = Keyboard.GetState();
            bool isWalking = false;

            if (Skull.ghosts.Count > 0)
            {
                foreach (Skull ghost in Skull.ghosts)
                {
                    ghost.update(gameTime, position, false);
                    int sum = 32 + ghost.radius;
                    if (Vector2.Distance(position, ghost.position) < sum)
                    {
                        dead = true;
                    }
                }
            }


            if (ks.IsKeyDown(Keys.Right))
            {
                isWalking = true;
                _direction = ActionScene.Direction.Right;
                position.X += 8;
            }
            else if (ks.IsKeyDown(Keys.Left))
            {
                isWalking = true;
                _direction = ActionScene.Direction.Left;
                if (position.X > 180)
                {
                    position.X -= 8;
                }
            }
            else if (ks.IsKeyDown(Keys.Up))
            {
                isWalking = true;
                _direction = ActionScene.Direction.Up;
                if (position.Y > 120)
                {
                    position.Y -= 8;
                }
            }
            else if (ks.IsKeyDown(Keys.Down))
            {
                isWalking = true;
                _direction = ActionScene.Direction.Down;
                if (position.Y < 1250)
                {
                    position.Y += 8;
                }
            }

            if (isWalking)
            {
                SwitchFrames();

                if (ks.IsKeyDown(Keys.Space) && _oldState.IsKeyUp(Keys.Space))
                {
                    FireBall.fireBalls.Add(new FireBall(g, position, _direction));
                    MySounds.projectileSound.Play(0.25f, 0.25f, 0f);
                }
            }
            else if (ks.IsKeyDown(Keys.Space) && _oldState.IsKeyUp(Keys.Space))
            {
                FireBall.fireBalls.Add(new FireBall(g, position, _direction));
                MySounds.projectileSound.Play(0.25f, 0.25f, 0f);
            }
            else
            {
                frameIndex = 1;
            }

            foreach (FireBall fireBall in FireBall.fireBalls)
            {
                fireBall.Update(gameTime);
            }

            if (!dead)
            {
                foreach (Skull skull in Skull.ghosts)
                {
                    skull.update(gameTime, position, dead);
                    int sum = 32 + skull.radius;
                    if (Vector2.Distance(position, skull.position) < sum)
                    {
                        MySounds.heroDies.Play();
                        dead = true;
                        highScores.Add(highScore);
                        break;
                    }
                }
            }

            if (!dead)
            {
                foreach (FireBall fireBall in FireBall.fireBalls)
                {
                    foreach (Skull skull in Skull.ghosts)
                    {
                        int totalRadius = fireBall.radius + skull.radius;
                        if (Vector2.Distance(fireBall.position, skull.position) < totalRadius)
                        {
                            fireBall.Collided = true;
                            skull.Dead = true;
                            highScore += 10;
                        }
                    }
                }
            }

            FireBall.fireBalls.RemoveAll(pb => pb.Collided);
            Skull.ghosts.RemoveAll(gh => gh.Dead);
            _oldState = ks;
        }


        /// <summary>
        /// Controls the animation frame switching for the hero character.
        /// Manages the frame transition based on a specified delay.
        /// </summary>
        public void SwitchFrames()
        {
            delayCounter++;
            if (delayCounter >= delay)
            {
                frameIndex++;
                if (frameIndex > _rows * _columns - 1)
                {
                    frameIndex = 0;
                }
                delayCounter = 0;
            }
        }

        /// <summary>
        /// Renders the hero's current state, animations, and game elements related to the hero.
        /// Draws the hero sprite, fireballs, and other relevant visuals on the screen.
        /// </summary>
        /// <param name="gameTime">game's timing state.</param>
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();

            sb.DrawString(font, $"Score:{highScore}", new Vector2(Shared.stage.X-200, Shared.stage.Y-700), Color.Red);
            foreach (Skull skull in Skull.ghosts)
            {
                skull.ghostAnimation.Draw(sb);
            }
            if (!dead)
            {
                foreach (FireBall fireBall in FireBall.fireBalls)
                {
                    sb.Draw(fireBall.fireBallSprite, fireBall.position, fireBall.frames[fireBall.frameIndex],
                        Color.White, fireBall.rotation, fireBall.fireBallOffset, 1.0f, SpriteEffects.None, 0f);
                }

                sb.Draw(_spriteSheets[(int)_direction], position, walkingFrames[(int)_direction][frameIndex],
                    Color.White, 0.0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0f);
            }

            sb.End();
            base.Draw(gameTime);
        }
    }
}