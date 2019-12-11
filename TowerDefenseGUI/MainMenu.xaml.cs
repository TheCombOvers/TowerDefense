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
using System.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TowerDefenseGUI
{
    public partial class MainMenu : Window
    {
        public bool cheat = true;
        public SoundHandler soundHandler;

        public MainMenu(SoundHandler sentSoundHandler)
        {
            InitializeComponent();
            soundHandler = sentSoundHandler;
            soundHandler.PlayMusic(SoundHandler.MusicType.MainMenu);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            //DifficultyPage diffPage = new DifficultyPage();
            //this.Content = diffPage;

            // Run code to open difficulty selection and map selection window
            // If page closes without finishing, return;
            // else:
            // Start Game with current variables and selections

            // For Alpha, just launch Game Window

            soundHandler.Play(null, "menubutton");
            DifficultyPage diffPage = new DifficultyPage(soundHandler);
            this.Content = diffPage;
        }

        private void HighScoreButton_Click(object sender, RoutedEventArgs e)
        {
            // Bring up the High Score screen
            soundHandler.Play(null, "menubutton");
            var newMenu = new HighScoresWindow();
            newMenu.ShowDialog();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            // Bring up the Help Menu
            soundHandler.Play(null, "menubutton");
            var newMenu = new HelpWindow();
            newMenu.ShowDialog();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            // Bring up the wiki page in a browser
            soundHandler.Play(null, "menubutton");
            System.Diagnostics.Process.Start("https://github.com/TheCombOvers/TowerDefense/wiki");
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            // if we're loading a old save then pass in true for isLoad, else pass false
            soundHandler.Play(null, "menubutton");
            var gameWindow = new GameWindow(cheat, true, 0, soundHandler, 0);
            gameWindow.Show();
            Close();
        }
    }
}