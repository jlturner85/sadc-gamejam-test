﻿using System;
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
        private Animation explosionAnimation;
        public Explosion(Game game, GameScreen screen, Vector2 position)
            : base(game, screen, position)
        {
            this.timer = 75;
            width = 64;
            height = 64;
            explosionAnimation = new Animation(this.Game.Content, "Sprites/explosion", width, height, 6, 6);
            this.Layer = Layer.EXPLOSION;
        }
        public override void Initialize()
        {
            
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            this.timer--;
            explosionAnimation.Update(gameTime);
            if (this.timer < 0)
            {
                this.Destroy();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            explosionAnimation.Draw((this.Game as Game1).SpriteBatch, position, 0f, scale);
            base.Draw(gameTime);
        }
    }
}
