/*
 * DemonSlayer
 * Final Project
 * Revision History
 * Sapana Chhetri & Anandpravesh Singh, 2023-12-01: Created
 */
using DemonSlayer.Components;
using DemonSlayer.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace DemonSlayer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private StartScene _startScene;
        private ActionScene _actionScene;
        private HelpScene _helpScene;
        private GameOverScene _gameOverScene;
        private HighScoreScene _highScoreScene;
        private AboutScene _aboutScene;

        public SpriteBatch _spriteBatch;       
        bool soundPlaying = false;

        /// <summary>
        /// Initializes a new instance of the Game1 class.
        ///Configures the graphics device manager, sets the content directory, and initializes variables.
        ///Sets the mouse visibility in the game window.
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game settings and configurations.
        ///Sets the preferred back buffer width and height for the graphics device.
        ///Sets up the stage size based on the preferred back buffer dimensions.
        /// </summary>
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);
            base.Initialize();
        }

        /// <summary>
        /// Loads necessary game content such as sound effects, music, and initializes game scenes.
        ///Creates instances of StartScene, ActionScene, HelpScene, GameOverScene, HighScoreScene, and AboutScene.
        ///Adds these scenes to the game's Components collection and displays the StartScene.
        /// </summary>
        protected override void LoadContent()
        {
            MySounds.projectileSound = Content.Load<SoundEffect>("sounds/Shoot");
            MySounds.gameStart = Content.Load<SoundEffect>("sounds/GameStart");
            MySounds.heroDies = Content.Load<SoundEffect>("sounds/CharacterDead");
            MySounds.levelStart = Content.Load<Song>("sounds/nature");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _startScene = new StartScene(this);
            _actionScene = new ActionScene(this);
            _helpScene = new HelpScene(this);
            _gameOverScene = new GameOverScene(this);
            _highScoreScene = new HighScoreScene(this);
            _aboutScene = new AboutScene(this);
            this.Components.Add(_startScene);
            this.Components.Add(_actionScene);
            this.Components.Add(_helpScene);
            this.Components.Add(_gameOverScene);
            this.Components.Add(_highScoreScene);
            this.Components.Add(_aboutScene);
            _startScene.show();
        }

        /// <summary>
        /// Handles updating game logic based on user input and game state changes.
        ///Manages scene transitions, keyboard input, and updates scene visibility accordingly.
        ///Handles transitions between different scenes based on player actions and game state changes.
        ///Controls scene visibility and component updates based on user input and game conditions.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            MenuComponent menu;

            // Handle StartScene transitions
            if (_startScene.Enabled)
            {
                if (!soundPlaying)
                {
                    MySounds.gameStart.Play();
                    soundPlaying = true;
                }

                menu = _startScene.Menu;
                if (menu.SelectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    _startScene.hide();
                    _actionScene.show();
                    MediaPlayer.Play(MySounds.levelStart);
                }
                if (menu.SelectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    _startScene.hide();
                    _helpScene.show();
                }
                if (menu.SelectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    _startScene.hide();
                    _highScoreScene.show();

                }
                if (menu.SelectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    _startScene.hide();
                    _aboutScene.show();

                }
                if (menu.SelectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }             
            }

            // Check if the hero is dead
            if (Hero.dead)
            {
                _actionScene.hide();
                _gameOverScene.show();
            }

            // Handle GameOverScene transitions
            if (_gameOverScene.Enabled)
            {
                menu = _gameOverScene.Menu;
                if (menu.SelectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    RestartGame();
                }
                else if (menu.SelectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    ReturnToStartScene();
                }
            }
            // Handle HelpScene transitions
            if (_helpScene.Enabled && ks.IsKeyDown(Keys.Escape))
            {
                _helpScene.hide();
                _startScene.show();
            }
            if (_highScoreScene.Enabled && ks.IsKeyDown(Keys.Escape))
            {
                _highScoreScene.hide();
                _startScene.show();
            }
            if (_aboutScene.Enabled && ks.IsKeyDown(Keys.Escape))
            {
                _aboutScene.hide();
                _startScene.show();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Restarts the game by clearing existing components and resetting game variables.
        /// Adds necessary scenes/components back and shows the start scene while hiding the game over scene.
        /// </summary>
        private void RestartGame()
        {
            // Clear existing components
            this.Components.Clear();

            // Reset game variables
            Hero.dead = false;
            Hero.highScore = 0;
            Skull.ghosts = new List<Skull>();

            // Add necessary scenes/components back
            this.Components.Add(_startScene);
            this.Components.Add(_actionScene);
            this.Components.Add(_helpScene);
            this.Components.Add(_gameOverScene);
            this.Components.Add(_highScoreScene);
            this.Components.Add(_aboutScene);
            // Add other scenes/components as required

            // Hide game over scene and show start scene
            _gameOverScene.hide();
            _startScene.show();
        }

        /// <summary>
        /// Returns to the StartScene from the Game Over Scene by reconfiguring components.
        /// Clears existing components and adds necessary scenes/components back while excluding GameOverScene.
        /// Shows the start scene and hides other scenes like game over, help, high score, and about.
        /// </summary>
        // Helper method to return to StartScene from Game Over Scene
        private void ReturnToStartScene()
        {
            // Clear existing components
            this.Components.Clear();

            // Add necessary scenes/components back excluding GameOverScene
            this.Components.Add(_startScene);
            this.Components.Add(_actionScene);

            // Hide the other scenes
            _gameOverScene.hide();
            _helpScene.hide();
            _highScoreScene.hide();
            _aboutScene.hide();

            // Add the scenes back to the components if they are not included
            if (!this.Components.Contains(_gameOverScene))
            {
                this.Components.Add(_gameOverScene);
            }
            if (!this.Components.Contains(_helpScene))
            {
                this.Components.Add(_helpScene);
            }
            if (!this.Components.Contains(_highScoreScene))
            {
                this.Components.Add(_highScoreScene);
            }
            if (!this.Components.Contains(_aboutScene))
            {
                this.Components.Add(_aboutScene);
            }
            Hero.dead = false;
            // Show the start scene
            _startScene.show();
        }

        /// <summary>
        /// Clears the screen with a specified color and renders the game components and scenes.
        ///Handles the rendering process for displaying game scenes and their associated components.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
