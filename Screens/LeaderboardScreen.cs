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
using System.IO;


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

        /**
         * returns the lowest score on the leaderboard
         */
        public static int LowestScore() {
            return leaderboard[leaderboard.Count - 1].Score;
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
            //leaderboard = MysqlDatabaseUtil.connection.query(top10);
            loadScoresFromFile();
            base.Initialize();
        }

        public void writeScoresToFile(string name, int score) {
            // search for new index
            // insert/write at index
        }
        //load scores method using local text file
        public void loadScoresFromFile() {

            if(!File.Exists("HIGHSCORES.TXT")) {
                StreamWriter outfile = new StreamWriter("HIGHSCORES.TXT", true);
                outfile.WriteLine("SIR 1000000");
                outfile.WriteLine("TOM 9001");
                outfile.WriteLine("JLT 19");
                outfile.WriteLine("FTR 17");
                outfile.Flush();
                outfile.Close();
            }

            string[] lines = File.ReadAllLines("HIGHSCORES.txt");
            PlayerRecord playerRecord;
            foreach(string line in lines) {
                playerRecord = new PlayerRecord(line.Split(' ')[0], Convert.ToInt32(line.Split(' ')[1]));
                leaderboard.Add(playerRecord);
            }
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

        public int leaderboardXAlign = 360;
        public static List<PlayerRecord> leaderboard;
        
        public override void Draw(GameTime gameTime) {

            spriteBatch.Begin();
            spriteBatch.DrawString(titleFont, "HIGH SCORES", new Vector2(450, 50), Color.White);
            spriteBatch.DrawString(titleFont, "RANK  NAME   SCORE", new Vector2(leaderboardXAlign, 100), Color.CornflowerBlue);

            int i = 1;
            int y = 130;
            String rank; // rank (space then rank if one digit) space
            String score; //
            String name; // two spaces, three initials
             
            foreach(PlayerRecord playerRecord in leaderboard) {
                score = String.Format("{0,7}", playerRecord.Score.ToString());
                name = String.Format("  {0,3} ", playerRecord.Name);    
                if (i == 1) {
                    //post is 'st'
                    rank = String.Format("{0,2}ST ", i.ToString());
                    spriteBatch.DrawString(titleFont, rank + name + score, new Vector2(leaderboardXAlign, y), Color.Salmon);
                } else if (i == 2) {
                    //post is 'nd'
                    rank = String.Format("{0,2}ND ", i.ToString());
                    spriteBatch.DrawString(titleFont, rank + name + score, new Vector2(leaderboardXAlign, y), Color.Salmon);
                } else if (i == 3) {
                    //post is 'rd'
                    rank = String.Format("{0,2}RD ", i.ToString());
                    spriteBatch.DrawString(titleFont, rank + name + score, new Vector2(leaderboardXAlign, y), Color.Salmon);
                } else {
                    //post is 'th'
                    rank = String.Format("{0,2}TH ", i.ToString());
                    spriteBatch.DrawString(titleFont, rank + name + score, new Vector2(leaderboardXAlign, y), Color.Yellow);
                }
                y += 35;
                i++;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
