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

        public Bullet(Game game, GameScreen screen, Team team, Vector2 position, Vector2 velocity)
            : base(game, screen, position, velocity)
        {
            this.team = team;
            this.Sprite = Sprites.Bullet;
            this.Layer = Layer.BULLET;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Position.X < 0 || this.Position.X > 1000)
            {
                this.Destroy();
            }

            if (team == Team.PLAYER)
            {
                foreach (GameComponent component in this.Screen.Components)
                {
                    GameJamComponent drawable = component as GameJamComponent;
                    if (drawable != null && this.Collide(drawable))
                    {
                        if (drawable is ZombieShip)
                        {
                            this.Destroy();
                            drawable.Explode();
                        }

                        if (drawable is Asteroid)
                        {
                            this.Destroy();
                        }
                    }
                }
            }
        }

        public Team Team
        {
            get { return this.team; }
        }
    }

    enum Team
    {
        PLAYER, ZOMBIE
    }
}
