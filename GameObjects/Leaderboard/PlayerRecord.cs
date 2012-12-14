using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJamTest.GameObjects.Leaderboard {
    public class PlayerRecord {
        private String name;
        private int score;
        //private int deaths;
        //private int kills;
        //private DateTime date;
        public String Name {
            get { return name; }
            set { name = value; }
        }
        public int Score {
            get { return score; }
            set { score = value; }
        }
        public PlayerRecord(String name, int score) {
            this.Name = name;
            this.Score = score;
        }
    }
}
