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
    public class CreditsScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Game game;
        SpriteFont titleFont;
        SpriteFont creditFont;
        SpriteBatch spriteBatch;
        public CreditsScreen(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            titleFont = this.game.Content.Load<SpriteFont>("Fonts/titlefont");
            //creditFont = this.game.Content.Load<SpriteFont>("Fonts/CreditFont");
            // TODO: Add your initialization code here
            spriteBatch = new SpriteBatch(this.game.GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            ParallaxBackground.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();            
            spriteBatch.DrawString(titleFont, "YOU WON THE GAME!", new Vector2(300, 100), Color.White);
            spriteBatch.DrawString(titleFont, "Credits:", new Vector2(20, 200), Color.White);
            spriteBatch.DrawString(titleFont, "Fernando Mosqueda - Artist, Sounds", new Vector2(20, 250), Color.White);
            spriteBatch.DrawString(titleFont, "Gerald Halbeisen- Artist, Programmer", new Vector2(20, 300), Color.White);
            spriteBatch.DrawString(titleFont, "Justin Turner - Programmer", new Vector2(20, 350), Color.White);
            spriteBatch.DrawString(titleFont, "Tom Farello- Programmer", new Vector2(20, 400), Color.White);
            spriteBatch.DrawString(titleFont, "Perturbator- Music", new Vector2(20, 450), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
