using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJamTest.Assets;
using GameJamTest.Screens;

namespace GameJamTest.GameObjects.Player
{
    public class PlayerShip : GameJamComponent
    {
        private GameScreen screen;
        private GameKeyboard keyboard;
        private Vector2 position;
        private Texture2D sprite;

        public PlayerShip(GameScreen screen, Game game)
            : base(game)
        {
            this.screen = screen;
            keyboard = new GameKeyboard();
            position = new Vector2(100, 100);
            this.Layer = Layer.PLAYER;
        }

        private void Fire()
        {
            Vector2 bulletPos = Vector2.Add(this.position, new Vector2(12, 3));
            screen.AddComponent(new Bullet(Game, Team.PLAYER, bulletPos, new Vector2(3, 0)));
        }

        public override void Initialize()
        {
            this.sprite = Sprites.Ship;
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

            if (keyboard.Fire.IsPressed())
            {
                this.Fire();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            (Game as Game1).SpriteBatch.Draw(this.sprite, this.position, Color.White);
        }
    }

}
