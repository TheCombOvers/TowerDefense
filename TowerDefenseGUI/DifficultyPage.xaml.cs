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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void BtnSelectMapEasy_Click(object sender, RoutedEventArgs e)
        {
            var gameWindow = new GameWindow(Cheat);
            gameWindow.Show();
            Window hostWindow = Window.GetWindow(this);
            hostWindow.Close();
        }
    }
}
