using DemonSlayer.Components;
using DemonSlayer.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DemonSlayer.Scenes
{
    /// <summary>
    /// Represents the scene that displays the credits and acknowledgments in the game.
    /// </summary>
    internal class AboutScene : GameScene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private string description;
        public AboutScene(Game game) : base(game)
        {
            // Accesses necessary components from the game
            Game1 g = (Game1)game;
            _spriteBatch = g._spriteBatch;
            _font = g.Content.Load<SpriteFont>("fonts/HilightFont");

            // Set the description for the credits and references
            description = "DemonSlayer (2023)\r\nCreators: Anandpravesh Singh / Sapana Chhetri\n" +
                "References:\r\n- Kleki: Paint Tool.Keys images. [https://kleki.com/](https://kleki.com/)\r\n- MonoGame Documentation: [https://monogame.net/](https://monogame.net/)\r\n- SpriteSheets Credit: [https://www.hiclipart.com/](https://www.hiclipart.com/)\r\n- Sound Credit: [https://pixabay.com/sound-effects/](https://pixabay.com/sound-effects/)";
        }

        /// <summary>
        /// Draws the content of the AboutScene, including credits and references.
        /// </summary>
        /// <param name="gameTime">Snapshot of the game's timing state.</param>
        public override void Draw(GameTime gameTime)
        {
            // Begin rendering content
            _spriteBatch.Begin();

            // Display the header for credits
            Vector2 center = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);

            // Display the credits and references
            _spriteBatch.DrawString(_font, "Credits", new Vector2(center.X - 100, center.Y - 350), Color.White);
            _spriteBatch.DrawString(_font, description, new Vector2(center.X - 630, center.Y - 300), Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }


}
