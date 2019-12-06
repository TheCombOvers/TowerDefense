using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TowerDefenseGUI
{
    [Serializable()]
    public class Highscores
    {
        public static List<string> highscoreslist = new List<string>();

        public void GetNewScore(string highscore)
        {
            highscoreslist.Add(highscore);
            Save("..\\..\\Resources\\SavedScores.txt");
        }

        public static List<string> PassScores()
        {
            return highscoreslist;
        }

        public static List<string> Load(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                List<string> highscorelist = new List<string>();
                string begin = reader.ReadLine();
                if (begin == "Start")
                {
                    string scorestring = reader.ReadLine();
                    while (scorestring != "END")
                    {
                        highscorelist.Add(scorestring);
                        scorestring = reader.ReadLine();
                    }
                }
                return highscorelist;
            }
        }

        public void Save(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("Start");
                for (int i = 0; i < highscoreslist.Count; i++)
                {
                    string highscoresstring = highscoreslist[i];
                    writer.WriteLine(highscoresstring);
                }
                writer.WriteLine("END");
            }

        }

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("Player", Player);
        //    info.AddValue("Score", Score);
        //}

        //public Highscores(SerializationInfo info, StreamingContext context)
        //{
        //    Player = (string)info.GetValue("Player", typeof(string));
        //    Score = (int)info.GetValue("Score", typeof(int));
        //}
    }
}
