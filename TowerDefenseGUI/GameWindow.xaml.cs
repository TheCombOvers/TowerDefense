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
        List<Image> turrets;
        List<string> eImageSources; // 0:infantry, 1:vehicle basic, 2:aircraft basic, 3:ground boss
        // 4:advance ground unit, 5:advanced ground vehicle, 6:aircraft advanced, 7: air boss
        List<string> tImageSources;// 0:MG tower, 1:flak tower, 2:laser tower, 3:mortar, 4:stun, 5:tesla
        bool isPlacing;
        Point mousePos;
        bool basic;
        bool machinegunplace;
        bool flakplace;
        bool mortarplace;
        bool teslaplace;
        bool laserplace;
        bool stunplace;

        public GameWindow(bool cheat, bool isLoad, int diff, SoundHandler soundHandler)
        {
            InitializeComponent();
            turrets = new List<Image>();
            enemies = new List<Image>();
            // do not mess with the order of these addition please :)
            eImageSources = new List<string>();
            eImageSources.Add("pack://application:,,,/Resources/Basic Ground Unit.png");
            eImageSources.Add("pack://application:,,,/Resources/Basic Ground Vehicle.png");
            eImageSources.Add("pack://application:,,,/Resources/Basic Aircraft.png");
            eImageSources.Add("pack://application:,,,/Resources/Ground Boss.png");
            eImageSources.Add("pack://application:,,,/Resources/Advanced Ground Unit.png");
            eImageSources.Add("pack://application:,,,/Resources/Advanced Ground Vehicle.png");
            eImageSources.Add("pack://application:,,,/Resources/Advanced Aircraft.png");
            eImageSources.Add("pack://application:,,,/Resources/Aircraft Boss.png");
            // add all image sources to the turret image sources list
            // again dont mess with the order of these lines
            tImageSources = new List<string>();
            tImageSources.Add("pack://application:,,,/Resources/turret tower.png");
            tImageSources.Add("pack://application:,,,/Resources/flak tower.png");
            tImageSources.Add("pack://application:,,,/Resources/laser tower.png");
            tImageSources.Add("pack://application:,,,/Resources/mortar tower.png");
            tImageSources.Add("pack://application:,,,/Resources/stun tower.png");
            tImageSources.Add("pack://application:,,,/Resources/tesla tower.png");
            // if we're loading a old save then call loadgame else just make a new game instance
            if (isLoad)
            {
                game = Game.LoadGame("..\\..\\Resources\\SavedGame3.txt", AddEnemy, RemoveEnemy);
                if (game.isWaveOver == false)
                {
                    btnNextWave.IsEnabled = false;
                }
                LoadTurretImgs();
            }
            else
            {
                game = new Game(0, cheat, AddEnemy, RemoveEnemy, diff);
              
            }
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 16);
            //add update model events
            gameTimer.Tick += UpdateGame;
            gameTimer.Tick += updateTowerPlace;
            Turret.RotateTurret += RotateTurret;
            Enemy.RotateEnemy += RotateEnemy;
            Turret.PlaySound += soundHandler.Play;
            btnBasic.IsEnabled = false;
            basic = true;
            txtMoney.Text += Game.money;
            txtLives.Text = "Lives: " + Game.lives;
            gameTimer.Start();
        }

        // main method that updates the entire game... yikes
        public void UpdateGame(object sender, object e)
        {
            game.UpdateModel();
            txtMoney.Text = "$" + Game.money;
            txtLives.Text = "Lives: " + Game.lives;
            txtRoundDisplay.Text = "Wave: " + game.currentWave;
            UpdateView();
        }
        public void UpdateView()
        {
            // access the game properties
            // draw objects based off the properties
            txtLives.Text = "Lives: " + Game.lives;
            int counter = 0;
            if (enemies.Count > 0)
            {
                foreach (Image en in enemies)
                {
                    en.Margin = new Thickness(game.currentEnemies[counter].posX, game.currentEnemies[counter].posY, 0, 0);
                    ++counter;
                }
            }
        }

        public void RotateEnemy(object en, int degrees)
        {
            int index = game.currentEnemies.IndexOf(en as Enemy);
            if (game.currentEnemies.Contains(en) && enemies.Count >= index + 1)
            {           
                enemies[index].RenderTransform = new RotateTransform(degrees);
            }
        }

        public void RotateTurret(object tur, int degrees)
        {
            if (game.currentTurrets.Contains(tur))
            {
                int index = game.currentTurrets.IndexOf(tur as Turret);
                turrets[index].RenderTransform = new RotateTransform(degrees);
            }
        }

        // creates a new coordinationg image for the given enemy "e" and and places on the screen and adds it to
        // the list of images of enemies in the view
        public void AddEnemy(Enemy e)
        {
            Image i = new Image();
            i.Source = new BitmapImage(new Uri(eImageSources[e.imageID]));
            i.RenderTransformOrigin = new Point(0.5, 0.5);
            if (e.imageID == 3 || e.imageID == 7) // if it's a boss it's bigger! :)
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
            if (enemies.Count == 0)
            {
                game.isWaveOver = true;
                game.currentWave += 1;
                btnNextWave.IsEnabled = true;
            }
        }

        private void btnNextWave_Click(object sender, RoutedEventArgs e)
        {
            game.NextWave();
            btnNextWave.IsEnabled =  false;
            game.isWaveOver = false;
        }
        private void btnSaveGame_Click(object sender, RoutedEventArgs e)
        {
            game.SaveGame("..\\..\\Resources\\SavedGame3.txt");
            MessageBox.Show("Your game has been saved on round: " + game.currentWave);
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
        public void LoadTurretImgs()
        {
            for (int i = 0; i < game.currentTurrets.Count; ++i)
            {
                Image image = new Image();
                image.Width = 50;
                image.Height = 50;
                image.Margin = new Thickness(game.currentTurrets[i].xPos - 25, game.currentTurrets[i].yPos - 25, 0, 0);
                image.RenderTransformOrigin = new Point(0.5, 0.5);
                image.Source = new BitmapImage(new Uri(tImageSources[game.currentTurrets[i].imageID]));
                
                turrets.Add(image);
                GameWindowCanvas.Children.Add(turrets[i]);
            }
        }

        //Creates a new bitmap everytime the buy button is clicked
        //and loads the machine gun place image into it
        //then it takes the Current cursor and changes it with the 
        //machine gun image
        // tower place methods
        private void updateTowerPlace(object sender, EventArgs e)
        {
            if (isPlacing == true)
            {
                mousePos = Mouse.GetPosition(GameWindowCanvas);
                imagetowerplace.Margin = new Thickness(mousePos.X - (imagetowerplace.ActualWidth / 2), mousePos.Y - (imagetowerplace.ActualHeight / 2), 0, 0);
            }
        }
        public int SnapToGridX(double x)
        {
            int oldx = Convert.ToInt32(x);
            int tempx = oldx / 50;
            int newx = tempx * 50;
            return newx;
        }
        public int SnapToGridY(double y)
        {
            int oldy = Convert.ToInt32(y);
            int tempy = oldy / 50;
            int newy = tempy * 50;
            return newy;
        }
        private void MapImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isPlacing == true)
            {
                imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
                Image image = new Image();
                isPlacing = false;
                image.RenderTransformOrigin = new Point(0.5, 0.5);
                image.Width = 50;
                image.Height = 50;
                mousePos = e.GetPosition(GameWindowCanvas);
                double posX = SnapToGridX(mousePos.X);
                double posY = SnapToGridY(mousePos.Y);
                image.Margin = new Thickness(posX, posY, 0, 0);
                turrets.Add(image);
                GameWindowCanvas.Children.Add(image);
                if (machinegunplace == true)
                {
                    Game.money -= 50;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/turret tower.PNG"));
                    MachineGun g = MachineGun.MakeMachineGun(posX + 25, posY + 25);
                    game.currentTurrets.Add(g);
                }
                else if (flakplace == true)
                {
                    Game.money -= 75;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower.PNG"));
                    Flak g = Flak.MakeFlak(posX + 25, posY + 25);
                    game.currentTurrets.Add(g);
                }
                else if (mortarplace == true)
                {
                    Game.money -= 200;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower.PNG"));
                    Mortar g = Mortar.MakeMortar(posX + 25, posY + 25);
                    game.currentTurrets.Add(g);
                }
                else if (teslaplace == true)
                {
                    Game.money -= 175;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tesla tower.PNG"));
                    Tesla g = Tesla.MakeTesla(posX + 25, posY + 25);
                    game.currentTurrets.Add(g);
                }
                else if (laserplace == true)
                {
                    Game.money -= 125;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower.PNG"));
                    Laser g = Laser.MakeLaser(posX + 25, posY + 25);
                    game.currentTurrets.Add(g);
                }
                else if (stunplace == true)
                {
                    Game.money -= 200;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/stun tower.PNG"));
                    Stun g = Stun.MakeStun(posX + 25, posY + 25);
                    game.currentTurrets.Add(g);
                }
                txtMoney.Text = "$" + Game.money;
            }
        }

        // button methods
        private void btnAdvanced_Click(object sender, RoutedEventArgs e)
        {
            btnBasic.IsEnabled = true;
            btnAdvanced.IsEnabled = false;
            basic = false;
            MachineGunTeslaImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tesla tower.png"));
            txtMachineGunTeslaName.Text = "Tesla Tower";
            txtMachineGunTeslaType.Text = "Target: Ground";
            txtMachineGunTeslaRange.Text = "Range: 100";
            txtMachineGunTeslaDmg.Text = "Damage: 3/s";
            txtMachineGunTeslaCost.Text = "Cost: $175";
            FlakLaserImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower.png"));
            txtFlakLaserName.Text = "Laser Tower";
            txtFlakLaserType.Text = "Target: Ground/Air";
            txtFlakLaserRange.Text = "Range: 175";
            txtFlakLaserDmg.Text = "Damage: 10/s";
            txtFlakLaserCost.Text = "Cost: $125";
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
            basic = true;
            MachineGunTeslaImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/turret tower.png"));
            txtMachineGunTeslaName.Text = "Machine Gun Tower";
            txtMachineGunTeslaType.Text = "Target: Ground";
            txtMachineGunTeslaRange.Text = "Rane: 125";
            txtMachineGunTeslaDmg.Text = "Damage: 4/s";
            txtMachineGunTeslaCost.Text = "Cost: $50";
            FlakLaserImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower.png"));
            txtFlakLaserName.Text = "Flak Tower";
            txtFlakLaserType.Text = "Target: Air";
            txtFlakLaserRange.Text = "Range: 225";
            txtFlakLaserDmg.Text = "Damage: 2/s";
            txtFlakLaserCost.Text = "Cost: $75";
            MortarStunImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower.png"));
            txtMortarStunName.Text = "Mortar Tower";
            txtMortarStunType.Text = "Target: Ground";
            txtMortarStunRange.Text = "Range: 275";
            txtMortarStunDmg.Text = "Damage: 50/5s";
            txtMortarStunCost.Text = "Cost: $150";
        }

        // buy turret methods
        private void btnMachineGunTeslaBuy_Click(object sender, RoutedEventArgs e)
        {
            if (basic)
            {
                if (Game.money >= 50)
                {
                    machinegunplace = true;
                    flakplace = mortarplace = teslaplace = laserplace = stunplace = false;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/turret tower place.png"));
                    mousePos = Mouse.GetPosition(GameWindowCanvas);
                    imagetowerplace.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
                    isPlacing = true;
                }
            }
            else
            {
                if (Game.money >= 175)
                {
                    machinegunplace = flakplace = mortarplace = laserplace = stunplace = false;
                    teslaplace = true;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tesla tower place.png"));
                    mousePos = Mouse.GetPosition(GameWindowCanvas);
                    imagetowerplace.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
                    isPlacing = true;
                }
            }
        }
        private void btnFlakLaserBuy_Click(object sender, RoutedEventArgs e)
        {
            if (basic)
            {
                if (Game.money >= 75)
                {
                    machinegunplace = mortarplace = teslaplace = laserplace = stunplace = false;
                    flakplace = true;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower place.png"));
                    mousePos = Mouse.GetPosition(GameWindowCanvas);
                    imagetowerplace.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
                    isPlacing = true;
                }
            }
            else
            {
                if (Game.money >= 125)
                {
                    machinegunplace = flakplace = mortarplace = teslaplace = stunplace = false;
                    laserplace = true;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower place.png"));
                    mousePos = Mouse.GetPosition(GameWindowCanvas);
                    imagetowerplace.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
                    isPlacing = true;
                }
            }
        }
        private void btnMortarStunBuy_Click(object sender, RoutedEventArgs e)
        {
            if (basic)
            {
                if (Game.money >= 150)
                {
                    machinegunplace = flakplace = teslaplace = laserplace = stunplace = false;
                    mortarplace = true;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower place.png"));
                    mousePos = Mouse.GetPosition(GameWindowCanvas);
                    imagetowerplace.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
                    isPlacing = true;
                }
            }
            else
            {
                if (Game.money >= 200)
                {
                    machinegunplace = flakplace = mortarplace = teslaplace = laserplace = false;
                    stunplace = true;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/stun tower place.png"));
                    mousePos = Mouse.GetPosition(GameWindowCanvas);
                    imagetowerplace.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
                    isPlacing = true;
                }
            }
        }

        private void GameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameTimer.Stop();

            game.currentEnemies = new List<Enemy>();
            Spawner.enemies = new List<Enemy>();
            game.currentTurrets = new List<Turret>();
            enemies = null;
            turrets = null;
            Turret.RotateTurret += null;
            Enemy.RotateEnemy += null;
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
    
        }
    }
}