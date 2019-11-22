using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Game game;
        System.Timers.Timer gameTimer;
        System.Timers.Timer nextWaveTimer; // for auto starting next wave

        public GameWindow()
        {
            InitializeComponent();
            game = new Game(0);
            gameTimer = new System.Timers.Timer(16.666666667);
            //add update gui event
            //add update model events
            gameTimer.Start();
        }

        public int SnapToGridX(double x)
        {
            int tempx = (int)x % 50;
            int newx = (tempx * 50) + 25;
            return newx;
        }

        public int SnapToGridY(double y)
        {
            int tempy = (int)y % 50;
            int newy = (tempy * 50) + 25;
            return newy;
        }

        private void btnNextWave_Click(object sender, RoutedEventArgs e)
        {
            game.NextWave();
        }

        //Creates a new bitmap everytime the buy button is clicked
        //and loads the machine gun place image into it
        //then it takes the Current cursor and changes it with the 
        //machine gun image
        private void btnTurretBuy_Click(object sender, RoutedEventArgs e)
        {
            Bitmap bmp = new Bitmap(Properties.Resources.turret_tower);
            System.Drawing.Point p = System.Windows.Forms.Cursor.Position;
            
            
            
            //bool place = true;
            //Task.Run(() =>
            //{
            //    while (place == true)
            //    {
            //        Dispatcher.Invoke(() =>
            //        {
            //            System.Windows.Point pointtoWindow = System.Windows.Input.Mouse.GetPosition(this);
            //            System.Windows.Point pointtoScreen = PointToScreen(pointtoWindow);
            //            double positionX = pointtoScreen.X;
            //            double positionY = pointtoScreen.Y;
            //            int newX = SnapToGridX(positionX);
            //            int newY = SnapToGridY(positionY);
            //            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(newX, newY);
            //        });
            //    }
            //});
        }
    }
}
