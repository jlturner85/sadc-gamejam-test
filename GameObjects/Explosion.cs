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
        private int timer;

        public Explosion(Game game, GameScreen screen, Vector2 position)
            : base(game, screen, position)
        {
            this.timer = 75;
            //this.Sprite = Sprites.Boom;
        }

        public override void Update(GameTime gameTime)
        {
            this.timer--;
            if (this.timer < 0)
            {
                this.Destroy();
            }
        }
    }
}
