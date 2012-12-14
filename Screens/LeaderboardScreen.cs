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
using GameJamTest.GameObjects.Leaderboard;


namespace GameJamTest.Screens {
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class LeaderboardScreen : Microsoft.Xna.Framework.DrawableGameComponent {
        public static int screenNumber = 5;
        Game game;
        private SpriteBatch spriteBatch;
        private SpriteFont titleFont;

        //Connection dataConnection;
        

        public LeaderboardScreen(Game game)
            : base(game) {
            // TODO: Construct any child components here
                this.game = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize() {
            // TODO: Add your initialization code here
            titleFont = this.game.Content.Load<SpriteFont>("Fonts/titlefont");
            spriteBatch = spriteBatch = new SpriteBatch(this.game.GraphicsDevice);

            leaderboard = new List<PlayerRecord>();
            leaderboard.Add(new PlayerRecord("Joe", 9001));
            leaderboard.Add(new PlayerRecord("Sally", 4567));
            leaderboard.Add(new PlayerRecord("Bob", 1234));
            leaderboard.Add(new PlayerRecord("Dick", 1));

            //leaderboard = MysqlDatabaseUtil.connection.query(top10);

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime) {

            // TODO: Add your update code here
            if ((this.game as Game1).Keyboard.Back.IsPressed()) { // if exit is pressed
                (this.game as Game1).setCurrentScreen(1); // go to menu screen
            }
            //database.query(scoreQuery)
            ParallaxBackground.Update(gameTime);
            base.Update(gameTime);
        }

        public int leaderboardXAlign = 50;
        public List<PlayerRecord> leaderboard;
        
        public override void Draw(GameTime gameTime) {

            spriteBatch.Begin();
            spriteBatch.DrawString(titleFont, "Leaderboards", new Vector2(450, 50), Color.White);
            //spriteBatch.DrawString(titleFont, "Rank |    Name    |  Score  ", new Vector2(450, 100), Color.White);
            int i = 1;
            int y = 130;
            foreach(PlayerRecord playerRecord in leaderboard) {
                spriteBatch.DrawString(titleFont, i + ".   " + playerRecord.Name + " " + playerRecord.Score, new Vector2(leaderboardXAlign, y), Color.White);
                y += 50;
                i++;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
