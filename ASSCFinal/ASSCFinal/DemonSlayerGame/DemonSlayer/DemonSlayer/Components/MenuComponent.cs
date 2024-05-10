using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace DemonSlayer.Components
{
    /// <summary>
    /// Represents a menu component with a list of selectable menu items.
    /// Inherits from DrawableGameComponent for rendering capabilities.
    /// Handles the rendering and selection of menu items.
    /// </summary>
    internal class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch _sb;
        private SpriteFont _regulatFont, _highlightedFont;
        private List<string> _menuItems;
        private Texture2D _rectangleHighlight;
        public int SelectedIndex { get; set; }
        private Vector2 _position;
        private Color _regularColor = Color.Black;
        private Color _highlightColor = Color.White;
        private KeyboardState _oldState;
        private Texture2D _bgImage;
        string description;

        /// <summary>
        /// Initializes a new instance of the MenuComponent class.
        /// </summary>
        /// <param name="game">The game instance.</param>
        /// <param name="sb">The SpriteBatch used for rendering.</param>
        /// <param name="regularFont">The font for regular menu items.</param>
        /// <param name="highlightedFont">The font for highlighted menu items.</param>
        /// <param name="menus">An array of menu items.</param>
        /// <param name="bgImage">The background image for the menu.</param>
        /// <param name="description">Description to differentiate menus.</param>
        public MenuComponent(Game game, SpriteBatch sb, SpriteFont regularFont,
            SpriteFont highlightedFont, string[] menus, Texture2D bgImage, string description) : base(game)
        {
            _sb = sb;
            _regulatFont = regularFont;
            _highlightedFont = highlightedFont;
            _menuItems = menus.ToList();
            _position = new Vector2(Shared.stage.X / 2 - 30, Shared.stage.Y / 2 - 150);
            _rectangleHighlight = game.Content.Load<Texture2D>("images/wood");
            _bgImage = bgImage;
            this.description = description;
        }

        /// <summary>
        /// Updates the selected index based on keyboard input.
        /// </summary>
        /// <param name="gameTime">game's timing state.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyUp(Keys.Down) && _oldState.IsKeyDown(Keys.Down))
            {
                SelectedIndex++;
                if (SelectedIndex == _menuItems.Count)
                {
                    SelectedIndex = 0;
                }
            }
            if (ks.IsKeyUp(Keys.Up) && _oldState.IsKeyDown(Keys.Up))
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = _menuItems.Count - 1;
                }
            }
            _oldState = ks;

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the menu items and handles the highlighting of the selected item.
        /// </summary>
        /// <param name="gameTime">game's timing state.</param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPosition = _position;

            _sb.Begin();
            _sb.Draw(_bgImage, new Rectangle(0, 0, GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height), Color.White);

            // Draw the menu items with highlighting for the selected item
            if (description == "start")
            {
                _sb.DrawString(_highlightedFont, $"DEMON SLAYER", new Vector2(Shared.stage.X / 2 - 100,
               _position.Y - 100), Color.Red);
            }
            else
            {
                _sb.DrawString(_highlightedFont, $"{"Score: " + Hero.highScore}", new Vector2(Shared.stage.X / 2 - 10,
               _position.Y - 100), Color.Red);
            }  

            for (int i = 0; i < _menuItems.Count; i++)
            {
                Rectangle borderRectangle = new Rectangle((int)tempPosition.X - 10,
                    (int)tempPosition.Y - 5, 200, _regulatFont.LineSpacing + 20);

                if (i == SelectedIndex)
                {
                    _sb.Draw(_rectangleHighlight, borderRectangle, Color.White);
                    _sb.DrawString(_highlightedFont, _menuItems[i], tempPosition, _highlightColor);
                    tempPosition.Y += _highlightedFont.LineSpacing + 20;
                }
                else
                {
                    _sb.DrawString(_regulatFont, _menuItems[i], tempPosition, _regularColor);
                    tempPosition.Y += _regulatFont.LineSpacing + 20;
                }
            }
            _sb.End();
            base.Draw(gameTime);
        }
    }
}
