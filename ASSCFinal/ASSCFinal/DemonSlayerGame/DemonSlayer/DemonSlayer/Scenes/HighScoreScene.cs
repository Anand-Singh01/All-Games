using DemonSlayer.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace DemonSlayer.Scenes
{
    /// <summary>
    /// Represents the scene displaying the high scores of players.
    /// </summary>
    internal class HighScoreScene : GameScene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        public HighScoreScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            _spriteBatch = g._spriteBatch;
            _font = g.Content.Load<SpriteFont>("fonts/HilightFont");
        }

        /// <summary>
        /// Draws the high scores of players on the screen.
        /// </summary>
        /// <param name="gameTime">game's timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            // Display the high scores of players

            Vector2 center = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
            _spriteBatch.DrawString(_font, "High Scores:", new Vector2(center.X - 100, center.Y - 300), Color.White);

            if (Hero.highScores.Count > 0)
            {
                _spriteBatch.DrawString(_font, $"1. Player1 - {Hero.highScores.Max()}", new Vector2(center.X - 100, center.Y - 200), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(_font, "1. Player1 - 0", new Vector2(center.X - 100, center.Y - 200), Color.White);
            }
            if (Hero.highScores.Count > 1)
            {
                int secondHighest = Hero.highScores.OrderByDescending(score => score).Skip(1).FirstOrDefault();
                _spriteBatch.DrawString(_font, $"2. Player2 - {secondHighest}", new Vector2(center.X - 100, center.Y - 100), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(_font, "2. Player2 - 0", new Vector2(center.X - 100, center.Y - 100), Color.White);
            }
            if (Hero.highScores.Count > 2)
            {
                int thirdHighest = Hero.highScores.OrderByDescending(score => score).Skip(2).FirstOrDefault();
                _spriteBatch.DrawString(_font, $"3. Player3 - {thirdHighest}", new Vector2(center.X - 100, center.Y), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(_font, "3. Player3 - 0", new Vector2(center.X - 100, center.Y), Color.White);
            }
            if (Hero.highScores.Count > 3)
            {
                int fourthHighest = Hero.highScores.OrderByDescending(score => score).Skip(3).FirstOrDefault();
                _spriteBatch.DrawString(_font, $"4. Player4 - {fourthHighest}", new Vector2(center.X - 100, center.Y + 100), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(_font, "4. Player4 - 0", new Vector2(center.X - 100, center.Y + 100), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
