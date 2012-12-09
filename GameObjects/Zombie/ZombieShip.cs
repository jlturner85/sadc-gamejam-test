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
<<<<<<< HEAD
        Random random;

        ZombieType type;
        int timer;
        long pointValue;

        public ZombieShip(Game game, GameScreen screen, Vector2 position, ZombieType type)
=======
        Animation zombieAnimationFlying;
        public ZombieShip(Game game, GameScreen screen, Vector2 position)
>>>>>>> 3f5fe3409cad7ff40b14dd02359fc623a565de14
            : base(game, screen, position)
        {
            this.random = new Random();

            this.Layer = Layer.ZOMBIE;
<<<<<<< HEAD
            this.Sprite = Sprites.Zombie;
            this.type = type;

            switch (this.type)
            {
                case ZombieType.FLOATER:
                    this.Velocity = new Vector2(0, 1 + this.NextFloat());
                    this.pointValue = 50;
                    break;
                case ZombieType.SHOOTER:
                    this.Velocity = new Vector2(-1 * (2 + 4 * this.NextFloat()), 0);
                    this.pointValue = 100;
                    break;
                case ZombieType.SLAMMER:
                    this.Velocity = new Vector2(0, -9 * (1 + 2 * this.NextFloat()));
                    this.pointValue = 250;
                    break;
            }

            this.ResetTimer();
        }

        private void Fire(float speed)
=======
            this.width = 30;
            this.height = 17;
            zombieAnimationFlying = new Animation(this.Game.Content, "Sprites/zombieShip", width, height, 2, 15);
            zombieAnimationFlying.EnableRepeating();
            //this.Sprite = Sprites.Zombie;
        }
        public override void Initialize()
        {
            
            
            base.Initialize();
        }
        private void Fire()
>>>>>>> 3f5fe3409cad7ff40b14dd02359fc623a565de14
        {
            Vector2 bulletPos = Vector2.Add(this.Position, new Vector2(0, 3));
            Vector2 velocity;
            velocity = Vector2.Subtract(this.Screen.Player.Position, this.Position);
            velocity.Normalize();
            velocity = Vector2.Multiply(velocity, speed);
            this.Screen.AddComponent(new Bullet(this.Game, this.Screen, Team.ZOMBIE, bulletPos, velocity));
        }

        public override void Update(GameTime gameTime)
        {
<<<<<<< HEAD
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
                        this.Fire(2);
                        this.ResetTimer();
                        break;
                    case ZombieType.SHOOTER:
                        this.Fire(5);
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
        }

        private void ResetTimer()
        {
            switch (this.type)
            {
                case ZombieType.FLOATER:
                    timer = 180 + random.Next(360);
                    break;
                case ZombieType.SHOOTER:
                    timer = 90 + random.Next(120);
                    break;
                case ZombieType.SLAMMER:
                    timer = 240 + random.Next(480);
                    break;
            }
        }

        private float NextFloat()
        {
            return (float)this.random.NextDouble();
        }

        public long PointValue
        {
            get { return this.pointValue; }
=======
            zombieAnimationFlying.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            zombieAnimationFlying.Draw((this.Game as Game1).SpriteBatch, position, 0f, 1.5f);
            base.Draw(gameTime);
>>>>>>> 3f5fe3409cad7ff40b14dd02359fc623a565de14
        }


    }

    public enum ZombieType
    {
        FLOATER, SHOOTER, SLAMMER
    }
}
