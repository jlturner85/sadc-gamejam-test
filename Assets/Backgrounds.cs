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
        private static Scrolling galaxy1, planet1, planet2, planet3;
        
        public static float arsenalSpeed;
        
        private static int height;
        private static int width;
        private static Random random;
        public static void Initialize(ContentManager content)
        {
            
            height = Game1.SCREEN_HEIGHT;
            width = Game1.SCREEN_WIDTH;
            random = new Random();
            
            scroll1 = new Scrolling(content.Load<Texture2D>("Backgrounds/starfield1"), new Rectangle(0, 0, width, height));
            scroll2 = new Scrolling(content.Load<Texture2D>("Backgrounds/starfield1"), new Rectangle(width, 0, width, height));
            scroll3 = new Scrolling(content.Load<Texture2D>("Backgrounds/starfield2"), new Rectangle(0, 0, width, height));
            scroll4 = new Scrolling(content.Load<Texture2D>("Backgrounds/starfield2"), new Rectangle(width, 0, width, height));
            galaxy1 = new Scrolling(content.Load<Texture2D>("Sprites/galaxy"), new Rectangle(1200,500,80,80));
            planet1 = new Scrolling(content.Load<Texture2D>("Sprites/planet"), new Rectangle(1600, 200, 100, 100));
            planet2 = new Scrolling(content.Load<Texture2D>("Sprites/mars"), new Rectangle(2800, 500, 100, 100));
            planet3 = new Scrolling(content.Load<Texture2D>("Sprites/saturn"), new Rectangle(4000, 200, 150, 150));
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
            if (galaxy1.rectangle.X + width <= 0)
            {
                galaxy1.rectangle.X = 1400;
                galaxy1.rectangle.Y = random.Next(80, Game1.SCREEN_HEIGHT-80);
            }

            if (planet1.rectangle.X + width <= 0)
            {
                planet1.rectangle.X = 3000;
                planet1.rectangle.Y = random.Next(80, Game1.SCREEN_HEIGHT - 80);
            }

            if (planet2.rectangle.X + width <= 0)
            {
                planet2.rectangle.X = 4000;
                planet2.rectangle.Y = random.Next(80, Game1.SCREEN_HEIGHT - 80);
            }

            if (planet3.rectangle.X + width <= 0)
            {
                planet3.rectangle.X = 4000;
                planet3.rectangle.Y = random.Next(80, Game1.SCREEN_HEIGHT - 80);
            }
            scroll1.Update(gameSpeed * 0.3f);
            scroll2.Update(gameSpeed * 0.3f);
            scroll3.Update(gameSpeed * 0.1f);
            scroll4.Update(gameSpeed * 0.1f);
            galaxy1.Update(gameSpeed * 0.05f);
            planet1.Update(gameSpeed * 0.2f);
            planet2.Update(gameSpeed * 0.2f);
            planet3.Update(gameSpeed * 0.2f);
            
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
            galaxy1.Draw(spriteBatch);
            spriteBatch.End();

            

            spriteBatch.Begin();
            
            planet1.Draw(spriteBatch);
            planet2.Draw(spriteBatch);
            planet3.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
