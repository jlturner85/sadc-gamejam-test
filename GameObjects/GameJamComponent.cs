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
        protected Vector2 position;
        private Vector2 velocity;
        //private Texture2D sprite;
        private Layer layer;
        private GameScreen screen;
        public int height;
        public int width;
        public GameJamComponent(Game game, GameScreen screen, Vector2 position)
            : this(game, screen, position, new Vector2(0, 0))
        {
        }

        public GameJamComponent(Game game, GameScreen screen, Vector2 position, Vector2 velocity)
            : base(game)
        {
            this.Screen = screen;
            this.Position = position;
            this.Velocity = velocity;
        }

        public bool Collide(GameJamComponent that)
        {
            Rectangle r1 = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.width, this.height);
            Rectangle r2 = new Rectangle((int)that.Position.X, (int)that.Position.Y, that.width, that.height);
            return r1.Intersects(r2);
        }

        public void Destroy()
        {
            this.Screen.RemoveComponent(this);
        }

        public void Explode()
        {
            this.Destroy();
            this.Screen.AddComponent(new Explosion(this.Game, this.Screen, Vector2.Add(this.Position, new Vector2(-4, -2))));
        }

        public override void Update(GameTime gameTime)
        {
            this.Position = Vector2.Add(this.Position, this.Velocity);

            if (this.Position.X < -100 || this.Position.X > Game1.SCREEN_WIDTH + 100 ||
                this.Position.Y < -100 || this.Position.Y > Game1.SCREEN_HEIGHT + 100)
            {
                this.Destroy();
            }
        }

        //public override void Draw(GameTime gameTime)
        //{

            //(Game as Game1).SpriteBatch.Draw(this.Sprite, this.Position, Color.White);
        //}

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Vector2 Velocity
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }

        //public Texture2D Sprite
        //{
            //get { return this.sprite; }
            //set { this.sprite = value; }
        //}

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
