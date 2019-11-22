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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        int money;
        int waves;
        public GameWindow()
        {
            InitializeComponent();
        }

        //difficulty is based on number: 0-easy, 1-medium, 2-hard
        public void SetMoneyAmount(int difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    money = 200;
                    waves = 10;
                    break;
                case 1:
                    money = 150;
                    waves = 20;
                    break;
                case 2:
                    money = 100;
                    waves = 30;
                    break;
            }
        }

        public int SnapToGridX(int x)
        {
            int tempx = x % 50;
            int newx = (tempx * 50) + 25;
            return newx;
        }

        public int SnapToGridY(int y)
        {
            int tempy = y % 50;
            int newy = (tempy * 50) + 25;
            return newy;
        }
    }
}
