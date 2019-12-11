using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TowerDefenseGUI
{
    /// <summary>
    /// Interaction logic for HighScoresWindow.xaml
    /// </summary>
    public partial class HighScoresWindow : Window
    {
        public HighScoresWindow()
        {
            List<int> scores = new List<int>();
            InitializeComponent();
            List<string> highscorestring = Highscores.Load("..\\..\\Resources\\SavedScores.txt");
            for (int i = 0; i < highscorestring.Count; ++i)
            {
                // splits names and scores in order to sort
                string[] scorestr = highscorestring[i].Split(':');
                int score = Convert.ToInt32(scorestr[1].Trim());
                scores.Add(score);
            }
            List<int> sorted = GetHighestScore(scores);
            // prints high scores to screen
            for (int i = 0; i < sorted.Count; ++i)
            {
                for (int j = 0; j < highscorestring.Count; ++j)
                {
                    if (highscorestring[j].Contains(sorted[i].ToString()))
                    {
                        scorebox.Items.Add(highscorestring[j]);
                    }
                }
            }
        }


        public List<int> GetHighestScore(List<int> list)
        {
            // sorts scores from highest to lowest
            list.Sort();
            list.Reverse();
            return list;
        }
    }
}
