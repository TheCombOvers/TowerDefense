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
            InitializeComponent();
            List<string> highscorestring = Highscores.Load("..\\..\\Resources\\SavedScores.txt");
            for (int i = 0; i < highscorestring.Count; ++i)
            {
                scorebox.Items.Add(highscorestring[i]);
            }
            List<string> scores = Highscores.PassScores();
            for (int i = 0; i < scores.Count; ++i)
            {
                        scorebox.Items.Add(scores[i]);
            }
        }
    }
}
