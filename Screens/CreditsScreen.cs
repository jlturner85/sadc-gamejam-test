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
using GameJamTest.Assets;

namespace GameJamTest.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class CreditsScreen : Screen
    {
        public static int screenNumber = 3;
        SpriteFont titleFont;
        Texture2D happyFamily;
        SpriteBatch spriteBatch;
        public CreditsScreen(Game1 game)
            : base(game)
        {
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            titleFont = this.Game.Content.Load<SpriteFont>("Fonts/titlefont");
            happyFamily = this.Game.Content.Load<Texture2D>("Images/happy");
            // TODO: Add your initialization code here
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            //if either enter of space is pressed, return to the main menu
            if (keyState.IsKeyDown(Keys.Escape))
            {
                Show(false);
                Game.ScreenManager.MenuScreen.Show(true);
            }
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(titleFont, "Thanks for a playing!", new Vector2(300, 100), Color.White);
            spriteBatch.DrawString(titleFont, "Credits:", new Vector2(20, 200), Color.White);
            spriteBatch.DrawString(titleFont, "Fernando Mosqueda - Artist, Sounds", new Vector2(20, 250), Color.White);
            spriteBatch.DrawString(titleFont, "Gerald Halbeisen- Artist, Programmer", new Vector2(20, 300), Color.White);
            spriteBatch.DrawString(titleFont, "Justin Turner - Programmer", new Vector2(20, 350), Color.White);
            spriteBatch.DrawString(titleFont, "Tom Farello- Programmer", new Vector2(20, 400), Color.White);
            spriteBatch.DrawString(titleFont, "Danny Carroll- Game Jam Theme", new Vector2(20, 450), Color.White);
            spriteBatch.DrawString(titleFont, "Music by Perturbator", new Vector2(20, 500), Color.White);
            spriteBatch.DrawString(titleFont, "Created 12/7-12/9", new Vector2(20, 550), Color.Red);
            spriteBatch.Draw(happyFamily, new Rectangle(1000,300,350,400), Color.CornflowerBlue);
            spriteBatch.DrawString(titleFont, "Press Escape to Return to Menu", new Vector2(20, 650), Color.Green);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        
    }
}
