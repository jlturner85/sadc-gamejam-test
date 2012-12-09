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

        public ScoreDisplay(Game game, GameScreen screen, Vector2 position, long points)
            : base(game, screen, position)
        {
            this.points = points;
            this.timer = 60;
            this.Layer = Layer.FRONT;
        }

        public override void Update(GameTime gameTime)
        {
            this.timer--;

            this.Velocity = new Vector2(0, -0.02f * timer);

            base.Update(gameTime);

            if (this.timer < 0)
            {
                this.Destroy();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            (this.Game as Game1).SpriteBatch.DrawString(Fonts.TitleFont, points.ToString(), this.Position, Color.White, 0f, new Vector2(0, 0), 0.34f, SpriteEffects.None, 0f);
        }
    }
}
