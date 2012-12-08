using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJamTest.Assets;

namespace GameJamTest.GameObjects
{
    class Bullet : GameJamComponent
    {
        private Team team;
        private Vector2 position;
        private Vector2 velocity;
        private Texture2D sprite;

        public Bullet(Game game, Team team, Vector2 position, Vector2 velocity)
            : base(game)
        {
            this.team = team;
            this.position = position;
            this.velocity = velocity;
            this.sprite = Sprites.Bullet;
            this.Layer = Layer.BULLET;
        }

        public override void Update(GameTime gameTime)
        {
            this.position = Vector2.Add(position, velocity);
        }

        public override void Draw(GameTime gameTime)
        {
            (Game as Game1).SpriteBatch.Draw(this.sprite, this.position, Color.White);
        }
    }

    enum Team
    {
        PLAYER, ZOMBIE
    }
}
