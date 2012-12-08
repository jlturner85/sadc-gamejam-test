using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJamTest.Assets;
using GameJamTest.GameObjects.Zombie;
using GameJamTest.Screens;

namespace GameJamTest.GameObjects
{
    class Bullet : GameJamComponent
    {
        private Team team;
        private Vector2 velocity;

        public Bullet(Game game, GameScreen screen, Team team, Vector2 position, Vector2 velocity)
            : base(game, screen)
        {
            this.team = team;
            this.Position = position;
            this.velocity = velocity;
            this.Sprite = Sprites.Bullet;
            this.Layer = Layer.BULLET;
        }

        public override void Update(GameTime gameTime)
        {
            this.Position = Vector2.Add(Position, velocity);

            if (this.Position.X < 0 || this.Position.X > 1000)
            {
                this.Destroy();
            }

            if (team == Team.PLAYER)
            {
                foreach (GameComponent component in this.Screen.Components)
                {
                    GameJamComponent drawable = component as GameJamComponent;
                    if (drawable != null)
                    {
                        if ((drawable as ZombieShip) != null && this.Collide(drawable))
                        {
                            this.Destroy();
                            drawable.Destroy();
                            this.Screen.AddComponent(new Explosion(this.Game, this.Screen, Vector2.Add(drawable.Position, new Vector2(-4, -2))));
                        }
                    }
                }
            }
        }
    }

    enum Team
    {
        PLAYER, ZOMBIE
    }
}
