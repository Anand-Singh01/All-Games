using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemonSlayer.Components;
namespace DemonSlayer.Scenes
{
    /// <summary>
    /// Represents the help scene providing instructions on controls to the player.
    /// </summary>
    internal class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D leftArrowImage;
        private Texture2D rightArrowImage;
        private Texture2D UpArrowImage;
        private Texture2D DownArrowImage;
        private Texture2D SpaceBarImage;
        private SpriteFont helpFont;
        public HelpScene(Game game) : base(game)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load images and fonts for displaying help instructions
            leftArrowImage = game.Content.Load<Texture2D>("HelpImage/leftarrow");
            rightArrowImage = game.Content.Load<Texture2D>("HelpImage/Rightarrow");
            UpArrowImage = game.Content.Load<Texture2D>("HelpImage/Uparrow");
            DownArrowImage = game.Content.Load<Texture2D>("HelpImage/Downarrow");
            SpaceBarImage = game.Content.Load<Texture2D>("HelpImage/Spacebar");

            helpFont = game.Content.Load<SpriteFont>("fonts/HilightFont");

            // Set positions for the images
            Vector2 leftArrowPosition = new Vector2(100, 100);
            Vector2 rightArrowPosition = new Vector2(100, 200);
            Vector2 upArrowPosition = new Vector2(100, 300);
            Vector2 downArrowPosition = new Vector2(100, 400);
            Vector2 spacebarPosition = new Vector2(100, 500);

            // Create components for displaying images
            var leftArrow = new ImageComponent(game, spriteBatch, leftArrowImage, leftArrowPosition);
            var rightArrow = new ImageComponent(game, spriteBatch, rightArrowImage, rightArrowPosition);
            var upArrow = new ImageComponent(game, spriteBatch, UpArrowImage, upArrowPosition);
            var downArrow = new ImageComponent(game, spriteBatch, DownArrowImage, downArrowPosition);
            var spaceBar = new ImageComponent(game, spriteBatch, SpaceBarImage, spacebarPosition);

            // Add image components to the scene's components list
            Components.Add(leftArrow);
            Components.Add(rightArrow);
            Components.Add(upArrow);
            Components.Add(downArrow);
            Components.Add(spaceBar);
        }

        /// <summary>
        /// Draws the help scene and instructions on controls for the player.
        /// </summary>
        /// <param name="gameTime">game's timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();

            // Display text instructions on screen
            spriteBatch.DrawString(helpFont, "Press Left Arrow to Move Left", new Vector2(200, 120), Color.Black);
            spriteBatch.DrawString(helpFont, "Press Right Arrow to Move Right", new Vector2(200, 220), Color.Black);
            spriteBatch.DrawString(helpFont, "Press Up Arrow to Move Up", new Vector2(200, 320), Color.Black);
            spriteBatch.DrawString(helpFont, "Press Down Arrow to Move Down", new Vector2(200, 420), Color.Black);
            spriteBatch.DrawString(helpFont, "Press Space bar to Shoot", new Vector2(207, 520), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
