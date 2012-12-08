using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameJamTest.Util;
using GameJamTest.Assets;

namespace GameJamTest.MenuSystem
{
    /// <summary>
    /// This is a game component that implements IUpdateable.  stuff stuff
    /// </summary>
    public class MenuScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        ContentManager content;
        Game game;
        Game1 mainGame;
        private SpriteFont titleFont;
        Scrolling scroll1, scroll2, scroll3, scroll4;
        Texture2D staticBackground;
        
        //get the graphics device, used for drawing objects
        private SpriteBatch spriteBatch;
        public MenuScreen(Game game)
            : base(game)
        {
            this.game = game;
            mainGame = (Game1)game;
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            
            //graphicsDevice = GameServices.GetService<GraphicsDevice>();
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            // TODO: Add your initialization code here
            content = game.Content;
            titleFont = content.Load<SpriteFont>("Fonts/TitleFont");
            scroll1 = new Scrolling(content.Load<Texture2D>("Backgrounds/starfield1"), new Rectangle(0, 0, 800, 500));
            scroll2 = new Scrolling(content.Load<Texture2D>("Backgrounds/starfield1"), new Rectangle(800, 0, 800, 500));
            scroll3 = new Scrolling(content.Load<Texture2D>("Backgrounds/starfield2"), new Rectangle(0, 0, 800, 500));
            scroll4 = new Scrolling(content.Load<Texture2D>("Backgrounds/starfield2"), new Rectangle(800, 0, 800, 500));
            staticBackground = content.Load<Texture2D>("Backgrounds/black");
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //if the space bar is pressed, load the gamescreen
            KeyboardState keyState = Keyboard.GetState();
            if(keyState.IsKeyDown(Keys.Space)){

                mainGame.setCurrentScreen(2);        
            }
            if (scroll1.rectangle.X + scroll1.texture.Width <= 0)
            {
                scroll1.rectangle.X = scroll2.rectangle.X + scroll2.texture.Width;
            }
            if (scroll2.rectangle.X + scroll2.texture.Width <= 0)
            {
                scroll2.rectangle.X = scroll1.rectangle.X + scroll1.texture.Width;
            }
            if (scroll3.rectangle.X + scroll3.texture.Width <= 0)
            {
                scroll3.rectangle.X = scroll4.rectangle.X + scroll4.texture.Width;
            }
            if (scroll4.rectangle.X + scroll4.texture.Width <= 0)
            {
                scroll4.rectangle.X = scroll3.rectangle.X + scroll3.texture.Width;
            }
            scroll1.Update(3);
            scroll2.Update(3);
            scroll3.Update(1);
            scroll4.Update(1);
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            //draw the black background layer
            spriteBatch.Begin();
            spriteBatch.Draw(staticBackground, new Rectangle(0, 0, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight), Color.White);
            spriteBatch.End();

            //draw the star scrolling backgrounds
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            scroll3.Draw(spriteBatch);
            scroll4.Draw(spriteBatch);
            scroll1.Draw(spriteBatch);
            scroll2.Draw(spriteBatch);
            spriteBatch.End();

            //draw the text on the screen
            spriteBatch.Begin();
            spriteBatch.DrawString(titleFont, "Game Title: The Subtitling", new Vector2(20, 45), Color.White);
            spriteBatch.DrawString(titleFont, "Press space bar to start game", new Vector2(50, 70), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
