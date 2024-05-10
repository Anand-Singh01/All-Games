using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemonSlayer.Scenes
{
    /// <summary>
    /// Represents a base class for game scenes, handling their visibility, updates, and drawing.
    /// </summary>
    internal class GameScene : DrawableGameComponent
    {
        /// <summary>
        /// Gets or sets the list of GameComponents associated with the scene.
        /// </summary>
        public List<GameComponent> Components { get; set; }

        protected GameScene(Game game) : base(game)
        {
            Components = new List<GameComponent>();
            hide();
        }

        /// <summary>
        /// Hides the current scene, disabling its visibility and functionality.
        /// </summary>
        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }
        /// <summary>
        /// Displays the current scene, enabling its visibility and functionality.
        /// </summary>
        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        /// <summary>
        /// Updates the components within the scene based on the game time.
        /// </summary>
        /// <param name="gameTime">Snapshot of the game's timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent gameComponent in Components)
            {
                if (gameComponent.Enabled)
                {
                    gameComponent.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the visible components of the scene based on the game time.
        /// </summary>
        /// <param name="gameTime">Snapshot of the game's timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent gameComponent in Components)
            {
                if (gameComponent is DrawableGameComponent)
                {
                    DrawableGameComponent component = (DrawableGameComponent)gameComponent;
                    if (component.Visible)
                    {
                        component.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }
    }
}
