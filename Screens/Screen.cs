using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJamTest.Screens
{
    public abstract class Screen : DrawableGameComponent
    {
        public new Game1 Game
        {
            get
            {
                return (Game1)base.Game;
            }
        }
        public SpriteBatch SpriteBatch
        {
            get
            {
                return (SpriteBatch)Game.SpriteBatch;
            }
        }

        public List<GameComponent> Components
        {
            get;
            private set;
        }

        public Screen(Game1 game)
            : base(game)
        {
            Components = new List<GameComponent>();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (GameComponent component in Components)
            {
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (GameComponent component in Components)
            {
                DrawableGameComponent drawable = component as DrawableGameComponent;
                if (drawable != null && drawable.Visible)
                {
                    drawable.Draw(gameTime);
                }
            }
        }

        public void Show(bool show)
        {
            Enabled = show;
            Visible = show;

            foreach (GameComponent component in Components)
            {
                component.Enabled = show;

                DrawableGameComponent drawable = component as DrawableGameComponent;
                if (drawable != null)
                {
                    drawable.Visible = show;
                }
            }
        }
    }
}
