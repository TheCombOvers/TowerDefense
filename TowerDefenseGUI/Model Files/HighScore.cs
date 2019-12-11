using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class HighScore
    {
        public string player;   // variable for player name
        public int score;       // variable for player score
        Highscores hs = new Highscores();

        public HighScore()
        {
            Player = player; // string for player name
            Score = score;   // int for player score
        }
        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }

        public string Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
            }
        }
        public void CreateScore(string name, int score)
        {
            // creates a new high score submitted by player and passes it to HighScores class
            string scorestring = name + ": " + score.ToString();
            hs.GetNewScore(scorestring);
        }
    }
}
