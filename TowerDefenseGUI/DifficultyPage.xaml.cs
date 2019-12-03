using System.Windows;
using System.Windows.Controls;

namespace TowerDefenseGUI
{
    /// <summary>
    /// Interaction logic for DifficultyPage.xaml
    /// </summary>
    public partial class DifficultyPage : Page
    {
        public bool Cheat;

        public DifficultyPage(bool cheat = false)
        {
            InitializeComponent();
            Cheat = cheat;
        }
        private void BtnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            Window.GetWindow(this).Close();
        }

        private void BtnSelectMapEasy_Click(object sender, RoutedEventArgs e)
        {
            var gameWindow = new GameWindow(Cheat);
            gameWindow.Show();
            Window hostWindow = Window.GetWindow(this);
            hostWindow.Close();
        }
    }
}
