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
        public static int screenNumber = 1;
        ContentManager content;
        Game game;
        Game1 mainGame;
        SoundEffect menuTickSound;
        Animation texasAnimation;
        Animation shipAnimationFlying;
        private SpriteFont titleFont;
        private Texture2D texasSymbol;
        private Song gameMusic;
        public int selectedScreen;
        private int selectedPosition = 550;
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
            selectedScreen = 4;//game screen
            int width = 32;
            int height = 16;
            gameMusic= game.Content.Load<Song>("Music/menumusic");
            shipAnimationFlying = new Animation(this.Game.Content, "Sprites/playerShip", width, height, 2, 15);
            texasAnimation = new Animation(this.game.Content,"Sprites/Texas_spriteSheet", 300, 300, 2, 15);
            //graphicsDevice = GameServices.GetService<GraphicsDevice>();
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            // TODO: Add your initialization code here
            content = game.Content;
            titleFont = content.Load<SpriteFont>("Fonts/titlefont");
            menuTickSound = content.Load<SoundEffect>("SoundEffects/fire_laser1");
            //texasSymbol = content.Load<Texture2D>("Sprites/Texas_spriteSheet");
            texasAnimation.EnableRepeating();
            shipAnimationFlying.EnableRepeating();
            ParallaxBackground.Initialize(this.content);
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

            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
            {
                if(selectedScreen == 3){
                    selectedScreen = 4;
                    selectedPosition = 550;
                }
            }

            if(keyState.IsKeyDown(Keys.Down)||keyState.IsKeyDown(Keys.S))
            {
                if(selectedScreen ==4){
                    selectedScreen = 3;
                    selectedPosition = 600;
                }
            }
            if (keyState.IsKeyDown(Keys.Space))
            {
                if (selectedScreen == 4)
                {
                    AudioManager.stopMusic();
                    AudioManager.playMusic(gameMusic);
                }
                (game as Game1).setCurrentScreen(selectedScreen); 
                      
            }

            ParallaxBackground.Update(gameTime);
            shipAnimationFlying.Update(gameTime);
            texasAnimation.Update(gameTime);
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            
            //draw the text on the screen
            spriteBatch.Begin();
            //spriteBatch.Draw(texasSymbol, new Rectangle(410, 180, 400, 400), Color.White);
            texasAnimation.Draw(spriteBatch, new Vector2(410,180));
            shipAnimationFlying.Draw((this.Game as Game1).SpriteBatch, new Vector2(350, selectedPosition), 0f, 1.5f);
            spriteBatch.DrawString(titleFont, "Civil War: 2015", new Vector2(380, 100), Color.CornflowerBlue);
            spriteBatch.DrawString(titleFont, "Start Game", new Vector2(450, 550), Color.White);
            spriteBatch.DrawString(titleFont, "Credits", new Vector2(475, 600), Color.White);
            spriteBatch.DrawString(titleFont, "SADC Game Jam 2012", new Vector2(330, 675), Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
