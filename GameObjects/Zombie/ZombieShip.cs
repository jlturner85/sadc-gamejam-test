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
        private GameScreen screen;

        public ZombieShip(Game game, GameScreen screen, Vector2 position)
            : base(game, screen)
        {
            this.screen = screen;
            this.Position = position;
            this.Layer = Layer.ZOMBIE;
            this.Sprite = Sprites.Zombie;
        }

        private void Fire()
        {
            Vector2 bulletPos = Vector2.Add(this.Position, new Vector2(0, 3));
            screen.AddComponent(new Bullet(Game, Screen, Team.PLAYER, bulletPos, new Vector2(-3, 0)));
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
