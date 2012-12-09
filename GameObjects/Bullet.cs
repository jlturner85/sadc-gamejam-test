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
        private Animation bulletAnimation;
        public Bullet(Game game, GameScreen screen, Team team, Vector2 position, Vector2 velocity)
            : base(game, screen, position, velocity)
        {
            this.team = team;
            
            //this.Sprite = Sprites.Bullet;
            if (team == Team.PLAYER)
            {
                //player shot
                width = 20;
                height = 8;
                bulletAnimation = new Animation(this.Game.Content, "Sprites/playershot2", width, height, 2, 15);
                
            }
            else
            {
                //enemy shot
                width = 17;
                height = 17;
                bulletAnimation = new Animation(this.Game.Content, "Sprites/enemyshot", width, height, 8, 15);
                
            }
            bulletAnimation.EnableRepeating();
            this.Layer = Layer.BULLET;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            bulletAnimation.Update(gameTime);
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
                        long points = zombie.PointValue * this.Screen.GameSpeed;
                        this.Screen.Player.ScorePoints(points);
                        this.Screen.AddComponent(new ScoreDisplay(this.Game, this.Screen, this.Position, points));
                    }
                }
            }
        }

        public Team Team
        {
            get { return this.team; }
        }

        public override void Draw(GameTime gameTime)
        {
            bulletAnimation.Draw((this.Game as Game1).SpriteBatch, position, 0f, 1f);
            base.Draw(gameTime);
        }
    }

    
    enum Team
    {
        PLAYER, ZOMBIE
    }
}
