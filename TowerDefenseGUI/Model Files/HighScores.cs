// This file contains the HighScores class
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
    // The HighScores class saves and loads a list of a scores
    // and adds new scores to the list.
    public class Highscores
    {
        public static List<string> highscoreslist = new List<string>();   // list of all high scores

        // receives new high score and adds it to the list of high scores
        public void GetNewScore(string highscore)
        {
            Save("..\\..\\Resources\\SavedScores.txt", highscore);
        }

        // loads high scores from given file
        // unload scores in file to the window class
        // returns list of scores
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

        // saves a given high score to the given file
        // reads file to see if something is already there
        // if so, add all scores to a list
        // add new score to list
        // unload list into file
        public void Save(string filename, string highscore)
        {
            List<string> line = new List<string>();
            using (StreamReader reader = new StreamReader(filename))
            {
                string start = reader.ReadLine();
                if (start == "Start")
                {
                    string readline = reader.ReadLine();
                    while (readline != "END")
                    {
                        line.Add(readline);
                        readline = reader.ReadLine();
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter(filename))
            {
                line.Add(highscore);
                writer.WriteLine("Start");
                writer.Flush();
                for (int i = 0; i < line.Count; ++i)
                {
                    writer.WriteLine(line[i]);
                    writer.Flush();
                }
                writer.WriteLine("END");
                writer.Flush();
            }
        }
    }
}
