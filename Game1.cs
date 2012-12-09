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
using GameJamTest.MenuSystem;
using GameJamTest.Util;
using GameJamTest.Screens;

namespace GameJamTest
{
    
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        public const int SCREEN_WIDTH = 1280;
        public const int SCREEN_HEIGHT = 720;

        //Gamestate Constants
        public const int splashScreenID = 0;
        public const int menuScreenID = 1;
        public const int gameScreenID = 2;
        public const int creditScreenID = 3;
        private int splashScreenTime = 0;
        private int currentScreen = splashScreenID;
        
        //Screens
        private MenuScreen menuScreen;
        private SplashScreen splashScreen;
        private GameScreen gameScreen;
        private CreditsScreen creditsScreen;
        private Song menuMusic;
        GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            menuScreen = new MenuScreen(this);
            splashScreen = new SplashScreen(this);
            gameScreen = new GameScreen(this);
            creditsScreen = new CreditsScreen(this);
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            
        }

        public int getScreenWidth()
        {
            return SCREEN_WIDTH;
        }

        public int getScreenHeight()
        {
            return SCREEN_HEIGHT;
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Content.RootDirectory = "GameJamTestContent";

            base.Initialize();
            menuScreen.Initialize();
            creditsScreen.Initialize();
            splashScreen.Initialize();
            GameServices.AddService<GraphicsDevice>(GraphicsDevice);
            GameServices.AddService<ContentManager>(Content);
        }
        public void setCurrentScreen(int currentScreen)
        {
            this.currentScreen = currentScreen;
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            menuMusic = Content.Load<Song>("Music/menumusic");
            AudioManager.playMusic(menuMusic);
            Sprites.LoadContent(this.Content);
            Fonts.LoadContent(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            switch (currentScreen)
            {
                case menuScreenID:
                    menuScreen.Update(gameTime);
                    break;
                case splashScreenID:
                    splashScreenTime += (int)gameTime.ElapsedGameTime.TotalSeconds;
                    if (splashScreenTime > 5)
                    {
                        currentScreen = menuScreenID;
                        break;
                    }
                    splashScreen.Update(gameTime);
                    break;
                case gameScreenID:
                    gameScreen.Update(gameTime);
                    break;
                case creditScreenID:
                    creditsScreen.Update(gameTime);
                    break;
                default:
                    break;
                   

            }
            // TODO: Add your update logic here
            //Test Test
            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ParallaxBackground.Draw(this);

            this.SpriteBatch.Begin();
            switch (currentScreen)
            {
                case menuScreenID:
                    menuScreen.Draw(gameTime);
                    break;
                case splashScreenID:
                    splashScreen.Draw(gameTime);
                    break;
                case gameScreenID:
                    gameScreen.Draw(gameTime);
                    break;
                case creditScreenID:
                    creditsScreen.Draw(gameTime);
                    break;
                default:
                    base.Draw(gameTime);
                    break;
            }
            this.SpriteBatch.End();
        }

        public SpriteBatch SpriteBatch
        {
            get { return this.spriteBatch; }
        }
    }
}
