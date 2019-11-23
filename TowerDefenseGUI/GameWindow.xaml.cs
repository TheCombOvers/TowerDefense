﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        List<Image> enemies;
        public GameWindow()
        {
            InitializeComponent();
            enemies = new List<Image>();
            game = new Game(0, AddEnemy, RemoveEnemy);
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 16);
            //add update model events
            gameTimer.Tick += UpdateGame;
            gameTimer.Start();

        }
        // main method that updates the entire game... yikes
        public void UpdateGame(object sender, object e)
        {
            // call all methods need to update the game
            //Task.Run(() => {
            //game.UpdateModel();
            //AddEnemy();
            //Dispatcher.Invoke(() =>  UpdateView());
            //});    
            game.UpdateModel();
            UpdateView();
        }
        public int SnapToGridX(int x)
        {
            int tempx = x % 50;
            int newx = (tempx * 50) + 25;
            return newx;
        }
        public void UpdateView()
        {
            // access the game properties
            // draw objects based off the properties
            int counter = 0;
            if (enemies != null)
            {
                foreach (Image en in enemies)
                {
                    en.Margin = new Thickness(game.currentEnemies[counter].posX, game.currentEnemies[counter].posY, 0, 0);
                    ++counter;
                }
            }
        }
        public void AddEnemy(Enemy e)
        {
            enemies.Add(e.image);
            GameWindowCanvas.Children.Add(e.image);
        }
        public void RemoveEnemy(Enemy e)
        {
            enemies.Remove(e.image);
            GameWindowCanvas.Children.Remove(e.image);
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

        //Creates a new bitmap everytime the buy button is clicked
        //and loads the machine gun place image into it
        //then it takes the Current cursor and changes it with the 
        //machine gun image
        private void btnTurretBuy_Click(object sender, RoutedEventArgs e)
        {
            imageturretplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/turret tower place.png"));
            bool loop = true;
            System.Drawing.Point p1 = System.Windows.Forms.Cursor.Position;
            imageturretplace.Margin = new Thickness(p1.X - 10, p1.Y - 10, 0, 0);
            Task.Run(() =>
            {
                while (loop == true)
                {
                    System.Drawing.Point p = System.Windows.Forms.Cursor.Position;
                    Dispatcher.Invoke(() => imageturretplace.Margin = new Thickness(p.X - 52, p.Y - 52, 0, 0));
                }
            });



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

