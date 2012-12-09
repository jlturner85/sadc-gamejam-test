using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJamTest.Assets;
using GameJamTest.Screens;

namespace GameJamTest.GameObjects
{
    public class ScoreDisplay : GameJamComponent
    {
        long points;
        int timer;
        float size;

        public ScoreDisplay(Game game, GameScreen screen, Vector2 position, long points)
            : this(game, screen, position, points, 1f / 3f)
        {
        }

        public ScoreDisplay(Game game, GameScreen screen, Vector2 position, long points, float size)
            : base(game, screen, position)
        {
            this.points = points;
            this.size = size;
            this.timer = (int)(180 * size);
            this.Layer = Layer.FRONT;
        }

        public override void Update(GameTime gameTime)
        {
            this.timer--;

            this.Velocity = new Vector2(0, -0.005f * timer / size);

            base.Update(gameTime);

            if (this.timer < 0)
            {
                this.Destroy();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            (this.Game as Game1).SpriteBatch.DrawString(Fonts.TitleFont, points.ToString(), this.Position, Color.White, 0f, new Vector2(0, 0), this.size, SpriteEffects.None, 0f);
        }
    }
}
