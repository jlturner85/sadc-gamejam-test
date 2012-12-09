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
        Animation shipAnimationFlying;
        public PlayerShip(Game game, GameScreen screen)
            : base(game, screen, new Vector2(100, 100))
        {
            this.keyboard = new GameKeyboard();
            this.Layer = Layer.PLAYER;
        }

        private void Fire()
        {
            Vector2 bulletPos = Vector2.Add(this.Position, new Vector2(12, 3));
            Screen.AddComponent(new Bullet(Game, Screen, Team.PLAYER, bulletPos, new Vector2(3, 0)));
        }

        public override void Initialize()
        {
            //this.Sprite = Sprites.Ship;
            width = 32;
            height = 16;
            shipAnimationFlying = new Animation(this.Game.Content, "Sprites/playerShip", width, height, 2, 15);
            shipAnimationFlying.EnableRepeating();
            
        }

        public override void Update(GameTime gameTime)
        {
            keyboard.Update(gameTime);
            shipAnimationFlying.Update(gameTime);
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

            this.Velocity = velocity;

            base.Update(gameTime);

            if (keyboard.Fire.IsPressed())
            {
                
                this.Fire();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            shipAnimationFlying.Draw((this.Game as Game1).SpriteBatch, position,0f, 1.5f);
            base.Draw(gameTime);
        }
    }

}
