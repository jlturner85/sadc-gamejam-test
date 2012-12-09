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
            //this.Sprite = Sprites.Bullet;
            this.Layer = Layer.BULLET;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (GameComponent component in this.Screen.Components)
            {
                GameJamComponent drawable = component as GameJamComponent;
                if (drawable != null && this.Collide(drawable))
                {
                    if (drawable is Asteroid)
                    {
                        this.Destroy();
                    }

                    ZombieShip zombie = drawable as ZombieShip;
                    if (zombie != null && team == Team.PLAYER)
                    {
                        this.Destroy();
                        zombie.Explode();
                        this.Screen.Player.ScorePoints(zombie.PointValue);
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
