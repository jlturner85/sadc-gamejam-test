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

namespace GameJamTest.Screens
{
    public class ScreenManager : DrawableGameComponent
    {
        public MenuScreen MenuScreen { get; private set; }
        public SplashScreen SplashScreen { get; private set; }
        public GameScreen GameScreen { get; private set; }
        public CreditsScreen CreditsScreen { get; private set; }
        public StoryScreen StoryScreen { get; private set; }
        public LeaderboardScreen LeaderboardScreen { get; private set; }
        public ParallaxBackground ParallaxBackground { get; private set; }

        public List<Screen> Screens
        {
            get;
            private set;
        }

        public new Game1 Game
        {
            get
            {
                return (Game1)(base.Game);
            }
        }

        public ScreenManager(Game1 game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            MenuScreen = new MenuScreen(Game);
            SplashScreen = new SplashScreen(Game);
            GameScreen = new GameScreen(Game);
            CreditsScreen = new CreditsScreen(Game);
            StoryScreen = new StoryScreen(Game);
            LeaderboardScreen = new LeaderboardScreen(Game);
            ParallaxBackground = new ParallaxBackground(Game);

            MenuScreen.Initialize();
            CreditsScreen.Initialize();
            SplashScreen.Initialize();
            StoryScreen.Initialize();
            LeaderboardScreen.Initialize();
            ParallaxBackground.Initialize();

            Screens = new List<Screen>();

            Screens.Add(ParallaxBackground);
            Screens.Add(MenuScreen);
            Screens.Add(LeaderboardScreen);
            Screens.Add(CreditsScreen);
            Screens.Add(StoryScreen);
            Screens.Add(GameScreen);
            Screens.Add(SplashScreen);

            MenuScreen.Show(false);
            GameScreen.Show(false);
            CreditsScreen.Show(false);
            StoryScreen.Show(false);
            LeaderboardScreen.Show(false);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (Screen screen in Screens)
            {
                if (screen.Enabled)
                {
                    screen.Update(gameTime);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (Screen screen in Screens)
            {
                if (screen.Visible)
                {
                    screen.Draw(gameTime);
                }
            }
        }
    }
}
