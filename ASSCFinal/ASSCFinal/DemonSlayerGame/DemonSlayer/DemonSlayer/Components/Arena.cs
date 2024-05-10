using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemonSlayer.Components
{
    /// <summary>
    /// Represents the arena in the game where gameplay occurs.
    /// </summary>
    internal class Arena : DrawableGameComponent
    {
        private Texture2D _background;
        private Vector2 _position;
        private SpriteBatch _sb;

        /// <summary>
        /// Initializes a new instance of the Arena class.
        /// </summary>
        /// <param name="game">The game to which this arena belongs.</param>
        /// <param name="sb">The SpriteBatch used to draw the arena.</param>
        /// <param name="background">The background texture of the arena.</param>
        /// <param name="position">The position of the arena.</param>
        public Arena(Game game, SpriteBatch sb, Texture2D background, Vector2 position) : base(game)
        {
            Game1 g = (Game1)game;
            _background = background;
            _position = position;
            _sb = sb;
        }

        /// <summary>
        /// Draws the arena and its background.
        /// </summary>
        /// <param name="gameTime">game's timing state.</param>
        public override void Draw(GameTime gameTime)
        {
            _sb.Begin();
            _sb.Draw(_background, _position, Color.White);
            _sb.End();
            base.Draw(gameTime);
        }
    }
}
