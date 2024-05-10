using DemonSlayer.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace DemonSlayer.Scenes
{
    internal class ActionScene : GameScene
    {
        public enum Direction
        {
            Down,
            Up,
            Left,
            Right
        }
        private SpriteBatch _sb;
        private Hero _hero;
        private Arena _arena;

        /// <summary>
        /// Initializes a new instance of the ActionScene class.
        ///Receives a Game instance and sets up the necessary components for the action scene.
        ///Loads content, initializes the scene, and adds components required for gameplay.
        /// </summary>
        /// <param name="game"></param>
        public ActionScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            _sb = g._spriteBatch;

            LoadContent();
            InitializeScene();
        }

        /// <summary>
        /// Loads content specific to the ActionScene, such as arena backgrounds and necessary textures.
        ///Initializes the arena and adds it to the scene's components collection.
        ///Loads and sets up other essential components required for the action scene.
        /// </summary>
        private void LoadContent()
        {
            // Load content for the ActionScene here
            // Example:
            _arena = new Arena((Game1)Game, _sb, Game.Content.Load<Texture2D>("images/gameArenas/background"), new Vector2(-500, -500));
            // Load other necessary content

            // Set up the scene by adding components
            this.Components.Add(_arena);
            // Add other necessary components
        }

        /// <summary>
        /// InitializeScene Method:
        ///Initializes the components required for the action scene.
        ///Creates and initializes the hero character, setting up its initial properties and sprites.
        ///Adds the hero and other initialized components to the scene's components collection.
        /// </summary>
        private void InitializeScene()
        {
            // Initialize the scene components here
            // Example:
            _hero = new Hero((Game1)Game, _sb, LoadHeroSprites(), new Vector2(500, 300), 7, 1, 4, Direction.Right);
            // Initialize other necessary components

            // Add the initialized components to the scene
            this.Components.Add(_hero);
            // Add other initialized components
        }

        /// <summary>
        /// Loads and returns an array of hero sprites for different directions (up, down, left, right).
        /// Loads hero walking sprites for each direction and stores them in a texture array.
        /// </summary>
        /// <returns></returns>
        private Texture2D[] LoadHeroSprites()
        {
            // Load hero sprites and return the sprite array
            Texture2D _heroWalkingDownSprite = Game.Content.Load<Texture2D>("images/hero/walkDown");
            Texture2D _heroWalkingUpSprite = Game.Content.Load<Texture2D>("images/hero/walkUp");
            Texture2D _heroWalkingLeftSprite = Game.Content.Load<Texture2D>("images/hero/walkLeft");
            Texture2D _heroWalkingRightSprite = Game.Content.Load<Texture2D>("images/hero/walkRight");

            return new Texture2D[] { _heroWalkingDownSprite, _heroWalkingUpSprite, _heroWalkingLeftSprite, _heroWalkingRightSprite };
        }

        /// <summary>
        /// Clears existing components from the action scene.
        ///Reloads content and re-initializes the scene and its components, effectively resetting the scene.
        ///Removes current components, loads fresh content, and initializes the scene anew.
        /// </summary>
        public void ResetScene()
        {
            // Remove existing components from the scene
            this.Components.Clear();

            // Re-initialize the ActionScene components
            LoadContent();
            InitializeScene();
        }
    }
}
