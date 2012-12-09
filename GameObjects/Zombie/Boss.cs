using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using GameJamTest.Assets;
using GameJamTest.Util;
using GameJamTest.Screens;

namespace GameJamTest.GameObjects.Zombie
{
    public class Boss : GameJamComponent
    {
        private const int BAR_WIDTH = 616;
        private const int BAR_HEIGHT = 20;
        private Animation bossAnimation;
        private const int INITIAL_X = 735;
        private const int INITIAL_Y = 260;
        private int xSign;
        private int ySign;
        SoundEffect shipExplosion;
        SoundEffect shipEntering;
        private Random random;
        private Texture2D sprite;

        private int hp;
        private int maxHp;

        private int explode;
        private int spawnTime;
        private long aliveTime;
        private int flash;
        
        public Boss(Game game, GameScreen screen)
            : base(game, screen, new Vector2(Game1.SCREEN_WIDTH + 50, (Game1.SCREEN_HEIGHT / 2) - 100))
        {
            this.random = new Random();

            this.height = 150;
            this.width = 150;
            this.scale = 3f;
            this.Layer = Layer.ZOMBIE;

            this.xSign = (400 * this.random.Next(2)) - 200;
            this.ySign = (200 * this.random.Next(2)) - 100;

            //this.sprite = Sprites.skull;
            this.flash = 0;
            this.explode = -1;
            this.spawnTime = 120;
            this.aliveTime = 0;
            this.hp = (int)(2.5f * this.Screen.GameSpeed);
            this.maxHp = this.hp;
            
        }

        public override void Initialize()
        {
            bossAnimation = new Animation(this.Game.Content, "Sprites/boss1", 150, 150, 8, 7);
            bossAnimation.EnableRepeating();
            shipEntering = this.Game.Content.Load<SoundEffect>("SoundEffects/zombiedemon");
            shipExplosion = this.Game.Content.Load<SoundEffect>("SoundEffects/cannon");
            AudioManager.playSoundEffect(shipEntering);
            base.Initialize();
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.flash > 0)
            {
                this.flash--;
            }

            if (this.spawnTime > 0)
            {
                this.spawnTime--;
                this.Velocity = new Vector2(-this.spawnTime / 12f, 0);
            }
            else if (this.explode > 0)
            {
                this.explode--;
                this.Velocity = new Vector2(0, 0.5f);
                if (this.explode > 100 && this.explode % 30 == 0)
                {
                    AudioManager.playSoundEffect(shipExplosion);
                    this.NewExplosion();
                }
                else if (this.explode > 40 && this.explode % 20 == 0)
                {
                    AudioManager.playSoundEffect(shipExplosion);
                    this.NewExplosion();
                }
                else if (this.explode == 75)
                {
                    AudioManager.playSoundEffect(shipExplosion);
                    this.Screen.ScreenExplosion();
                }
                else if (this.explode > 0 && this.explode % 10 == 0)
                {
                    AudioManager.playSoundEffect(shipExplosion);
                    this.NewExplosion();
                }
                else if (this.explode == 0)
                {
                    
                    this.Destroy();
                }
            }
            else
            {
                this.aliveTime += this.Screen.GameSpeed;
                this.Position = new Vector2(INITIAL_X + xSign * (float)Math.Sin(MathHelper.Pi * this.aliveTime / 7200),
                    INITIAL_Y + ySign * (float)Math.Sin(MathHelper.Pi * this.aliveTime / 3600));
            }
            bossAnimation.Update(gameTime);
            base.Update(gameTime);
        }

        private void NewExplosion()
        {
            Explosion explosion = new Explosion(this.Game, this.Screen, Vector2.Add(this.Position, new Vector2(this.random.Next(this.width*(int)scale), this.random.Next(this.height*(int)scale))));
            this.Screen.AddComponent(explosion);
        }

        public override void Explode()
        {
            this.explode = 180;
            long points = 500 * ((2 * this.Screen.GameSpeed) - 10);
            this.Screen.Player.ScorePoints(points);
            this.Screen.AddComponent(new ScoreDisplay(this.Game, this.Screen, this.Position, points, 1));
        }

        public void Damage()
        {
            this.hp--;
            this.flash = 2;

            if (this.hp <= 0)
            {
                this.Explode();
            }
        }

        private float NextFloat()
        {
            return (float)this.random.NextDouble();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (this.Game as Game1).SpriteBatch;
            bossAnimation.Draw((this.Game as Game1).SpriteBatch, position,0, scale);
            //spriteBatch.Draw(sprite, this.Position, (flash > 0 ? Color.Red : Color.White));
            this.DrawHealthBar(gameTime);
        }

        private void DrawHealthBar(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (this.Game as Game1).SpriteBatch;

            spriteBatch.Draw(Sprites.lifebarBg, new Vector2(610, 690), Color.White);
            int missingPiece = (int)(BAR_WIDTH * (1 - ((float)this.hp / (float)this.maxHp)));

            spriteBatch.Draw(Sprites.lifebar, new Vector2(616 + missingPiece, 695),
                new Rectangle(missingPiece, 0, 616 - missingPiece, BAR_HEIGHT), Color.White);
        }

        public bool Alive
        {
            get { return this.explode < 0; }
        }
    }
}
