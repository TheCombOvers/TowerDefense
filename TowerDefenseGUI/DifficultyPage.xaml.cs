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
        public bool Cheat = true;
        public int Difficulty = 1;
        public SoundHandler soundHandler;

        public DifficultyPage(SoundHandler sentSoundHandler)
        {
            soundHandler = sentSoundHandler;
            InitializeComponent();
        }
        private void BtnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            Window.GetWindow(this).Close();
        }

        private void BtnMainMenu_Hover(object sender, RoutedEventArgs e)
        {
            btnRect.Fill = Brushes.LightSkyBlue;
        }

        private void BtnMainMenu_UnHover(object sender, RoutedEventArgs e)
        {
            btnRect.Fill = Brushes.LightGray;
        }

        private void CheatMode_Click(object sender, RoutedEventArgs e)
        {
            if (CheatMode.Content.ToString() == "Cheat Mode: On")
            {
                CheatMode.Content = "Cheat Mode: Off";
                CheatMode.Foreground = Brushes.DarkRed;
                Cheat = false;
            }
            else
            {
                CheatMode.Content = "Cheat Mode: On";
                CheatMode.Foreground = Brushes.Red;
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
                    Difficulty = 1;
                    break;
                case 2:
                    DifficultyDisplay.Text = "Medium";
                    DifficultyDisplay.Foreground = Brushes.Orange;
                    Difficulty = 2;
                    break;
                case 3:
                    DifficultyDisplay.Text = "Hard";
                    DifficultyDisplay.Foreground = Brushes.Red;
                    Difficulty = 3;
                    break;
            }
        }
        private void BtnSelectMapEasy_Click(object sender, RoutedEventArgs e)
        {
            var gameWindow = new GameWindow(Cheat, false, Difficulty, soundHandler); //put difficulty variable where the zero is 
            gameWindow.Show();
            Window hostWindow = Window.GetWindow(this);
            hostWindow.Close();
        }

        private void BtnSelectMapMedium_Click(object sender, RoutedEventArgs e)
        {
            var gameWindow = new GameWindow(Cheat, false, Difficulty, soundHandler); //put difficulty variable where the zero is 
            gameWindow.Show();
            Window hostWindow = Window.GetWindow(this);
            hostWindow.Close();
        }

        private void BtnSelectMapHard_Click(object sender, RoutedEventArgs e)
        {
            var gameWindow = new GameWindow(Cheat, false, Difficulty, soundHandler); //put difficulty variable where the zero is 
            gameWindow.Show();
            Window hostWindow = Window.GetWindow(this);
            hostWindow.Close();
        }
    }
}
