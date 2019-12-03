using System.Windows;
using System.Windows.Controls;

namespace TowerDefenseGUI
{
    /// <summary>
    /// Interaction logic for DifficultyPage.xaml
    /// </summary>
    public partial class DifficultyPage : Page
    {
        public bool Cheat = false;

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

        private void BtnSelectMapEasy_Click(object sender, RoutedEventArgs e)
        {
            var gameWindow = new GameWindow(Cheat, false);
            gameWindow.Show();
            Window hostWindow = Window.GetWindow(this);
            hostWindow.Close();
        }
    }
}
