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
    class ZombieShip : GameJamComponent
    {
        Animation zombieAnimationFlying;
        public ZombieShip(Game game, GameScreen screen, Vector2 position)
            : base(game, screen, position)
        {
            this.Layer = Layer.ZOMBIE;
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
        {
            Vector2 bulletPos = Vector2.Add(this.Position, new Vector2(0, 3));
            this.Screen.AddComponent(new Bullet(Game, Screen, Team.PLAYER, bulletPos, new Vector2(-3, 0)));
        }

        public override void Update(GameTime gameTime)
        {
            zombieAnimationFlying.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            zombieAnimationFlying.Draw((this.Game as Game1).SpriteBatch, position, 0f, 1.5f);
            base.Draw(gameTime);
        }


    }
}
