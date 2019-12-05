using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class HighScore
    {
        public string player;
        public int score;
        Highscores hs = new Highscores();

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
        public void CreateScore(string name, int score)
        {
            string scorestring = name + ": " + score.ToString();
            hs.GetNewScore(scorestring);
        }
    }
}
