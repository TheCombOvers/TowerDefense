using System;
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
        List<string> eImageSources; // 0:infantry, 1:vehicle, 2:aircraft, 3:ground boss
        bool loop;
        Point mousePos;
        int lives;
        bool machinegun;
        bool tesla;
        bool flak;
        bool laser;
        bool mortar;
        bool stun;
        bool machinegunplace;
        bool flakplace;
        bool mortarplace;
        bool teslaplace;
        bool laserplace;
        bool stunplace;

        public GameWindow(bool cheat, bool isLoad)
        {
            InitializeComponent();
            enemies = new List<Image>();
            eImageSources = new List<string>();
            eImageSources.Add("pack://application:,,,/Resources/Basic Ground Unit.png");
            // add all the image sources-levi
            
            // if we're loading a old save then call loadgame else just make a new game instance
            if (isLoad)
            {
                game = Game.LoadGame("..\\..\\Resources\\SavedGame3.txt", AddEnemy, RemoveEnemy);
            }
            else
            {
                game = new Game(0, cheat, AddEnemy, RemoveEnemy);
            }
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 16);
            //add update model events
            gameTimer.Tick += UpdateGame;
            gameTimer.Start();
            btnBasic.IsEnabled = false;
            machinegun = true;
            flak = true;
            mortar = true;
            txtMoney.Text += game.money;
            Task.Run(() =>
            {
                while (true)
                {
                    Dispatcher.Invoke(() => txtLives.Text = "Lives: " + Game.lives);
                }
            });
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
        // creates a new coordinationg image for the given enemy "e" and and places on the screen and adds it to
        // the list of images of enemies in the view
        public void AddEnemy(Enemy e)
        {
            Image i = new Image();
            i.Source = new BitmapImage(new Uri(eImageSources[e.imageID]));
            i.RenderTransformOrigin = new Point(0.5, 0.5);
            if (e.imageID == 3) // if it's a boss it's bigger! :)
            {
                i.Width = 80;
                i.Height = 80;
            }
            else
            {
                i.Width = 50;
                i.Height = 50;
            }
            e.imageIndex = enemies.Count; // set the index of the enemy so we can use it to remove later
            enemies.Add(i);
            GameWindowCanvas.Children.Add(i);
            
        }
        // removes a specified enemy from the game state and the view
        public void RemoveEnemy(Enemy e)
        {   
            game.currentEnemies.Remove(e);
            Spawner.enemies.Remove(e);  // remove it from the game state
            GameWindowCanvas.Children.Remove(enemies[e.imageIndex]); // remove from the game window canvas
            enemies.RemoveAt(e.imageIndex);     // remove it from the image list in the view
            for (int i = e.imageIndex; i < enemies.Count; ++i)
            {
                Spawner.enemies[i].imageIndex -= 1;
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
        private void btnSaveGame_Click(object sender, RoutedEventArgs e)
        {
            // Code for saving the game state
        }
        private void btnPauseGame_Click(object sender, RoutedEventArgs e)
        {
            Button pressBtn = (Button)sender;
            pressBtn.Content = "Resume";
            pressBtn.Click += btnResumeGame_Click;
            Pause();
        }
        private void btnResumeGame_Click(object sender, RoutedEventArgs e)
        {
            Button pressBtn = (Button)sender;
            pressBtn.Content = "Pause";
            pressBtn.Click += btnPauseGame_Click;
            gameTimer.Start();
        }
        public void Pause()
        {
            gameTimer.Stop();
        }

        //Creates a new bitmap everytime the buy button is clicked
        //and loads the machine gun place image into it
        //then it takes the Current cursor and changes it with the 
        //machine gun image
        private void btnMachineGunTeslaBuy_Click(object sender, RoutedEventArgs e)
        {
            if (machinegun == true)
            {
                if (game.money >= 50)
                {
                    machinegunplace = true;
                    flakplace = false;
                    mortarplace = false;
                    teslaplace = false;
                    laserplace = false;
                    stunplace = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/turret tower place.png"));
                    mousePos = Mouse.GetPosition(GameWindowCanvas);
                    imagetowerplace.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
                    loop = true;
                    Task.Run(() =>
                    {
                        while (loop == true)
                        {
                            Dispatcher.Invoke(() => {
                                mousePos = Mouse.GetPosition(GameWindowCanvas);
                                imagetowerplace.Margin = new Thickness((int)mousePos.X, (int)mousePos.Y, 0, 0);
                                });
                        }
                    });
                }
            }
            else if (tesla == true)
            {
                if (game.money >= 175)
                {
                    machinegunplace = false;
                    flakplace = false;
                    mortarplace = false;
                    teslaplace = true;
                    laserplace = false;
                    stunplace = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tesla tower place.png"));
                    System.Drawing.Point p1 = System.Windows.Forms.Cursor.Position;
                    imagetowerplace.Margin = new Thickness(p1.X * .9, p1.Y * .9, 0, 0);
                    loop = true;

                    Task.Run(() =>
                    {
                        while (loop == true)
                        {
                            int posX = System.Windows.Forms.Cursor.Position.X;
                            int posY = System.Windows.Forms.Cursor.Position.Y;
                            mousePos.X = System.Windows.Forms.Cursor.Position.X;
                            mousePos.Y = System.Windows.Forms.Cursor.Position.Y;
                            Dispatcher.Invoke(() => imagetowerplace.Margin = new Thickness(posX * .9, posY * .9, 0, 0));
                        }
                    });
                }
            }
        }

        private void imagetowerplace_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(GameWindowCanvas);
            if (loop == true)
            {
                if (machinegunplace == true)
                {
                    game.money -= 50;
                    txtMoney.Text = "$" + game.money;
                    loop = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/turret tower.PNG"));
                    double posX = p.X;
                    double posY = p.Y;
                    image.Margin = new Thickness(posX, posY, 0, 0);
                    MachineGun g = MachineGun.MakeMachineGun(posX, posY);
                    g.xPos = Convert.ToInt32(posX);
                    g.xPos = Convert.ToInt32(posY);
                    game.currentTurrets.Add(g);
                    image.Width = 50;
                    image.Height = 50;
                    GameWindowCanvas.Children.Add(image);
                }
                else if (flakplace == true)
                {
                    game.money -= 75;
                    txtMoney.Text = "$" + game.money;
                    loop = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower.PNG"));
                    double posX = mousePos.X;
                    double posY = mousePos.Y;
                    image.Margin = new Thickness(posX * .9, posY * .9, 0, 0);
                    Flak g = Flak.MakeFlak();
                    g.xPos = Convert.ToInt32(posX * .9);
                    g.xPos = Convert.ToInt32(posY * .9);
                    game.currentTurrets.Add(g);
                    image.Width = 50;
                    image.Height = 50;
                    GameWindowCanvas.Children.Add(image);
                }
                else if (mortarplace == true)
                {
                    game.money -= 200;
                    txtMoney.Text = "$" + game.money;
                    loop = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower.PNG"));
                    double posX = mousePos.X;
                    double posY = mousePos.Y;
                    image.Margin = new Thickness(posX * .9, posY * .9, 0, 0);
                    Mortar g = Mortar.MakeMortar();
                    g.xPos = Convert.ToInt32(posX * .9);
                    g.xPos = Convert.ToInt32(posY * .9);
                    game.currentTurrets.Add(g);
                    image.Width = 50;
                    image.Height = 50;
                    GameWindowCanvas.Children.Add(image);
                }
                else if (teslaplace == true)
                {
                    game.money -= 175;
                    txtMoney.Text = "$" + game.money;
                    loop = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tesla tower.PNG"));
                    double posX = mousePos.X;
                    double posY = mousePos.Y;
                    image.Margin = new Thickness(posX * .9, posY * .9, 0, 0);
                    Tesla g = Tesla.MakeTesla();
                    g.xPos = Convert.ToInt32(posX * .9);
                    g.xPos = Convert.ToInt32(posY * .9);
                    game.currentTurrets.Add(g);
                    image.Width = 50;
                    image.Height = 50;
                    GameWindowCanvas.Children.Add(image);
                }
                else if (laserplace == true)
                {
                    game.money -= 125;
                    txtMoney.Text = "$" + game.money;
                    loop = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower.PNG"));
                    double posX = mousePos.X;
                    double posY = mousePos.Y;
                    image.Margin = new Thickness(posX * .9, posY * .9, 0, 0);
                    Laser g = Laser.MakeLaser();
                    g.xPos = Convert.ToInt32(posX * .9);
                    g.xPos = Convert.ToInt32(posY * .9);
                    game.currentTurrets.Add(g);
                    image.Width = 50;
                    image.Height = 50;
                    GameWindowCanvas.Children.Add(image);
                }
                else if (stunplace == true)
                {
                    game.money -= 200;
                    txtMoney.Text = "$" + game.money;
                    loop = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/stun tower.PNG"));
                    double posX = mousePos.X;
                    double posY = mousePos.Y;
                    image.Margin = new Thickness(posX * .9, posY * .9, 0, 0);
                    Stun g = Stun.MakeStun();
                    g.xPos = Convert.ToInt32(posX * .9);
                    g.xPos = Convert.ToInt32(posY * .9);
                    game.currentTurrets.Add(g);
                    image.Width = 50;
                    image.Height = 50;
                    GameWindowCanvas.Children.Add(image);
                }
            }
        }

        private void MapImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (loop == true)
            {
                if (machinegunplace == true)
                {
                    game.money -= 50;
                    txtMoney.Text = "$" + game.money;
                    loop = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/turret tower.PNG"));
                    image.RenderTransformOrigin = new Point(0.5, 0.5);
                    double posX = mousePos.X;
                    double posY = mousePos.Y;
                    image.Margin = new Thickness(posX, posY, 0, 0);
                    MachineGun g = MachineGun.MakeMachineGun(posX, posY);
                    g.xPos = Convert.ToInt32(posX);
                    g.yPos = Convert.ToInt32(posY);
                    game.currentTurrets.Add(g);
                    image.Width = 50;
                    image.Height = 50;
                    GameWindowCanvas.Children.Add(image);
                }
                else if (flakplace == true)
                {
                    game.money -= 75;
                    txtMoney.Text = "$" + game.money;
                    loop = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower.PNG"));
                    double posX = mousePos.X;
                    double posY = mousePos.Y;
                    image.Margin = new Thickness(posX * .9, posY * .9, 0, 0);
                    Flak g = Flak.MakeFlak();
                    g.xPos = Convert.ToInt32(posX * .9);
                    g.xPos = Convert.ToInt32(posY * .9);
                    game.currentTurrets.Add(g);
                    image.Width = 50;
                    image.Height = 50;
                    GameWindowCanvas.Children.Add(image);
                }
                else if (mortarplace == true)
                {
                    game.money -= 200;
                    txtMoney.Text = "$" + game.money;
                    loop = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower.PNG"));
                    double posX = mousePos.X;
                    double posY = mousePos.Y;
                    image.Margin = new Thickness(posX * .9, posY * .9, 0, 0);
                    Mortar g = Mortar.MakeMortar();
                    g.xPos = Convert.ToInt32(posX * .9);
                    g.xPos = Convert.ToInt32(posY * .9);
                    game.currentTurrets.Add(g);
                    image.Width = 50;
                    image.Height = 50;
                    GameWindowCanvas.Children.Add(image);
                }
                else if (teslaplace == true)
                {
                    game.money -= 175;
                    txtMoney.Text = "$" + game.money;
                    loop = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tesla tower.PNG"));
                    double posX = mousePos.X;
                    double posY = mousePos.Y;
                    image.Margin = new Thickness(posX * .9, posY * .9, 0, 0);
                    Tesla g = Tesla.MakeTesla();
                    g.xPos = Convert.ToInt32(posX * .9);
                    g.xPos = Convert.ToInt32(posY * .9);
                    game.currentTurrets.Add(g);
                    image.Width = 50;
                    image.Height = 50;
                    GameWindowCanvas.Children.Add(image);
                }
                else if (laserplace == true)
                {
                    game.money -= 125;
                    txtMoney.Text = "$" + game.money;
                    loop = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower.PNG"));
                    double posX = mousePos.X;
                    double posY = mousePos.Y;
                    image.Margin = new Thickness(posX * .9, posY * .9, 0, 0);
                    Laser g = Laser.MakeLaser();
                    g.xPos = Convert.ToInt32(posX * .9);
                    g.xPos = Convert.ToInt32(posY * .9);
                    game.currentTurrets.Add(g);
                    image.Width = 50;
                    image.Height = 50;
                    GameWindowCanvas.Children.Add(image);
                }
                else if (stunplace == true)
                {
                    game.money -= 200;
                    txtMoney.Text = "$" + game.money;
                    loop = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/stun tower.PNG"));
                    double posX = mousePos.X;
                    double posY = mousePos.Y;
                    image.Margin = new Thickness(posX * .9, posY * .9, 0, 0);
                    Stun g = Stun.MakeStun();
                    g.xPos = Convert.ToInt32(posX * .9);
                    g.xPos = Convert.ToInt32(posY * .9);
                    game.currentTurrets.Add(g);
                    image.Width = 50;
                    image.Height = 50;
                    GameWindowCanvas.Children.Add(image);
                }
            }
        }

        private void btnAdvanced_Click(object sender, RoutedEventArgs e)
        {
            btnBasic.IsEnabled = true;
            btnAdvanced.IsEnabled = false;
            machinegun = false;
            tesla = true;
            MachineGunTeslaImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tesla tower.png"));
            txtMachineGunTeslaName.Text = "Tesla Tower";
            txtMachineGunTeslaType.Text = "Target: Ground";
            txtMachineGunTeslaRange.Text = "Range: 100";
            txtMachineGunTeslaDmg.Text = "Damage: 3/s";
            txtMachineGunTeslaCost.Text = "Cost: $175";
            flak = false;
            laser = true;
            FlakLaserImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower.png"));
            txtFlakLaserName.Text = "Laser Tower";
            txtFlakLaserType.Text = "Target: Ground/Air";
            txtFlakLaserRange.Text = "Range: 175";
            txtFlakLaserDmg.Text = "Damage: 10/s";
            txtFlakLaserCost.Text = "Cost: $125";
            mortar = false;
            stun = true;
            MortarStunImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/stun tower.png"));
            txtMortarStunName.Text = "Stun Tower";
            txtMortarStunType.Text = "Target: Ground/Air";
            txtMortarStunRange.Text = "Range: 200";
            txtMortarStunDmg.Text = "Damage: 15/s";
            txtMortarStunCost.Text = "Cost: $200";
        }

        private void btnBasic_Click(object sender, RoutedEventArgs e)
        {
            btnBasic.IsEnabled = false;
            btnAdvanced.IsEnabled = true;
            machinegun = true;
            tesla = false;
            MachineGunTeslaImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/turret tower.png"));
            txtMachineGunTeslaName.Text = "Machine Gun Tower";
            txtMachineGunTeslaType.Text = "Target: Ground";
            txtMachineGunTeslaRange.Text = "Rane: 125";
            txtMachineGunTeslaDmg.Text = "Damage: 4/s";
            txtMachineGunTeslaCost.Text = "Cost: $50";
            flak = true;
            laser = false;
            FlakLaserImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower.png"));
            txtFlakLaserName.Text = "Flak Tower";
            txtFlakLaserType.Text = "Target: Air";
            txtFlakLaserRange.Text = "Range: 225";
            txtFlakLaserDmg.Text = "Damage: 2/s";
            txtFlakLaserCost.Text = "Cost: $75";
            mortar = true;
            stun = false;
            MortarStunImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower.png"));
            txtMortarStunName.Text = "Mortar Tower";
            txtMortarStunType.Text = "Target: Ground";
            txtMortarStunRange.Text = "Range: 275";
            txtMortarStunDmg.Text = "Damage: 50/5s";
            txtMortarStunCost.Text = "Cost: $150";
        }

        private void btnFlakLaserBuy_Click(object sender, RoutedEventArgs e)
        {
            if (flak == true)
            {
                if (game.money >= 75)
                {
                    machinegunplace = false;
                    flakplace = true;
                    mortarplace = false;
                    teslaplace = false;
                    laserplace = false;
                    stunplace = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower place.png"));
                    System.Drawing.Point p1 = System.Windows.Forms.Cursor.Position;
                    imagetowerplace.Margin = new Thickness(p1.X * .9, p1.Y * .9, 0, 0);
                    loop = true;

                    Task.Run(() =>
                    {
                        while (loop == true)
                        {
                            int posX = System.Windows.Forms.Cursor.Position.X;
                            int posY = System.Windows.Forms.Cursor.Position.Y;
                            mousePos.X = System.Windows.Forms.Cursor.Position.X;
                            mousePos.Y = System.Windows.Forms.Cursor.Position.Y;
                            Dispatcher.Invoke(() => imagetowerplace.Margin = new Thickness(posX * .9, posY * .9, 0, 0));
                        }
                    });
                }
            }
            else if (laser == true)
            {
                if (game.money >= 125)
                {
                    machinegunplace = false;
                    flakplace = false;
                    mortarplace = false;
                    teslaplace = false;
                    laserplace = true;
                    stunplace = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower place.png"));
                    System.Drawing.Point p1 = System.Windows.Forms.Cursor.Position;
                    imagetowerplace.Margin = new Thickness(p1.X * .9, p1.Y * .9, 0, 0);
                    loop = true;

                    Task.Run(() =>
                    {
                        while (loop == true)
                        {
                            int posX = System.Windows.Forms.Cursor.Position.X;
                            int posY = System.Windows.Forms.Cursor.Position.Y;
                            mousePos.X = System.Windows.Forms.Cursor.Position.X;
                            mousePos.Y = System.Windows.Forms.Cursor.Position.Y;
                            Dispatcher.Invoke(() => imagetowerplace.Margin = new Thickness(posX * .9, posY * .9, 0, 0));
                        }
                    });
                }
            }
        }

        private void btnMortarStunBuy_Click(object sender, RoutedEventArgs e)
        {
            if (mortar == true)
            {
                if (game.money >= 150)
                {
                    machinegunplace = false;
                    flakplace = false;
                    mortarplace = true;
                    teslaplace = false;
                    laserplace = false;
                    stunplace = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower place.png"));
                    System.Drawing.Point p1 = System.Windows.Forms.Cursor.Position;
                    imagetowerplace.Margin = new Thickness(p1.X * .9, p1.Y * .9, 0, 0);
                    loop = true;

                    Task.Run(() =>
                    {
                        while (loop == true)
                        {
                            int posX = System.Windows.Forms.Cursor.Position.X;
                            int posY = System.Windows.Forms.Cursor.Position.Y;
                            mousePos.X = System.Windows.Forms.Cursor.Position.X;
                            mousePos.Y = System.Windows.Forms.Cursor.Position.Y;
                            Dispatcher.Invoke(() => imagetowerplace.Margin = new Thickness(posX * .9, posY * .9, 0, 0));
                        }
                    });
                }
            }
            else if (stun == true)
            {
                if (game.money >= 200)
                {
                    machinegunplace = false;
                    flakplace = false;
                    mortarplace = false;
                    teslaplace = false;
                    laserplace = false;
                    stunplace = true;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/stun tower place.png"));
                    System.Drawing.Point p1 = System.Windows.Forms.Cursor.Position;
                    imagetowerplace.Margin = new Thickness(p1.X * .9, p1.Y * .9, 0, 0);
                    loop = true;

                    Task.Run(() =>
                    {
                        while (loop == true)
                        {
                            int posX = System.Windows.Forms.Cursor.Position.X;
                            int posY = System.Windows.Forms.Cursor.Position.Y;
                            mousePos.X = System.Windows.Forms.Cursor.Position.X;
                            mousePos.Y = System.Windows.Forms.Cursor.Position.Y;
                            Dispatcher.Invoke(() => imagetowerplace.Margin = new Thickness(posX * .9, posY * .9, 0, 0));
                        }
                    });
                }
            }
        }

        private void GameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }
    }
}

