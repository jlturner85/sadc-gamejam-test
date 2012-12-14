using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJamTest.GameObjects.Leaderboard {
    public class PlayerRecord {
        private string name;
        private int score;
        //private double speed;
        //private int deaths;
        //private int kills;
        //private DateTime date;
        public string Name {
            get { return name; }
            set { name = value; }
        }
        public int Score {
            get { return score; }
            set { score = value; }
        }
        public PlayerRecord(string name, int score) {
            this.Name = name;
            this.Score = score;
        }
    }
}
