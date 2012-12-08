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

using GameJamTest.Assets;
using GameJamTest.GameObjects;
using GameJamTest.GameObjects.Player;
using GameJamTest.GameObjects.Zombie;

namespace GameJamTest.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameScreen : DrawableGameComponent
    {
        private bool initialized = false;

        private Random random;

        private Game game;
        private ContentManager content;

        private List<GameComponent> components;
        private List<GameComponent> newComponents;
        private List<GameComponent> oldComponents;

        public GameScreen(Game game)
            : base(game)
        {
            this.game = game;
            this.components = new List<GameComponent>();
            this.components.Add(new PlayerShip(game, this));
            this.newComponents = new List<GameComponent>();
            this.oldComponents = new List<GameComponent>();
        }

        public void AddComponent(GameComponent component)
        {
            this.newComponents.Add(component);
        }

        public void RemoveComponent(GameComponent component)
        {
            this.oldComponents.Add(component);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            if (!initialized)
            {
                this.random = new Random();
                this.content = game.Content;
                foreach (GameComponent component in this.components)
                {
                    component.Initialize();
                    this.newComponents.Add(new ZombieShip(Game, this, new Vector2(300, 150)));
                    this.newComponents.Add(new ZombieShip(Game, this, new Vector2(550, 250)));
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

            ParallaxBackground.Update(gameTime);

            if (this.random.NextDouble() < 0.01)
            {
                int sign = (2 * this.random.Next(2)) - 1;
                Vector2 position = new Vector2(400 + this.random.Next(400), sign > 0 ? -50 : Game1.SCREEN_HEIGHT + 50);
                Vector2 velocity = new Vector2(-1 * (float)(this.random.NextDouble() + 1), sign * ((float)this.random.NextDouble() + 1));
                this.AddComponent(new Asteroid(this.Game, this, position, velocity));
            }

            foreach (GameComponent component in this.components)
            {
                component.Update(gameTime);
            }

            this.components.AddRange(this.newComponents);
            this.newComponents = new List<GameComponent>();

            foreach (GameComponent oldComponent in this.oldComponents)
            {
                this.components.Remove(oldComponent);
            }
            this.oldComponents = new List<GameComponent>();
        }

        public override void Draw(GameTime gameTime)
        {
            this.Initialize();

            foreach (Layer layer in Layers.Values())
            {
                foreach (GameComponent component in this.components)
                {
                    GameJamComponent drawable = component as GameJamComponent;
                    if (drawable.Layer == layer)
                    {
                        drawable.Draw(gameTime);
                    }
                }
            }
        }

        public List<GameComponent> Components
        {
            get { return this.components; }
        }
    }
}
