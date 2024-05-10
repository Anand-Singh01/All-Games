using DemonSlayer.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemonSlayer.Scenes
{
    /// <summary>
    /// Represents the scene displayed upon the player's defeat, offering options to restart the 
    /// game or return to the main menu.
    /// </summary>
    internal class GameOverScene : GameScene
    {
        private MenuComponent menu;
        private SpriteBatch sb;

        /// <summary>
        /// Gets or sets the MenuComponent associated with the GameOverScene.
        /// </summary>
        public MenuComponent Menu { get => menu; set => menu = value; }

        public GameOverScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            sb = g._spriteBatch;
            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/RegularFont");
            SpriteFont highlightedFont = game.Content.Load<SpriteFont>("fonts/HilightFont");
            string[] menuItems = { "Restart Game", "Main Menu" };
            string id = $"GameOver";

            // Initialize the MenuComponent for the GameOverScene
            Menu = new MenuComponent(game, sb, regularFont, highlightedFont, menuItems,
                g.Content.Load<Texture2D>("images/menuImage"),  id);

            // Add MenuComponent to the GameOverScene's Components collection
            this.Components.Add(Menu);
        }
    }
}
