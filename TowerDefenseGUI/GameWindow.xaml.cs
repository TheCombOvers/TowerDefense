using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
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
        Game game;
        Timer gameTimer;

        public GameWindow()
        {
            InitializeComponent();
            game = new Game();
            gameTimer = new Timer(16.666666667);
            //add update gui event
            //add update model events
            gameTimer.Start();
        }

        //difficulty is based on number: 0-easy, 1-medium, 2-hard
        public void SetMoneyAmount(int difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    game.money = 200;
                    game.waveTotal = 10;
                    break;
                case 1:
                    game.money = 150;
                    game.waveTotal = 20;
                    break;
                case 2:
                    game.money = 100;
                    game.waveTotal = 30;
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

        private void btnNextWave_Click(object sender, RoutedEventArgs e)
        {
            game.NextWave();
        }
    }
}
