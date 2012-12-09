using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using GameJamTest.Assets;
using GameJamTest.GameObjects.Zombie;
using GameJamTest.Screens;
using GameJamTest.Util;

namespace GameJamTest.GameObjects.Player
{
    public class PlayerShip : GameJamComponent
    {
        private long score;
        private int lives;

        private int spawnTime;
        private int invulnerableTime;
        Animation shipAnimationFlying;
        SoundEffect shipFiringSound;
        public PlayerShip(Game game, GameScreen screen)
            : base(game, screen, new Vector2(100, 100))
        {
            this.Layer = Layer.PLAYER;
        }

        private void Respawn(bool invulnerable)
        {
            this.Position = new Vector2(-50, Game1.SCREEN_HEIGHT / 2);
            this.Velocity = new Vector2(0, 0);
            this.spawnTime = 180;
            if (invulnerable)
            {
                this.invulnerableTime = 360;
            }
        }

        public override void Destroy()
        {
            if (this.lives > 0)
            {
                Respawn(true);
            }
            else
            {
                base.Destroy();
                this.Screen.Lose();
            }
        }

        public override void Explode()
        {
            if (this.invulnerableTime <= 0)
            {
                base.Explode();
            }
        }

        private void Fire()
        {
            Vector2 bulletPos = Vector2.Add(this.Position, new Vector2(12, 3));
            Screen.AddComponent(new Bullet(Game, Screen, Team.PLAYER, bulletPos, new Vector2(15, 0)));
            AudioManager.playSoundEffect(shipFiringSound);
        }

        public override void Initialize()
        {
            width = 32;
            height = 16;
            this.lives = 3;
            shipAnimationFlying = new Animation(this.Game.Content, "Sprites/playerShip", width, height, 2, 15);
            shipAnimationFlying.EnableRepeating();
            shipFiringSound = this.Game.Content.Load<SoundEffect>("SoundEffects/fire_laser1");
            Respawn(false);
        }

        public override void Update(GameTime gameTime)
        {
            shipAnimationFlying.Update(gameTime);

            Vector2 velocity = new Vector2(0, 0);

            if (this.spawnTime > 0)
            {
                this.spawnTime--;
            }
            if (this.invulnerableTime > 0)
            {
                this.invulnerableTime--;
            }

            if (this.spawnTime <= 0)
            {
                if (this.Screen.Keyboard.Up.IsHeld())
                {
                    velocity = Vector2.Add(velocity, new Vector2(0, -5));
                }

                if (this.Screen.Keyboard.Left.IsHeld())
                {
                    velocity = Vector2.Add(velocity, new Vector2(-5, 0));
                }

                if (this.Screen.Keyboard.Down.IsHeld())
                {
                    velocity = Vector2.Add(velocity, new Vector2(0, 5));
                }

                if (this.Screen.Keyboard.Right.IsHeld())
                {
                    velocity = Vector2.Add(velocity, new Vector2(5, 0));
                }

                if (this.Screen.Keyboard.Fire.IsPressed())
                {
                    this.Fire();
                }

                this.Velocity = velocity;

                base.Update(gameTime);

                this.Position = new Vector2(
                    MathHelper.Clamp(this.Position.X, 0, Game1.SCREEN_WIDTH - this.width),
                    MathHelper.Clamp(this.Position.Y, 0, Game1.SCREEN_HEIGHT - this.height)
                );
            }
            else if (this.spawnTime == 60)
            {
                this.lives--;
            }
            else if (this.spawnTime < 60)
            {
                this.Velocity = new Vector2(this.spawnTime / 6f, 0);

                base.Update(gameTime);
            }

            foreach (GameComponent component in this.Screen.Components)
            {
                GameJamComponent drawable = component as GameJamComponent;
                if (drawable != null && this.Collide(drawable))
                {
                    Bullet bullet = drawable as Bullet;
                    if (bullet != null && bullet.Team == Team.ZOMBIE)
                    {
                        this.Explode();
                        bullet.Destroy();
                    }

                    if (drawable is Asteroid)
                    {
                        this.Explode();
                    }

                    if (drawable is ZombieShip)
                    {
                        this.Explode();
                        drawable.Explode();
                    }
                }
            }
        }

        public void ScorePoints(long points)
        {
            this.score += points;
        }

        public int Lives
        {
            get { return this.lives; }
        }

        public long Score
        {
            get { return this.score; }
        }

        public override void Draw(GameTime gameTime)
        {
            shipAnimationFlying.Draw((this.Game as Game1).SpriteBatch, position, 0f, 1.5f);
            base.Draw(gameTime);
        }
    }

}
