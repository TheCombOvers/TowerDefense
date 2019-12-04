using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TowerDefenseGUI
{
    
    // Page Logic for DifficultyPage

    // For references:
    //  Difficulty  - int, where 1 is easy, 2 is medium, 3 is hard
    //  Cheat       - bool, where true = cheats, and false = no cheats

    public partial class DifficultyPage : Page
    {
        public bool Cheat = false;
        public int Difficulty = 1;

        public DifficultyPage()
        {
            InitializeComponent();
        }
        private void BtnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            Window.GetWindow(this).Close();
        }

        private void CheatMode_Click(object sender, RoutedEventArgs e)
        {
            if (CheatMode.Content.ToString() == "Cheat Mode: On")
            {
                CheatMode.Content = "Cheat Mode: Off";
                Cheat = false;
            }
            else
            {
                CheatMode.Content = "Cheat Mode: On";
                Cheat = true;
            }
        }

        private void DifficultySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Difficulty = Convert.ToInt32(((Slider)sender).Value);
            switch (Difficulty)
            {
                case 1:
                    DifficultyDisplay.Text = "Easy";
                    DifficultyDisplay.Foreground = Brushes.LightGreen;
                    break;
                case 2:
                    DifficultyDisplay.Text = "Medium";
                    DifficultyDisplay.Foreground = Brushes.Orange;
                    break;
                case 3:
                    DifficultyDisplay.Text = "Hard";
                    DifficultyDisplay.Foreground = Brushes.Red;
                    break;
            }
        }

        private void BtnSelectMapEasy_Click(object sender, RoutedEventArgs e)
        {
            var gameWindow = new GameWindow(Cheat, false);
            gameWindow.Show();
            Window hostWindow = Window.GetWindow(this);
            hostWindow.Close();
        }
    }
}
