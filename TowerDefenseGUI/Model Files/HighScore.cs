// This file contains the HighScore class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    // The HighScore class creates an instance of a score and sends it to the HighScores class
    class HighScore
    {
        public string player;   // variable for player name
        public int score;       // variable for player score
        Highscores hs = new Highscores(); // reference to HighScores

        // Initializes player name and score
        public HighScore()
        {
            Player = player; 
            Score = score;   
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

        // creates a new high score submitted by player 
        // and passes it to HighScores class using the given name and score
        public void CreateScore(string name, int score)
        {
            string scorestring = name + ": " + score.ToString();
            hs.GetNewScore(scorestring);
        }
    }
}
