using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DemonSlayer.Components;

namespace DemonSlayer.Scenes
{
    /// <summary>
    /// Represents the starting scene of the game with menu options.
    /// </summary>
    internal class StartScene : GameScene
    {
        private MenuComponent menu;
        private SpriteBatch sb;
        /// <summary>
        /// Gets or sets the menu component for the StartScene.
        /// </summary>
        public MenuComponent Menu { get => menu; set => menu = value; }
        public StartScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            sb = g._spriteBatch;
            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/RegularFont");
            SpriteFont highlightedFont = game.Content.Load<SpriteFont>("fonts/HilightFont");
            string[] menuItems = { "Start Game", "Help", "High Score", "Credit", "Quit" };

            // Create the menu component for the StartScene
            Menu = new MenuComponent(game, sb, regularFont, highlightedFont, menuItems, 
                g.Content.Load<Texture2D>("images/menuImage"), "start");
            this.Components.Add(Menu);
        }
    }
}
