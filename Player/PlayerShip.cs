using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJamTest.Assets;

namespace GameJamTest.Player
{
    public class PlayerShip : DrawableGameComponent
    {
        private GameKeyboard keyboard;
        private Vector2 position;
        private Texture2D sprite;

        public PlayerShip(Game game)
            : base(game)
        {
            keyboard = new GameKeyboard();
            position = new Vector2(100, 100);
        }

        public override void Initialize()
        {
            this.sprite = Sprites.Ship;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            keyboard.Update(gameTime);

            if (keyboard.Up.IsHeld())
            {
                position.Y -= 1;
            }

            if (keyboard.Left.IsHeld())
            {
                position.X -= 1;
            }

            if (keyboard.Down.IsHeld())
            {
                position.Y += 1;
            }

            if (keyboard.Right.IsHeld())
            {
                position.X += 1;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            (Game as Game1).SpriteBatch.Draw(sprite, position, Color.White);
        }
    }

}
