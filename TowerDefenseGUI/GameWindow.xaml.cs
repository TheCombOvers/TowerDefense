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

namespace TowerDefenseGUI.Model_Files
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        int gamedifficulty;
        int money;
        public GameWindow()
        {
            InitializeComponent();
        }

        //difficulty is based on number: 0-easy, 1-medium, 2-hard
        public void SetMoneyAmount(int difficulty)
        {
            this.gamedifficulty = difficulty;
            
        }
    }
}
