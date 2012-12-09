using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJamTest.Assets;
using GameJamTest.Screens;

namespace GameJamTest.GameObjects
{
    public class Explosion : GameJamComponent
    {
        private int aliveTime;
        protected Animation explosionAnimation;
        public Explosion(Game game, GameScreen screen, Vector2 position)
            : base(game, screen, position)
        {
            this.aliveTime = 0;
            width = 64;
            height = 64;
            explosionAnimation = new Animation(this.Game.Content, "Sprites/explosion", width, height, 6, 6);
            
            //this.Sprite = Sprites.Boom;
        }
        public override void Initialize()
        {
            
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            explosionAnimation.Update(gameTime);
            this.aliveTime += 1;
            if (this.aliveTime > 75)
            {
                this.Destroy();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            explosionAnimation.Draw((this.Game as Game1).SpriteBatch, position, 0f, 1);
            base.Draw(gameTime);
        }
    }
}
