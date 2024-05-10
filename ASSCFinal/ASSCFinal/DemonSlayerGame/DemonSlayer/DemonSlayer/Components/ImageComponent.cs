using DemonSlayer.Components;
using DemonSlayer.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DemonSlayer.Components
{

    /// <summary>
    /// Represents an individual image component to be drawn on the screen.
    /// Inherits from DrawableGameComponent for rendering capabilities.
    /// Manages the rendering of a single image at a specified position on the screen.
    /// </summary>
    public class ImageComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 position;

        /// <summary>
        /// Constructs a new instance of the ImageComponent class.
        /// </summary>
        /// <param name="game">The game instance.</param>
        /// <param name="spriteBatch">The SpriteBatch used for rendering.</param>
        /// <param name="texture">The image texture to be drawn.</param>
        /// <param name="position">The position to draw the image on the screen.</param>
        public ImageComponent(Game game, SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.position = position;
        }

        /// <summary>
        /// Draws the image at the specified position on the screen.
        /// </summary>
        /// <param name="gameTime">game's timing state.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
