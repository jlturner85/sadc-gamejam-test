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
    /// 
    public class GameScreen : DrawableGameComponent
    {
        public static int screenNumber = 2;
        private bool initialized = false;

        private Random random;

        private Game game;
        private ContentManager content;

        private PlayerShip player;
        private List<GameComponent> components;
        private List<GameComponent> newComponents;
        private List<GameComponent> oldComponents;

        private int gameSpeed;
        private int speedUpTimer;
        private int speedDisplayGrow;

        public GameScreen(Game game)
            : base(game)
        {
            this.game = game;
            this.components = new List<GameComponent>();
            this.player = new PlayerShip(game, this);
            this.components.Add(this.Player);
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

                this.gameSpeed = 10;
                this.speedUpTimer = 2400;

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

            this.speedUpTimer--;
            if (speedDisplayGrow > 0)
            {
                speedDisplayGrow--;
            }

            if (speedUpTimer < 0)
            {
                this.gameSpeed++;
                this.speedUpTimer = 2400;
                this.speedDisplayGrow = 60;
            }

            ParallaxBackground.Update(gameTime);

            if (this.random.NextDouble() < (0.001 * this.GameSpeed))
            {
                int halfScreenWidth = Game1.SCREEN_WIDTH / 2;

                int type = this.random.Next(10);
                if (type < 2)
                {
                    float sign = (0.12f * this.random.Next(2)) - 0.06f;
                    Vector2 position = new Vector2(halfScreenWidth + this.random.Next(halfScreenWidth), sign > 0 ? -50 : Game1.SCREEN_HEIGHT + 50);
                    Vector2 velocity = new Vector2(-0.06f * this.GameSpeed * (this.NextFloat() + 0.5f), sign * this.GameSpeed * (this.NextFloat() + 0.5f));
                    this.AddComponent(new Asteroid(this.Game, this, position, velocity));
                }
                else if (type < 6)
                {
                    this.AddComponent(new ZombieShip(this.Game, this, new Vector2(halfScreenWidth + this.random.Next(halfScreenWidth), -50), ZombieType.FLOATER));
                }
                else if (type < 9)
                {
                    this.AddComponent(new ZombieShip(this.Game, this, new Vector2(Game1.SCREEN_WIDTH + 50, this.random.Next(Game1.SCREEN_HEIGHT)), ZombieType.SHOOTER));
                }
                else
                {
                    this.AddComponent(new ZombieShip(this.Game, this, new Vector2(halfScreenWidth + this.random.Next(halfScreenWidth), Game1.SCREEN_HEIGHT + 50), ZombieType.SLAMMER));
                }
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
            
            SpriteFont font = Fonts.TitleFont;

            (this.Game as Game1).SpriteBatch.DrawString(font, "Score: " + this.Player.Score, new Vector2(0, 0), Color.CornflowerBlue);
            String speed = "Speed x" + this.GameSpeed;
            speed = speed.Insert(speed.Length - 1, ".");
            float size = 1 + (speedDisplayGrow / 50f) - (speedDisplayGrow * speedDisplayGrow / 3000f);
            float length = font.MeasureString(speed).X * size;
            (this.Game as Game1).SpriteBatch.DrawString(Fonts.TitleFont, speed, new Vector2(Game1.SCREEN_WIDTH - length, 0), Color.Salmon, 0f, new Vector2(0, 0), size, SpriteEffects.None, 0f);

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

        private float NextFloat()
        {
            return (float)this.random.NextDouble();
        }

        public PlayerShip Player
        {
            get { return this.player; }
        }

        public int GameSpeed
        {
            get { return this.gameSpeed; }
        }

        public List<GameComponent> Components
        {
            get { return this.components; }
        }
    }
}
