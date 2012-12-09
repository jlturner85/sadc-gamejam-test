using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJamTest.Assets;
using GameJamTest.Screens;

namespace GameJamTest.GameObjects.Zombie
{
    public class ZombieShip : GameJamComponent
    {
        Random random;
        Animation zombieAnimationFlying;

        public ZombieType type;
        int timer;
        long pointValue;

        public ZombieShip(Game game, GameScreen screen, Vector2 position, ZombieType type)
            : base(game, screen, position)
        {
            this.random = new Random();

            this.Layer = Layer.ZOMBIE;
            
            //this.Sprite = Sprites.Zombie;
            this.type = type;

            switch (this.type)
            {
                case ZombieType.FLOATER:
                    this.width = 58;
                    this.height = 76;
                    zombieAnimationFlying = new Animation(this.Game.Content, "Sprites/paraZombie", width, height, 4, 5);
                    zombieAnimationFlying.EnableRepeating();
                    this.Velocity = new Vector2(0, 1 + this.NextFloat());
                    this.pointValue = 5;
                    break;
                case ZombieType.SHOOTER:
                    this.width = 30;
                    this.height = 17;
                    zombieAnimationFlying = new Animation(this.Game.Content, "Sprites/zombieShip", width, height, 2, 15);
                    zombieAnimationFlying.EnableRepeating();
                    this.Velocity = new Vector2(-1 * (2 + 4 * this.NextFloat()), 0);
                    this.pointValue = 10;
                    break;
                case ZombieType.SLAMMER:
                    this.width = 32;
                    this.height = 17;
                    zombieAnimationFlying = new Animation(this.Game.Content, "Sprites/kamakazi", width, height, 2, 15);
                    zombieAnimationFlying.EnableRepeating();
                    this.Velocity = new Vector2(0, -9 * (1 + 2 * this.NextFloat()));
                    this.pointValue = 25;
                    break;
            }

            this.ResetTimer();
        }

        private void Fire(float speed)
        {
            Vector2 bulletPos = Vector2.Add(this.Position, new Vector2(0, 3));
            Vector2 velocity;
            velocity = Vector2.Subtract(this.Screen.Player.Position, this.Position);
            velocity.Normalize();
            velocity = Vector2.Multiply(velocity, speed * this.Screen.GameSpeed);
            this.Screen.AddComponent(new Bullet(this.Game, this.Screen, Team.ZOMBIE, bulletPos, velocity));
        }
        
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timer--;

            if (this.timer > 0 && this.type == ZombieType.SLAMMER)
            {
                this.Velocity = new Vector2(0, MathHelper.Min(0, this.Velocity.Y + 0.5f));
            }

            if (this.timer < 0)
            {
                switch (this.type)
                {
                    case ZombieType.FLOATER:
                        this.Fire(0.2f);
                        this.ResetTimer();
                        break;
                    case ZombieType.SHOOTER:
                        this.Fire(0.5f);
                        this.ResetTimer();
                        break;
                    case ZombieType.SLAMMER:
                        if (this.Position.X > this.Screen.Player.Position.X)
                        {
                            Vector2 velocity = this.Velocity;
                            float speed = velocity.Length();
                            if (speed > 0)
                            {
                                velocity.Normalize();
                            }
                            velocity = Vector2.Multiply(velocity, 9);
                            Vector2 target = Vector2.Subtract(this.Screen.Player.Position, this.Position);
                            target.Normalize();
                            velocity = Vector2.Add(velocity, target);
                            velocity.Normalize();
                            velocity = Vector2.Multiply(velocity, speed);
                            velocity.X -= 0.15f;
                            this.Velocity = velocity;
                        }
                        break;
                }
            }
            
            zombieAnimationFlying.Update(gameTime);
        }     
           
        public override void Draw(GameTime gameTime)
        {
            float scale = 1;
            switch(this.type)
            {
                case ZombieType.FLOATER:
                    scale = 0.7f;
                    break;
                case ZombieType.SHOOTER:
                    scale = 1.5f;
                    break;
                case ZombieType.SLAMMER:
                    scale = 1.5f;
                    break;
            }
            zombieAnimationFlying.Draw((this.Game as Game1).SpriteBatch, position, 0f, scale);
            base.Draw(gameTime);
        }

        private void ResetTimer()
        {
            switch (this.type)
            {
                case ZombieType.FLOATER:
                    timer = 1800 + random.Next(3600);
                    break;
                case ZombieType.SHOOTER:
                    timer = 900 + random.Next(1200);
                    break;
                case ZombieType.SLAMMER:
                    timer = 2400 + random.Next(4800);
                    break;
            }
            timer = timer / this.Screen.GameSpeed;
        }

        private float NextFloat()
        {
            return (float)this.random.NextDouble();
        }

        public long PointValue
        {
            get { return this.pointValue; }
        }
    }

    public enum ZombieType
    {
        FLOATER, SHOOTER, SLAMMER
    }
}
