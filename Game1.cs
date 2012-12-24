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
using GameJamTest.Util;
using GameJamTest.Screens;
using GameJamTest.GameObjects.Player;

namespace GameJamTest
{
    
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        public static int SCREEN_WIDTH = 1280;
        public static int SCREEN_HEIGHT = 720;
        
        //Gamestate Constants
        private int splashScreenTime = 0;
        
        //Screens
        private Song menuMusic;
        GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public ScreenManager ScreenManager;

        private GameKeyboard keyboard;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;


            ScreenManager = new ScreenManager(this);

            Components.Add(ScreenManager);
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
            MediaPlayer.Volume=0.5f;
            //MediaPlayer.Volume = 0;
            base.Initialize();
            GameServices.AddService<GraphicsDevice>(GraphicsDevice);
            GameServices.AddService<ContentManager>(Content);
            keyboard = new GameKeyboard();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Fonts.LoadContent(Content);
            Sprites.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Keyboard.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            base.Draw(gameTime);
            SpriteBatch.End();
        }

        public SpriteBatch SpriteBatch {
            get { return this.spriteBatch; }
        }

        public GameKeyboard Keyboard {
            get { return keyboard; }
        }
    }
}
