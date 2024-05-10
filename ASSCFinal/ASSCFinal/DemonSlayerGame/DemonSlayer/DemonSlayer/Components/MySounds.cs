using DemonSlayer.Components;
using DemonSlayer.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace DemonSlayer.Components
{
    /// <summary>
    /// A static class managing sound effects and music resources for the game.
    /// </summary>
    public static class MySounds
    {
        /// <summary>
        /// Sound effect for projectile firing
        /// </summary>
        public static SoundEffect projectileSound;
        /// <summary>
        /// Sound effect for game Start
        /// </summary>
        public static SoundEffect gameStart;
        /// <summary>
        /// Sound effect for heroe's death
        /// </summary>
        public static SoundEffect heroDies;
        /// <summary>
        /// Sound effect for level start
        /// </summary>
        public static Song levelStart;
    }
}
