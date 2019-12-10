using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
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
    public partial class GameWindow : Window
    {
        Game game;
        DispatcherTimer gameTimer;
        Timer nextWaveTimer; // for auto starting next wave
        public List<Image> enemies;
        public List<Image> turrets;
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
        bool isFastForward = false;
        int wave;
        public SoundHandler soundHandler;
        public bool selling = false;
        public Turret selectedTurret;
        public Image selectedRing = new Image();
        public int numWavesToWin;
        public GameWindow(bool cheat, bool isLoad, int diff, SoundHandler sentSoundHandler, int mapId)
        {
            InitializeComponent();
            Turret.ChangeImage = ChangeTowerImage;
            if (diff == 1)
            {
                numWavesToWin = 10;
            }
            else if (diff == 2)
            {
                numWavesToWin = 20;
            }
            else
            {
                numWavesToWin = 30;
            }
            soundHandler = sentSoundHandler;
            selectedRing.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/machine gun tower select.png"));
            selectedRing.RenderTransformOrigin = new Point(0.5, 0.5);
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
            tImageSources.Add("pack://application:,,,/Resources/machine gun tower.png");
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
                game = new Game(mapId, cheat, AddEnemy, RemoveEnemy, diff);
            }
            // map selection
            if (Game.map.mapID == 1)
            {
                MapImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/path2.png"));
            }
            else if (Game.map.mapID == 2)
            {
                MapImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/path3.png"));
            }
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 16);
            nextWaveTimer = new Timer();
            nextWaveTimer.Interval = 1000;
            //add update model events
            gameTimer.Tick += UpdateGame;
            gameTimer.Tick += updateTowerPlace;
            nextWaveTimer.Elapsed += SetWaveTimer;
            Turret.RotateTurret += RotateTurret;
            Enemy.RotateEnemy += RotateEnemy;
            Spawner.DisplayWave += DisplayWave;
            Turret.PlaySound += soundHandler.Play;
            btnBasic.IsEnabled = false;
            basic = true;
            txtMoney.Text += Game.money;
            txtLives.Text = "Lives: " + Game.lives;
            gameTimer.Start();
            //soundHandler.GameMusic.PlayLooping();
        }

        private void SetWaveTimer(object sender, ElapsedEventArgs e)
        {
            if (game.isWaveOver)
            {
                int time = 60;
                Dispatcher.Invoke(()=> (time = Convert.ToInt32(txtNextWaveTimer.Text) - 1));
                if(time == -1)
                {
                    Dispatcher.Invoke(() => {
                        btnNextWave_Click(null, null);
                        txtNextWaveTimer.Text = "60";
                    });
                }
                else
                {
                    Dispatcher.Invoke(() => txtNextWaveTimer.Text = time.ToString());
                }
            }
            else
            {
                Dispatcher.Invoke(() => txtNextWaveTimer.Text = "60");
            }
        }

        public void ChangeTowerImage(string tower, int index, bool value)
        {
            if (tower == "machinegun")
            {
                if (value == true)
                {
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/machine gun tower fire.png"));
                }
                else
                {
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/machine gun tower.png"));
                }
            }
            else if (tower == "flak")
            {
                if (value == true)
                {
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower fire.png"));
                }
                else
                {
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower.png"));
                }
            }
            else if (tower == "mortar")
            {
                if (value == true)
                {
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower fire.png"));
                }
                else
                {
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower.png"));
                }
            }
            else if (tower == "stun")
            {
                
            }
            else if (tower == "laser")
            {
                
            }
            else if (tower == "tesla")
            {
                
            }
        }

        // main method that updates the entire game... yikes
        public void UpdateGame(object sender, object e)
        {
            txtMoney.Text = "$" + Game.money;
            txtLives.Text = "Lives: " + Game.lives;
            if (Game.lives == 0 && game.gameOver != true)
            {
                game.gameOver = true;
                if (MessageBox.Show("Final Score: " + game.score, "You Lose!\n", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    rectname.Visibility = Visibility.Visible;
                    boxName.Visibility = Visibility.Visible;
                    txtName.Visibility = Visibility.Visible;
                    btnName.Visibility = Visibility.Visible;
                }
            }
            txtRoundDisplay.Text = "Wave: " + game.currentWave;
            txtScore.Text = "Score: " + game.score;
            wave = game.currentWave;
            if (wave == numWavesToWin && game.isWaveOver && game.gameOver != true)
            {
                game.gameOver = true;
                if (MessageBox.Show("Final Score: " + game.score + "\n" + "Continue in Endless Mode?", "You Win!", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    rectname.Visibility = Visibility.Visible;
                    boxName.Visibility = Visibility.Visible;
                    txtName.Visibility = Visibility.Visible;
                    btnName.Visibility = Visibility.Visible;
                }
            }
            game.UpdateModel();
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

        private void DisplayWave(object sender, Enemy nul)
        {
            Task.Run(() =>
            {
                while (!game.isWaveOver)
                {
                    Enemy e = Spawner.GenerateEnemy();
                    if (e != null)
                    {
                        Dispatcher.Invoke(() => AddEnemy(e));
                    }
                    else
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(500);
                }
            });
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
            i.Margin = new Thickness(e.posX, e.posY, 0, 0);

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
        public void RemoveEnemy(Enemy e, bool isKill)
        {
            game.currentEnemies.Remove(e);
            Spawner.enemies.Remove(e);  // remove it from the game state
            GameWindowCanvas.Children.Remove(enemies[e.imageIndex]); // remove from the game window canvas
            enemies.RemoveAt(e.imageIndex);     // remove it from the image list in the view
            for (int i = e.imageIndex; i < enemies.Count; ++i)
            {
                Spawner.enemies[i].imageIndex -= 1;
            }
            if (enemies.Count == 0 && Spawner.count[0] == 0 && Spawner.count[1] == 0)
            {
                game.isWaveOver = true;
                btnNextWave.IsEnabled = true;
                nextWaveTimer.Start();
            }
            if (isKill)
            {
                game.score += e.rewardScore;
            }
        }

        public void RemoveTurret(Turret t)
        {
            game.currentTurrets.Remove(t);
            GameWindowCanvas.Children.Remove(turrets[t.imageIndex]); // remove from the game window canvas
            turrets.RemoveAt(t.imageIndex);     // remove it from the image list in the view
            for (int i = t.imageIndex; i < turrets.Count; ++i)
            {
                game.currentTurrets[i].imageIndex -= 1;
            }
        }

        private void btnNextWave_Click(object sender, RoutedEventArgs e)
        {
            game.currentWave += 1;
            game.NextWave();
            btnNextWave.IsEnabled = false;
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
                image.RenderTransformOrigin = new Point(0.5, 0.5);
                image.Margin = new Thickness(game.currentTurrets[i].xPos, game.currentTurrets[i].yPos, 0, 0);
                image.MouseDown += SelectTurret;
                image.Source = new BitmapImage(new Uri(tImageSources[game.currentTurrets[i].imageID]));
                turrets.Add(image);
                GameWindowCanvas.Children.Add(turrets[i]);
            }
        }

        // Creates a new bitmap everytime the buy button is clicked
        // and loads the machine gun place image into it
        // then it takes the Current cursor and changes it with the 
        // machine gun image
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
                image.MouseDown += SelectTurret;
                int index = turrets.Count;
                turrets.Add(image);
                GameWindowCanvas.Children.Add(image);
                if (machinegunplace == true)
                {
                    Game.money -= 50;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/machine gun tower.png"));
                    MachineGun g = MachineGun.MakeMachineGun(posX, posY, index);
                    game.currentTurrets.Add(g);
                }
                else if (flakplace == true)
                {
                    Game.money -= 75;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower.PNG"));
                    Flak g = Flak.MakeFlak(posX, posY, index);
                    game.currentTurrets.Add(g);
                }
                else if (mortarplace == true)
                {
                    Game.money -= 200;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower.PNG"));
                    Mortar g = Mortar.MakeMortar(posX, posY, index);
                    game.currentTurrets.Add(g);
                }
                else if (teslaplace == true)
                {
                    Game.money -= 175;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tesla tower.PNG"));
                    Tesla g = Tesla.MakeTesla(posX, posY, index);
                    game.currentTurrets.Add(g);
                }
                else if (laserplace == true)
                {
                    Game.money -= 125;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower.PNG"));
                    Laser g = Laser.MakeLaser(posX, posY, index);
                    game.currentTurrets.Add(g);
                }
                else if (stunplace == true)
                {
                    Game.money -= 200;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/stun tower.PNG"));
                    Stun g = Stun.MakeStun(posX, posY, index);
                    game.currentTurrets.Add(g);
                }
                txtMoney.Text = "$" + Game.money;
                imagetowerplace.Margin = new Thickness(0, 0, 0, 0);
            }
            else
            {
                selectedTurret = null;
                b_upgrade_border.Visibility = Visibility.Hidden;
                btn_Upgrade.Visibility = Visibility.Hidden;
                lb_cost_to_upgrade.Visibility = Visibility.Hidden;
                lb_current_Dps.Visibility = Visibility.Hidden;
                lb_selectedType.Visibility = Visibility.Hidden;
                lb_upgraded_dps.Visibility = Visibility.Hidden;
                lb_turret_lvl.Visibility = Visibility.Hidden;              
            }
            if (GameWindowCanvas.Children.Contains(selectedRing))
            {
                GameWindowCanvas.Children.Remove(selectedRing);
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
            txtMachineGunTeslaDmg.Text = "Damage: 36/s";
            txtMachineGunTeslaCost.Text = "Cost: $175";
            FlakLaserImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower.png"));
            txtFlakLaserName.Text = "Laser Tower";
            txtFlakLaserType.Text = "Target: Ground/Air";
            txtFlakLaserRange.Text = "Range: 175";
            txtFlakLaserDmg.Text = "Damage: 60/s";
            txtFlakLaserCost.Text = "Cost: $125";
            MortarStunImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/stun tower.png"));
            txtMortarStunName.Text = "Stun Tower";
            txtMortarStunType.Text = "Target: Ground/Air";
            txtMortarStunRange.Text = "Range: 200";
            txtMortarStunDmg.Text = "Damage: 15/2s";
            txtMortarStunCost.Text = "Cost: $200";
        }
        private void btnBasic_Click(object sender, RoutedEventArgs e)
        {
            btnBasic.IsEnabled = false;
            btnAdvanced.IsEnabled = true;
            basic = true;
            MachineGunTeslaImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/machine gun tower.png"));
            txtMachineGunTeslaName.Text = "Machine Gun Tower";
            txtMachineGunTeslaType.Text = "Target: Ground";
            txtMachineGunTeslaRange.Text = "Range: 125";
            txtMachineGunTeslaDmg.Text = "Damage: 7.5/s";
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
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/machine gun tower place.png"));
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
            int count = enemies.Count();
            try
            {
                for (int i = count - 1; i > -1; --i)
                {
                    RemoveEnemy(game.currentEnemies[i], false);
                }
            }
            catch (ArgumentOutOfRangeException oops)
            {
                MessageBox.Show("oopsie");
            }
            
            game.currentTurrets = new List<Turret>();
            turrets = new List<Image>();
            Turret.RotateTurret += null;
            Enemy.RotateEnemy += null;
            Turret.PlaySound += null;
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void btn_FastForward_Click(object sender, RoutedEventArgs e)
        {
            isFastForward = !isFastForward;
            if (isFastForward)
            {
                gameTimer.Tick += UpdateGame;
            }
            else
            {
                gameTimer.Tick -= UpdateGame;
            }
        }

        private void btn_Sell_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTurret != null)
            {
                RemoveTurret(selectedTurret);
                Game.money += Convert.ToInt32(selectedTurret.cost * .8);
                GameWindowCanvas.Children.Remove(selectedRing);
                selectedTurret = null;
            }
        }
        // hightlights the clicked turret and displays its information in the bottomm right corner of the gui
        // also sets selectedTurret to the clicked turret
        public void SelectTurret(object sender, object e)
        {
            string cost = "Cost: ";
            string type = "Type: ";
            string dps = "Dps: ";
            string upDps = "Upgraded Dps: ";
            string lvl = "Level: ";
            
            for (int i = 0; i < turrets.Count; ++i)
            {
                if (sender == turrets[i])
                {
                    selectedTurret = game.currentTurrets[i];
                    Console.WriteLine(selectedTurret.type);
                }
            }
            double dmg = selectedTurret.damage;
            lb_cost_to_upgrade.Content = cost + Convert.ToInt32(selectedTurret.upCost);
            lb_current_Dps.Content = dps + Math.Round(selectedTurret.damage, 1);
            lb_turret_lvl.Content = lvl + selectedTurret.upgradeLvl;

            switch (selectedTurret.type)                 // grab the type and call methods based on it
            {
                case "flak":
                    lb_selectedType.Content = type + "Flak Cannon";
                    break;
                case "laser":
                    lb_selectedType.Content = type + "Laser Cannon";
                    break;
                case "machinegun":
                    lb_selectedType.Content = type + "Machine Gun";
                    break;
                case "mortar":
                    lb_selectedType.Content = type + "Mortar";
                    break;                   
                case "stun":
                    lb_selectedType.Content = type + "Stun Gun";
                    break;
                case "tesla":
                    lb_selectedType.Content = type + "Telsa Tower";
                    break;
                default:
                    MessageBox.Show("The type was wrong, it was: " + selectedTurret.type);
                    break;
            }
            if (selectedTurret.upgradeLvl == 1)
            {
                lb_upgraded_dps.Content = upDps + Math.Round(dmg += dmg * .2, 1);
            }
            else if (selectedTurret.upgradeLvl == 2)
            {
                
                lb_upgraded_dps.Content = upDps + Math.Round(dmg += dmg * .3, 1);
            }
            else if (selectedTurret.upgradeLvl == 3)
            {
                lb_upgraded_dps.Content = upDps + Math.Round(dmg += dmg * .4, 1);
            }
            else if (selectedTurret.upgradeLvl == 4)
            {
                lb_cost_to_upgrade.Content = "Max Lvl";
                lb_upgraded_dps.Content = "Max Lvl";
            }
            // make all the stuff visable
            b_upgrade_border.Visibility = Visibility.Visible;
            btn_Upgrade.Visibility =  Visibility.Visible;
            lb_cost_to_upgrade.Visibility = Visibility.Visible;
            lb_current_Dps.Visibility = Visibility.Visible;
            lb_selectedType.Visibility = Visibility.Visible;
            lb_upgraded_dps.Visibility = Visibility.Visible;
            lb_turret_lvl.Visibility = Visibility.Visible;

            selectedRing.Margin = new Thickness(selectedTurret.xPos- selectedTurret.range, selectedTurret.yPos - selectedTurret.range, 0, 0);
            selectedRing.Width = selectedTurret.range * 2;
            selectedRing.Height = selectedTurret.range * 2;
            GameWindowCanvas.Children.Add(selectedRing);          // add the ring around the turret         
        }

        private void btnName_Click(object sender, RoutedEventArgs e)
        {
            if (btnName.Content.ToString() == "")
            {
                MessageBox.Show("Please enter a name or alias");
            }
            else
            {
                HighScore hs = new HighScore();
                string name = boxName.Text.ToString();
                int score = game.score;
                hs.CreateScore(name, score);
                this.Close();
            }
        }

        private void btn_Upgrade_Click(object sender, RoutedEventArgs e)
        {
            string cost = "Cost: ";
            string dps = "Dps: ";
            string upDps = "Upgraded Dps: ";
            string lvl = "Level: ";
            if (selectedTurret == null)
            {
                return;
            }
            if (selectedTurret.upgradeLvl == 4) //if max lvl, stop
            {
                return;
            }
            if (Game.money > selectedTurret.upCost)
            {          
                selectedTurret.Upgrade(); // do the upgrade
                // change gui to represent the upgrade
                double dmg = selectedTurret.damage;
                lb_turret_lvl.Content = lvl + selectedTurret.upgradeLvl;
                lb_current_Dps.Content = dps + Math.Round(dmg, 1);
                if (selectedTurret.upgradeLvl == 2)
                {
                    lb_upgraded_dps.Content = upDps + Math.Round(dmg += dmg * .3, 1);
                    lb_cost_to_upgrade.Content = cost + selectedTurret.upCost;
                }
                else if (selectedTurret.upgradeLvl == 3)
                {
                    lb_upgraded_dps.Content = upDps + Math.Round(dmg += dmg * .4, 1);
                    lb_cost_to_upgrade.Content = cost + selectedTurret.upCost;
                }
                else if (selectedTurret.upgradeLvl == 4)
                {
                    lb_cost_to_upgrade.Content = "Max Lvl";
                    lb_upgraded_dps.Content = "Max Lvl";
                }                
            }
            else
            {
                // do stuff for when they dont have enough money
            }
        }
        public void Deselect(object sender, object e)
        {
            // to do levi
        }
    }
}