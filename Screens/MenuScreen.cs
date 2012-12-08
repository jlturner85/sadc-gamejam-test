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
        SoundEffect menuTickSound;
        private SpriteFont titleFont;
        
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
            menuTickSound = content.Load<SoundEffect>("SoundEffects/fire_laser1");
            ParallaxBackground.Initialize(this.content,(Game1)game);
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
                AudioManager.playSoundEffect(menuTickSound);
                mainGame.setCurrentScreen(2);        
            }

            ParallaxBackground.Update(gameTime);
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            //draw the text on the screen
            spriteBatch.Begin();
            spriteBatch.DrawString(titleFont, "Game Title: The Subtitling", new Vector2(20, 45), Color.White);
            spriteBatch.DrawString(titleFont, "Press space bar to start game", new Vector2(50, 70), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
