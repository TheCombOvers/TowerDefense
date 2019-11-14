using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TowerDefenseGUI
{
    [Serializable()]
    public class Highscores : ISerializable
    {
        public List<string> highscorelst = new List<string>();
        private int score;
        private string player;

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

        public Highscores()
        {
            Player = player;
            Score = score;
        }


        public void AddHighscore(string player, int score)
        {
            highscorelst.Add(player + ": " + score + "\n");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Player", Player);
            info.AddValue("Score", Score);
        }

        public Highscores(SerializationInfo info, StreamingContext context)
        {
            Player = (string)info.GetValue("Player", typeof(string));
            Score = (int)info.GetValue("Score", typeof(int));
        }
    }
}
