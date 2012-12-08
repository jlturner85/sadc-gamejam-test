using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJamTest.Screens;

namespace GameJamTest.GameObjects
{
    public class GameJamComponent : DrawableGameComponent
    {
        private Vector2 position;
        private Texture2D sprite;
        private Layer layer;
        private GameScreen screen;

        public GameJamComponent(Game game, GameScreen screen)
            : base(game)
        {
            this.Screen = screen;
        }

        public bool Collide(GameJamComponent that)
        {
            Rectangle r1 = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Sprite.Width, this.Sprite.Height);
            Rectangle r2 = new Rectangle((int)that.Position.X, (int)that.Position.Y, that.Sprite.Width, that.Sprite.Height);
            return r1.Intersects(r2);
        }

        public void Destroy()
        {
            this.Screen.RemoveComponent(this);
        }

        public override void Draw(GameTime gameTime)
        {
            (Game as Game1).SpriteBatch.Draw(this.Sprite, this.Position, Color.White);
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Texture2D Sprite
        {
            get { return this.sprite; }
            set { this.sprite = value; }
        }

        public Layer Layer
        {
            get { return this.layer; }
            set { this.layer = value; }
        }

        public GameScreen Screen
        {
            get { return this.screen; }
            set { this.screen = value; }
        }
    }
}
