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
using GameJamTest.Util;

namespace GameJamTest.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    /// 
    public class GameScreen : Screen
    {
        private bool initialized = false;

        private Random random;
        
        private ContentManager content;
        //private GameKeyboard keyboard;
        private PlayerShip player;
        private Arsenal arsenal;
        private List<GameComponent> components;
        private List<GameComponent> newComponents;
        private List<GameComponent> oldComponents;

        private int gameSpeed;
        private Song bossSong;
        private Song gameSong;
        private Song menuSong;
        private int intro;
        private int speedUpTimer;
        private int speedDisplayGrow;
        private int bossCountdown;
        private int lose;
        private int timeout;

        Animation shipAnimationStatic;

        public GameScreen(Game1 game)
            : base(game)
        {
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
                bossSong = Game.Content.Load<Song>("Music/vengeance");
                gameSong = Game.Content.Load<Song>("Music/menumusic");
                menuSong = Game.Content.Load<Song>("Music/shadowforce");
                this.components = new List<GameComponent>();
                this.player = new PlayerShip(Game, this);
                this.arsenal = new Arsenal(Game, this);
                this.components.Add(this.Player);
                this.components.Add(this.arsenal);
                this.newComponents = new List<GameComponent>();
                this.oldComponents = new List<GameComponent>();
                this.lose = -1;

                this.random = new Random();
                this.content = Game.Content;

                this.gameSpeed = 10;
                this.ResetSpeedTimer();
                this.ResetBossTimer();
                this.intro = 300;

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
            AudioManager.stopMusic();
            AudioManager.playMusic(bossSong);
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
            AudioManager.stopMusic();
            AudioManager.playMusic(gameSong);
            this.bossCountdown = (250 * this.GameSpeed) + 2500;
           
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            this.Initialize();
            //TODO change this to option menu?
            if ((this.Game as Game1).Keyboard.Back.IsPressed() && this.Alive)
            {
                AudioManager.stopMusic();
                AudioManager.playMusic(menuSong);
                Show(false);
                Game.ScreenManager.ParallaxBackground.gameSpeed = 10;
                Game.ScreenManager.MenuScreen.Show(true);
                this.initialized = false;
            }

            if (this.intro > 0)
            {
                this.intro--;
                this.arsenal.Velocity = new Vector2(-MathHelper.Clamp(40 - (intro / 5f), 0, 10), 0);
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

            Game.ScreenManager.ParallaxBackground.gameSpeed = gameSpeed;

            if (timeout <= 0 && this.intro <= 0)
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
                            this.AddComponent(new ZombieShip(this.Game, this, new Vector2(halfScreenWidth + this.random.Next(halfScreenWidth), -60), ZombieType.FLOATER));
                        }
                        else if (type < 9)
                        {
                            this.AddComponent(new ZombieShip(this.Game, this, new Vector2(Game1.SCREEN_WIDTH + 10, this.random.Next(Game1.SCREEN_HEIGHT)), ZombieType.SHOOTER));
                        }
                        else
                        {
                            this.AddComponent(new ZombieShip(this.Game, this, new Vector2(halfScreenWidth + this.random.Next(halfScreenWidth), Game1.SCREEN_HEIGHT + 10), ZombieType.SLAMMER));
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

            if (!this.Alive && player.Score > LeaderboardScreen.LowestScore() && this.lose < 100) {
                UpdateNameInput();
                
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.Initialize();

            SpriteBatch spriteBatch = (this.Game as Game1).SpriteBatch;
            SpriteFont font = Fonts.TitleFont;

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
                }

                if (layer == Layer.BEHIND_TEXT)
                {
                    this.DrawGameOverText(spriteBatch, font);
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

        #region GAME OVER STUFF
        // initial name variable and char array for iteration
        char[] name = {'A','A','A'};
        int pos = 0;
        int nameCharPos = 0;
        char[] nameChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?.$ ".ToCharArray();
        //method for updating name input
        private void UpdateNameInput() {
            #region Up/Down
            
            if (Game.Keyboard.Up.IsPressed()) {
                // up was pressed, so go forward nameChar
                if (nameCharPos == nameChars.Count() - 1) {
                    // last position, set to first.
                    nameCharPos = 0;
                } else {
                    nameCharPos++;
                }
                name[pos] = nameChars[nameCharPos];
                AudioManager.playSoundEffect(Player.shipFiringSound);
            } else if (Game.Keyboard.Down.IsPressed()) {
                // down was pressed, go back a nameChar
                if (nameCharPos == 0) {
                    // first position, set to last.
                    nameCharPos = nameChars.Count() - 1;
                } else {
                    nameCharPos--;
                }
                name[pos] = nameChars[nameCharPos];
                AudioManager.playSoundEffect(Player.shipFiringSound);
            }
            #endregion
            #region Left/Right
            if (Game.Keyboard.Right.IsPressed()) {
                if (pos == 2) {
                    // last position, set to first.
                    pos = 0;
                } else {
                    pos++;
                }
            } else if (Game.Keyboard.Left.IsPressed()) {
                if (pos == 0) {
                    // last position, set to first.
                    pos = 2;
                } else {
                    pos--;
                }
            }
            #endregion
            if (Game.Keyboard.Fire.IsPressed()) {
                //save and go back to leaderboard
                Show(false);
                Game.ScreenManager.ParallaxBackground.gameSpeed = 10;
                Game.ScreenManager.LeaderboardScreen.Show(true);
                this.initialized = false;
            }
        }
        private void DrawNameInput(SpriteBatch spriteBatch, SpriteFont font) {
            //method for drawing name input
            String initials = String.Format("NAME: {0}", new string(name));
            float initialsLength = font.MeasureString(initials).X;
            (this.Game as Game1).SpriteBatch.DrawString(font, initials, new Vector2((Game1.SCREEN_WIDTH - initialsLength * 1.7f) / 2 + 250, (Game1.SCREEN_HEIGHT / 2) + 50), Color.Chartreuse, 0f, new Vector2(0, 0), 1.7f, SpriteEffects.None, 0f);
        }
        private void DrawHighScoreGet(SpriteBatch spriteBatch, SpriteFont font) {
            //Display high score get message
            String messageString = "HIGH SCORE GET!";
            float messageLength = font.MeasureString(messageString).X;
            (this.Game as Game1).SpriteBatch.DrawString(font, messageString, new Vector2((Game1.SCREEN_WIDTH - messageLength * 2) / 2, (Game1.SCREEN_HEIGHT / 2) - 25), Color.Salmon, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);

            // display rank
            String rankString = String.Format("RANK: {0}", "1ST");
            float rankLength = font.MeasureString(rankString).X;
            (this.Game as Game1).SpriteBatch.DrawString(font, rankString, new Vector2((Game1.SCREEN_WIDTH - rankLength * 1.7f) / 2 - 300, (Game1.SCREEN_HEIGHT / 2) + 50), Color.Chartreuse, 0f, new Vector2(0, 0), 1.7f, SpriteEffects.None, 0f);

            DrawNameInput(spriteBatch, font);
        }

        private void DrawGameOverText(SpriteBatch spriteBatch, SpriteFont font) {
            if (this.lose >= 0) {

                if (this.lose < 180) {
                    //present game over text
                    String s1 = "You lost the game";
                    float length1 = font.MeasureString(s1).X;
                    (this.Game as Game1).SpriteBatch.DrawString(font, s1, new Vector2((Game1.SCREEN_WIDTH - length1 * 2) / 2, (Game1.SCREEN_HEIGHT / 2) - 100), Color.Yellow, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
                }

                if (player.Score > LeaderboardScreen.LowestScore() && this.lose < 100) {
                    DrawHighScoreGet(spriteBatch, font);

                } else {
                    if (this.lose < 90) {
                        String s1 = "Try again next time.";
                        float length1 = font.MeasureString(s1).X;
                        (this.Game as Game1).SpriteBatch.DrawString(font, s1, new Vector2((Game1.SCREEN_WIDTH - length1) / 2, (Game1.SCREEN_HEIGHT / 2) - 25), Color.White);
                    }
                    if (this.lose == 0) {
                        String s1 = "See you!";
                        float length1 = font.MeasureString(s1).X;
                        (this.Game as Game1).SpriteBatch.DrawString(font, s1, new Vector2((Game1.SCREEN_WIDTH - length1) / 2, (Game1.SCREEN_HEIGHT / 2) + 10), Color.White);
                    }
                }

            }
        }
        #endregion
        

        private float NextFloat()
        {
            return (float)this.random.NextDouble();
        }

        public int Intro
        {
            get { return this.intro; }
        }

        public bool Alive
        {
            get { return this.lose < 0 ; }
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
