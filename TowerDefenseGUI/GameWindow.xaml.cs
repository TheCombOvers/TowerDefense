using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TowerDefenseGUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Game game;
        DispatcherTimer gameTimer;
        Timer nextWaveTimer; // for auto starting next wave

        public GameWindow()
        {
            InitializeComponent();
            game = new Game(0);
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 16);
            //add update model events
            gameTimer.Tick += game.UpdateModel;
            gameTimer.Start();
        }

        public int SnapToGridX(int x)
        {
            int tempx = x % 50;
            int newx = (tempx * 50) + 25;
            return newx;
        }
        public void UpdateGame(object sender, object e)
        {
            // access the game properties
            // draw objects based off the properties
            foreach(Enemy en in game.currentEnemies)
            {
                Image i = en.image;
            }
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
        public void Pause()
        {
            gameTimer.Stop();
        }
    }
}
