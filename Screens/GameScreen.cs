using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GameJamTest.Player;

namespace GameJamTest.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private bool initialized = false;

        private Game game;
        private ContentManager content;

        private List<GameComponent> components;

        public GameScreen(Game game)
            : base(game)
        {
            this.game = game;
            this.components = new List<GameComponent>();
            this.components.Add(new PlayerShip(game));
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            if (!initialized)
            {
                content = game.Content;
                foreach (GameComponent component in this.components)
                {
                    component.Initialize();
                }

                initialized = true;
            }
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            this.Initialize();

            foreach (GameComponent component in this.components)
            {
                component.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.Initialize();

            foreach (GameComponent component in this.components)
            {
                DrawableGameComponent drawable = component as DrawableGameComponent;
                drawable.Draw(gameTime);
            }
        }
    }
}
