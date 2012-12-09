using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace GameJamTest.Assets
{
    class Backgrounds
    {
        //backgrounds
        public Texture2D texture;
        public Rectangle rectangle;

        protected float fractionalPart;
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, new Color(255,255,255,255));
        }
    }

    class Scrolling : Backgrounds
    {
        public Scrolling(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

        public void Update(float speed)
        {
            float total = speed + this.fractionalPart;
            int integralPart = (int)total;
            rectangle.X -= integralPart;
            this.fractionalPart = total - integralPart;
        }
    }

    public class ParallaxBackground
    {
        private static Scrolling scroll1, scroll2, scroll3, scroll4;
        private static Texture2D staticBackground;
        private static int height;
        private static int width;
        public static void Initialize(ContentManager content)
        {
            height = Game1.SCREEN_HEIGHT;
            width = Game1.SCREEN_WIDTH;
            scroll1 = new Scrolling(content.Load<Texture2D>("Backgrounds/starfield1"), new Rectangle(0, 0, width, height));
            scroll2 = new Scrolling(content.Load<Texture2D>("Backgrounds/starfield1"), new Rectangle(width, 0, width, height));
            scroll3 = new Scrolling(content.Load<Texture2D>("Backgrounds/starfield2"), new Rectangle(0, 0, width, height));
            scroll4 = new Scrolling(content.Load<Texture2D>("Backgrounds/starfield2"), new Rectangle(width, 0, width, height));
            staticBackground = content.Load<Texture2D>("Backgrounds/black");
        }

        public static void Update(GameTime gameTime)
        {
            Update(gameTime, 10);
        }

        public static void Update(GameTime gameTime, int gameSpeed)
        {
            if (scroll1.rectangle.X + width <= 0)
            {
                scroll1.rectangle.X = scroll2.rectangle.X + width;
            }
            if (scroll2.rectangle.X + width<= 0)
            {
                scroll2.rectangle.X = scroll1.rectangle.X + width;
            }
            if (scroll3.rectangle.X + width <= 0)
            {
                scroll3.rectangle.X = scroll4.rectangle.X + width;
            }
            if (scroll4.rectangle.X + width <= 0)
            {
                scroll4.rectangle.X = scroll3.rectangle.X + width;
            }
            scroll1.Update(gameSpeed * 0.3f);
            scroll2.Update(gameSpeed * 0.3f);
            scroll3.Update(gameSpeed * 0.1f);
            scroll4.Update(gameSpeed * 0.1f);
        }

        public static void Draw(Game game)
        {
            SpriteBatch spriteBatch = (game as Game1).SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.Draw(staticBackground, new Rectangle(0, 0, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight), Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            scroll3.Draw(spriteBatch);
            scroll4.Draw(spriteBatch);
            scroll1.Draw(spriteBatch);
            scroll2.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
