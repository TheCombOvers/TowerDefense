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
        public static List<string> highscoreslist = new List<string>();   // list of all high scores

        public void GetNewScore(string highscore)
        {
            // adds new high score to the list of high scores
            Save("..\\..\\Resources\\SavedScores.txt", highscore);
        }

        public static List<string> Load(string filename)
        {
            // loads high scores from file
            using (StreamReader reader = new StreamReader(filename))
            {
                List<string> highscorelist = new List<string>();
                string begin = reader.ReadLine();
                if (begin == "Start")
                {
                    // unload scores in file to the window class
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

        public void Save(string filename, string highscore)
        {
            // saves high scores to file
            List<string> line = new List<string>();
            using (StreamReader reader = new StreamReader(filename))
            {
                // reads file to see if something is already there
                string start = reader.ReadLine();
                if (start == "Start")
                {
                    string readline = reader.ReadLine();
                    while (readline != "END")
                    {
                        // if so, add all scores to a list
                        line.Add(readline);
                        readline = reader.ReadLine();
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter(filename))
            {
                line.Add(highscore);  // add new score to list
                writer.WriteLine("Start");
                writer.Flush();
                for (int i = 0; i < line.Count; ++i)
                {
                    // unload list into file
                    writer.WriteLine(line[i]);
                    writer.Flush();
                }
                writer.WriteLine("END");
                writer.Flush();
            }
        }
    }
}
