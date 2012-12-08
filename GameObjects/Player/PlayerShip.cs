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
        private GameKeyboard keyboard;

        public PlayerShip(Game game, GameScreen screen)
            : base(game, screen)
        {
            this.Screen = screen;
            this.keyboard = new GameKeyboard();
            this.Position = new Vector2(100, 100);
            this.Layer = Layer.PLAYER;
        }

        private void Fire()
        {
            Vector2 bulletPos = Vector2.Add(this.Position, new Vector2(12, 3));
            Screen.AddComponent(new Bullet(Game, Screen, Team.PLAYER, bulletPos, new Vector2(3, 0)));
        }

        public override void Initialize()
        {
            this.Sprite = Sprites.Ship;
        }

        public override void Update(GameTime gameTime)
        {
            keyboard.Update(gameTime);

            Vector2 velocity = new Vector2(0, 0);

            if (keyboard.Up.IsHeld())
            {
                velocity = Vector2.Add(velocity, new Vector2(0, -1));
            }

            if (keyboard.Left.IsHeld())
            {
                velocity = Vector2.Add(velocity, new Vector2(-1, 0));
            }

            if (keyboard.Down.IsHeld())
            {
                velocity = Vector2.Add(velocity, new Vector2(0, 1));
            }

            if (keyboard.Right.IsHeld())
            {
                velocity = Vector2.Add(velocity, new Vector2(1, 0));
            }

            this.Position = Vector2.Add(this.Position, velocity);

            if (keyboard.Fire.IsPressed())
            {
                this.Fire();
            }
        }
    }

}
