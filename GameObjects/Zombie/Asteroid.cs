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
    class Asteroid : GameJamComponent
    {
        private Animation asteroidAnimation;
        public Asteroid(Game game, GameScreen screen, Vector2 position, Vector2 velocity)
            : base(game, screen, position, velocity)
        {
            this.Layer = Layer.ASTEROID;
            width = 32;
            height = 32;
            asteroidAnimation = new Animation(game.Content, "Sprites/asteroid", width, height, 4, 3);
            asteroidAnimation.EnableRepeating();
            //this.Sprite = Sprites.Asteroid;
        }

        public override void Update(GameTime gameTime)
        {
            asteroidAnimation.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            asteroidAnimation.Draw((this.Game as Game1).SpriteBatch, position, 0f, 2f);
            base.Draw(gameTime);
        }
    }
}
