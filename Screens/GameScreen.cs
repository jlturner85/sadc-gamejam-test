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
        private GameKeyboard keyboard;

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
        private int bossCountdown;
        private int lose;
        private int timeout;

        Animation shipAnimationStatic;

        public GameScreen(Game game)
            : base(game)
        {
            this.game = game;
        }

        public void AddComponent(GameComponent component)
        {
            this.newComponents.Add(component);
        }

        public void RemoveComponent(GameComponent component)
        {
            this.oldComponents.Add(component);
        }

        private Boss FindBoss()
        {
            foreach (GameComponent component in components)
            {
                Boss boss = component as Boss;
                if (boss != null)
                {
                    return boss;
                }
            }

            return null;
        }

        public void Lose()
        {
            this.lose = 240;
            Boss boss = this.FindBoss();
            if (boss != null)
            {
                boss.Layer = Layer.BEHIND_TEXT;
            }
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            if (!initialized)
            {
                this.keyboard = new GameKeyboard();

                this.components = new List<GameComponent>();
                this.player = new PlayerShip(game, this);
                this.components.Add(this.Player);
                this.newComponents = new List<GameComponent>();
                this.oldComponents = new List<GameComponent>();
                this.lose = -1;

                this.random = new Random();
                this.content = game.Content;

                this.gameSpeed = 10;
                this.ResetSpeedTimer();
                this.ResetBossTimer();

                shipAnimationStatic = new Animation(this.Game.Content, "Sprites/playerShip", 32, 16, 1, 1);

                foreach (GameComponent component in this.components)
                {
                    component.Initialize();
                }

                initialized = true;
            }
        }

        private bool NoZombies()
        {
            foreach (GameComponent component in this.Components)
            {
                if (component is ZombieShip || component is Boss)
                {
                    return false;
                }
            }

            return true;
        }

        private void SpawnBoss()
        {
            Boss boss = new Boss(this.Game, this);
            this.AddComponent(boss);
            boss.Initialize();
        }

        public void ScreenExplosion()
        {
            this.ResetBossTimer();
            this.timeout = 180;
        }

        private void ResetSpeedTimer()
        {
            this.speedUpTimer = (150 * this.GameSpeed) - 300;
        }

        private void ResetBossTimer()
        {
            //this.bossCountdown = (250 * this.GameSpeed) + 2500;
            this.bossCountdown = 0;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            this.Initialize();

            this.keyboard.Update(gameTime);

            if (this.Keyboard.Back.IsPressed())
            {
                (this.Game as Game1).setCurrentScreen(Game1.menuScreenID);
                this.initialized = false;
            }

            if (speedDisplayGrow > 0)
            {
                speedDisplayGrow--;
            }

            if (timeout > 0)
            {
                timeout--;
                if (timeout == 105)
                {
                    foreach (GameComponent component in this.Components)
                    {
                        if (component is ZombieShip || component is Bullet || component is Asteroid || component is Explosion)
                        {
                            this.RemoveComponent(component);
                        }
                    }
                }
            }

            if (this.lose > 0)
            {
                this.lose--;
            }
            else if (this.lose < 0)
            {
                this.speedUpTimer--;

                if (speedUpTimer == 30)
                {
                    this.speedDisplayGrow = 60;
                }
                if (speedUpTimer < 0)
                {
                    this.gameSpeed++;
                    this.ResetSpeedTimer();
                }

                if (this.bossCountdown > 0)
                {
                    this.bossCountdown--;
                }
                else if (this.NoZombies())
                {
                    this.SpawnBoss();
                }
            }

            ParallaxBackground.Update(gameTime, this.GameSpeed);

            if (timeout <= 0)
            {
                if (this.random.NextDouble() < (0.001 * this.GameSpeed) && this.lose < 0)
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
                    else if (this.bossCountdown > 0)
                    {
                        if (type < 6)
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

            SpriteBatch spriteBatch = (this.Game as Game1).SpriteBatch;
            SpriteFont font = Fonts.TitleFont;

            (this.Game as Game1).SpriteBatch.DrawString(font, "Boss Countdown: " + this.bossCountdown, new Vector2(500, 0), Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0f); // debug

            (this.Game as Game1).SpriteBatch.DrawString(font, "Score: " + this.Player.Score, new Vector2(0, 0), Color.CornflowerBlue);
            String speed = "Speed x" + this.GameSpeed;
            speed = speed.Insert(speed.Length - 1, ".");
            float size = 1 + (speedDisplayGrow / 50f) - (speedDisplayGrow * speedDisplayGrow / 3000f);
            float length = font.MeasureString(speed).X * size;
            spriteBatch.DrawString(Fonts.TitleFont, speed, new Vector2(Game1.SCREEN_WIDTH - length, 0), Color.Salmon, 0f, new Vector2(0, 0), size, SpriteEffects.None, 0f);

            int i = 0;
            while (i < this.Player.Lives) {
                shipAnimationStatic.Draw(spriteBatch, new Vector2(i * 50, Game1.SCREEN_HEIGHT - 28), 0f, 1.5f);
                i++;
            }

            foreach (Layer layer in Layers.Values())
            {
                foreach (GameComponent component in this.components)
                {
                    GameJamComponent drawable = component as GameJamComponent;
                    if (drawable.Layer == layer)
                    {
                        drawable.Draw(gameTime);
                    }

                    if (layer == Layer.BEHIND_TEXT)
                    {
                        this.DrawGameOverText(spriteBatch, font);
                    }
                }
            }

            if (timeout > 0)
            {
                Texture2D texture = new Texture2D(this.Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                Color[] colors = new Color[1];
                int n = 90 - Math.Abs(timeout - 90);
                int r = (int)MathHelper.Clamp(6 * n, 0, 255);
                int gb = (int)MathHelper.Clamp(6 * n - 255, 0, 255);
                int a = (int)MathHelper.Clamp(3 * n, 0, 255);
                colors[0] = Color.FromNonPremultiplied(r, gb, gb, a);
                texture.SetData<Color>(colors);
                spriteBatch.Draw(texture, new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), Color.White);
            }
        }

        private void DrawGameOverText(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (this.lose >= 0)
            {
                if (this.lose < 180)
                {
                    String s1 = "You lost the game";
                    float length1 = font.MeasureString(s1).X;
                    (this.Game as Game1).SpriteBatch.DrawString(font, s1, new Vector2((Game1.SCREEN_WIDTH - length1 * 2) / 2, (Game1.SCREEN_HEIGHT / 2) - 100), Color.Yellow, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
                }
                if (this.lose < 90)
                {
                    String s1 = "Try again next time.";
                    float length1 = font.MeasureString(s1).X;
                    (this.Game as Game1).SpriteBatch.DrawString(font, s1, new Vector2((Game1.SCREEN_WIDTH - length1) / 2, (Game1.SCREEN_HEIGHT / 2) - 25), Color.White);
                }
                if (this.lose == 0)
                {
                    String s1 = "See you!";
                    float length1 = font.MeasureString(s1).X;
                    (this.Game as Game1).SpriteBatch.DrawString(font, s1, new Vector2((Game1.SCREEN_WIDTH - length1) / 2, (Game1.SCREEN_HEIGHT / 2) + 10), Color.White);
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

        public GameKeyboard Keyboard
        {
            get { return this.keyboard; }
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
