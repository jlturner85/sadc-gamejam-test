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
    public class StoryScreen : Screen
    {
        SpriteFont titleFont;
        
        SpriteBatch spriteBatch;
        public StoryScreen(Game1 game)
            : base(game)
        {
            // TODO: Construct any child components here
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            titleFont = this.Game.Content.Load<SpriteFont>("Fonts/titlefont");
            
            // TODO: Add your initialization code here
            spriteBatch = spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //if either enter of space is pressed, return to the main menu
            if (this.Game.Keyboard.Fire.IsPressed()) {
                Show(false);
                Game.ScreenManager.GameScreen.Show(true);
            }
        }


        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            spriteBatch.DrawString(titleFont, "In the year 2015, war was beginning. The", new Vector2(20, 50), Color.White);
            spriteBatch.DrawString(titleFont, "United States of the America has been", new Vector2(20, 100), Color.White);
            spriteBatch.DrawString(titleFont, "severed. Land of hope, Texas, sends elite", new Vector2(20, 150), Color.White);
            spriteBatch.DrawString(titleFont, "defenders of Space Station San Antonio,", new Vector2(20, 200), Color.White);
            spriteBatch.DrawString(titleFont, "the SADC, have taken to space to launch", new Vector2(20, 250), Color.White);
            spriteBatch.DrawString(titleFont, "a final attack against those who would", new Vector2(20, 300), Color.White);
            spriteBatch.DrawString(titleFont, "oppose peace. The Arsenal base has neared", new Vector2(20, 350), Color.White);
            spriteBatch.DrawString(titleFont, "the final target, and the SADC defenders", new Vector2(20, 400), Color.White);
            spriteBatch.DrawString(titleFont, "have been dispatched. The zombie army of", new Vector2(20, 450), Color.White);
            spriteBatch.DrawString(titleFont, "our enemy awaits... are you a strong", new Vector2(20, 500), Color.White);
            spriteBatch.DrawString(titleFont, "enough to bring successful victory?", new Vector2(20, 550), Color.White); 
            spriteBatch.DrawString(titleFont, "Press SPACE to continue...", new Vector2(20, 650), Color.CornflowerBlue);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
