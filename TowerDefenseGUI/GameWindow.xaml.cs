// GAME WINDOW-view
/* This file contains the game window class which contians all the methods that
 * update the view of the game and display it's functionality. */
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
using System.Diagnostics;

namespace TowerDefenseGUI
{
    // This class initalizes the game window and provideds the view for the game.
    // it calls all the method neccassary to run the game and keeps track of all the view level variables for the game.
    
    public partial class GameWindow : Window
    {
        Game game; // our instance of the model class for the game
        DispatcherTimer gameTimer;  // timer that tick 60/s to update the game
        Timer nextWaveTimer; // timer to auto start the next wave at the end of a wave
        public List<Image> enemies; // images for all the enemies currently on the field
        public List<Image> turrets; // images for allt the turrets currently on the field 
        // the master list of image sources for enemies
        List<string> eImageSources; // 0:infantry, 1:vehicle basic, 2:aircraft basic, 3:ground boss
        // 4:advance ground unit, 5:advanced ground vehicle, 6:aircraft advanced, 7: air boss
        // the master list of image sources for turrets
        List<string> tImageSources;// 0:MG tower, 1:flak tower, 2:laser tower, 3:mortar, 4:stun, 5:tesla
        bool isPlacing; // true if the player is placing a turret else false
        Point mousePos; // the current position of the mouse
        bool basic; // true if placing a basic turret
        bool machinegunplace;   // true if placing a machine gun
        bool flakplace;     // true if placing a flak cannon    
        bool mortarplace;   // true if placing a mortar
        bool teslaplace;    // true if placing a tesla      
        bool laserplace;    // true if placing a laser
        bool isFastForward = false; // true if the game is in fast forward
        public SoundHandler soundHandler;   // Instance of the sound class for playing all music and affects  
        public Turret selectedTurret;   // instance of the currently selected turret 
        public Image selectedRing;   // instance of the image used for selecting turrets
        public int numWavesToWin;       // the number of wave to beat the game
        HighScore hs;   // instance of the highscore class for the game

        // constructor for GameWindow sets all instance variables, decides difficulty, what map is being played, 
        // loads the saved game if load is true, starts the game timer.
        public GameWindow(bool cheat, bool isLoad, int diff, SoundHandler sentSoundHandler, int mapId)
        {
            InitializeComponent();
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
            hs = new HighScore();
            selectedRing = new Image();
            soundHandler = sentSoundHandler;
            selectedRing.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tower select.png"));
            selectedRing.RenderTransformOrigin = new Point(0.5, 0.5);
            selectedRing.MouseDown += Deselect;
            btnFlakLaserBuy.Click += Deselect;
            btnFlakLaserBuy.Click += Deselect;
            btnMachineGunTeslaBuy.Click += Deselect;
            btnNextWave.Click += Deselect;
            btnPauseGame.Click += Deselect;
            btnSaveGame.Click += Deselect;
            btn_Sell_Turret.Click += Deselect;
            side_menu.MouseDown += Deselect;
            btn_fast_forward.Click += Deselect;
            MapImage.MouseDown += Deselect;
            turrets = new List<Image>();
            enemies = new List<Image>();

            eImageSources = new List<string>();
            eImageSources.Add("pack://application:,,,/Resources/Basic Ground Unit.png");
            eImageSources.Add("pack://application:,,,/Resources/Basic Ground Vehicle.png");
            eImageSources.Add("pack://application:,,,/Resources/Basic Aircraft.png");
            eImageSources.Add("pack://application:,,,/Resources/Ground Boss.png");
            eImageSources.Add("pack://application:,,,/Resources/Advanced Ground Unit.png");
            eImageSources.Add("pack://application:,,,/Resources/Advanced Ground Vehicle.png");
            eImageSources.Add("pack://application:,,,/Resources/Advanced Aircraft.png");
            eImageSources.Add("pack://application:,,,/Resources/Aircraft Boss.png");

            tImageSources = new List<string>();
            tImageSources.Add("pack://application:,,,/Resources/machine gun tower.png");
            tImageSources.Add("pack://application:,,,/Resources/flak tower.png");
            tImageSources.Add("pack://application:,,,/Resources/laser tower.png");
            tImageSources.Add("pack://application:,,,/Resources/mortar tower.png");
            tImageSources.Add("pack://application:,,,/Resources/stun tower.png");
            tImageSources.Add("pack://application:,,,/Resources/tesla tower.png");

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
            gameTimer.Tick += UpdateGame;
            gameTimer.Tick += updateTowerPlace;
            nextWaveTimer.Elapsed += SetWaveTimer;
            Turret.RotateTurret += RotateTurret;
            Enemy.RotateEnemy += RotateEnemy;
            Spawner.DisplayWave += DisplayWave;
            Turret.PlaySound += soundHandler.Play;
            Turret.ChangeImage += ChangeTowerImage;
            btnBasic.IsEnabled = false;
            basic = true;
            txtMoney.Text += Game.money;
            txtLives.Text = "Lives: " + Game.lives;
            gameTimer.Start();
            soundHandler.PlayMusic(SoundHandler.MusicType.Game);
        }

        // resets the next wave timer each wave and keeps the timer running in between waves
        // gets called by nextWaveTimer
        private void SetWaveTimer(object sender, ElapsedEventArgs e)
        {
            if (game.isWaveOver)
            {
                Dispatcher.Invoke(() =>
                {
                    int time = Convert.ToInt32(txtNextWaveTimer.Text) - 1;
                    if (time == -1)
                    {

                        btnNextWave_Click(null, null);
                        txtNextWaveTimer.Text = "60";
                    }
                    else
                    {
                        txtNextWaveTimer.Text = time.ToString();
                    }
                });
            }
            else
            {
                Dispatcher.Invoke(() => txtNextWaveTimer.Text = "60");
            }
        }
        // changes the given tower type at the passed index to the firing form or the none firing form of the turret based off of value
        // returns nothing
        public void ChangeTowerImage(string tower, int index, bool value)
        {
            // switches between tower firing images and non-firing images to animate firing visuals
            if (tower == "machinegun")
            {
                if (value)
                {
                    // changes machine gun tower image to firing if enemy is within range
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/machine gun tower fire.png"));
                }
                else
                {
                    // changes machine gun tower image to non-firing
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/machine gun tower.png"));
                }
            }
            else if (tower == "flak")
            {
                if (value)
                {
                    // changes flak tower image to firing
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower fire.png"));
                }
                else
                {
                    // changes flak tower image to non-firing
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower.png"));
                }
            }
            else if (tower == "mortar")
            {
                if (value)
                {
                    // changes mortar image to firing
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower fire.png"));
                }
                else
                {
                    // changes mortar image to non-firing
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower.png"));
                }
            }
            else if (tower == "stun")
            {
                if (value)
                {
                    // changes stun tower image to firing
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/stun tower fire.png"));
                }
                else
                {
                    // changes stun tower image to non-firing
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/stun tower.png"));
                }
            }
            else if (tower == "laser")
            {
                if (value)
                {
                    // changes laser tower image to firing
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower fire.png"));
                }
                else
                {
                    // changes laser tower image to non-firing
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower.png"));
                }
            }
            else
            {
                if (value)
                {
                    // changes tesla tower image to firing
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tesla tower fire.png"));
                }
                else
                {
                    // changes tesla tower image to non-firing
                    turrets[index].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tesla tower.png"));
                }
            }
        }

        // main event handler that updates the entire game
        // returns nothing, takes no params
        public void UpdateGame(object sender, object e)
        {
            IsGameOver();
            game.UpdateModel();
            UpdateView();
        }

        // when the game is over executes the game ending sequence
        // returns nothing, takes no params
        private void IsGameOver()
        {
            if (Game.lives == 0 && !game.gameOver)
            {
                game.gameOver = true;
                gameTimer.Stop();
                nextWaveTimer.Stop();
                if (MessageBox.Show("Final Score: " + game.score, "You Lose!\n", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    rectname.Visibility = Visibility.Visible;
                    boxName.Visibility = Visibility.Visible;
                    txtName.Visibility = Visibility.Visible;
                    btnName.Visibility = Visibility.Visible;
                }
            }
            if (game.currentWave == numWavesToWin && game.isWaveOver && !game.gameOver)
            {
                game.gameOver = true;
                gameTimer.Stop();
                nextWaveTimer.Stop();
                if (MessageBox.Show("Final Score: " + game.score + "\n" + "Continue in Endless Mode?", "You Win!", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    rectname.Visibility = Visibility.Visible;
                    boxName.Visibility = Visibility.Visible;
                    txtName.Visibility = Visibility.Visible;
                    btnName.Visibility = Visibility.Visible;
                }
                else
                {
                    gameTimer.Start();
                    nextWaveTimer.Start();
                }
            }
        }

        // updates the view based off of the state of the model
        // returns nothing, takes no params
        public void UpdateView()
        {
            // access the game properties
            // draw objects based off the properties
            txtMoney.Text = "$" + Game.money;
            txtLives.Text = "Lives: " + Game.lives;
            txtRoundDisplay.Text = "Wave: " + game.currentWave;
            txtScore.Text = "Score: " + game.score;
            int counter = 0;
            if (enemies.Count > 0)
            {
                foreach (Image en in enemies)
                {
                    en.Margin = new Thickness(game.currentEnemies[counter].posX, game.currentEnemies[counter].posY, 0, 0);
                    ++counter;
                }
            }
            // tells if turret is placed on map
            // if wave is over, changes all images of towers back to non-firing image
            if (game.isWaveOver == true)
            {
                for (int i = 0; i < game.currentTurrets.Count; ++i)
                {
                    turrets[i].Source = new BitmapImage(new Uri(tImageSources[game.currentTurrets[i].imageID]));
                }
            }
        }

        // event handler for when the next wave starts. adds the enemies generated by spawer to the view at intervals of .5 seconds
        // returns nothing, takes no params
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

        // rotates the given enemy object to the given degrees
        // returns nothing
        public void RotateEnemy(object en, int degrees)
        {
            // rotates enemy images to simulate turning on path
            int index = game.currentEnemies.IndexOf(en as Enemy);
            if (game.currentEnemies.Contains(en) && enemies.Count >= index + 1)
            {
                enemies[index].RenderTransform = new RotateTransform(degrees);
            }
        }
        // rotates the given turret object to the given degrees
        // returns nothing
        public void RotateTurret(object tur, int degrees)
        {
            // rotates turret images to simulate visuals of turret firing at enemy in range
            if (game.currentTurrets.Contains(tur))
            {
                int index = game.currentTurrets.IndexOf(tur as Turret);
                turrets[index].RenderTransform = new RotateTransform(degrees);
            }
        }

        // creates a new coordinationg image for the given enemy "e" and and places on the screen and adds it to
        // the list of images of enemies in the view
        // returns nothing
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

        // removes a specified enemy from the game state and the view and gives the player the appropriate amount of money if isKill is true
        // returns nothing
        public void RemoveEnemy(Enemy e, bool isKill)
        {
            if (GameWindowCanvas.Children.Contains(enemies[e.imageIndex]))
            {
                GameWindowCanvas.Children.Remove(enemies[e.imageIndex]); // remove from the game window canvas
            }
            else
            {
                return;
            }
            game.currentEnemies.Remove(e);
            Spawner.enemies.Remove(e);  // remove it from the game state           
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

        // removes the given turrent from the model and the view
        // returns nothing
        public void RemoveTurret(Turret t)
        {
            game.currentTurrets.Remove(t);
            GameWindowCanvas.Children.Remove(turrets[t.imageIndex]); // remove from the game window canvas
            turrets.RemoveAt(t.imageIndex);     // remove it from the image list in the view
            for (int i = t.imageIndex; i < turrets.Count; ++i)
            {
                game.currentTurrets[i].imageIndex -= 1;
            }
            Debug.WriteLine("Number of Turrets " + turrets.Count());
        }

        // event handler for when next wave is clicked, starts the next wave and increments the current wave
        // returns nothing
        private void btnNextWave_Click(object sender, RoutedEventArgs e)
        {
            // advanced to the next wave
            game.currentWave += 1;
            game.NextWave();
            btnNextWave.IsEnabled = false; // disables next wave button
            game.isWaveOver = false;
        }

        // calls the SaveGame method and tells the user their game was saved on <current wave they're on>
        // returns nothing
        private void btnSaveGame_Click(object sender, RoutedEventArgs e)
        {
            // saves the current game state
            game.SaveGame("..\\..\\Resources\\SavedGame3.txt");
            MessageBox.Show("Your game has been saved on round: " + game.currentWave);
        }

        // stops the game timer and the next wave timer pausing the game 
        // returns nothing
        private void btnPauseGame_Click(object sender, RoutedEventArgs e)
        {
            // stops the timer
            // freezes all objects on the screen
            Button pressBtn = (Button)sender;
            pressBtn.Content = "Resume";
            pressBtn.Click += btnResumeGame_Click;
            gameTimer.Stop();
            nextWaveTimer.Stop();
        }

        // starts the game timer and the next wave timer starting the game back up again 
        // returns nothing
        private void btnResumeGame_Click(object sender, RoutedEventArgs e)
        {
            // resumes the timer to resume game
            // all objects on screen unfreeze
            Button pressBtn = (Button)sender;
            pressBtn.Content = "Pause";
            pressBtn.Click += btnPauseGame_Click;
            gameTimer.Start();
            nextWaveTimer.Start();
        }

        // loops through the turrets in the game state and adds them to the model as the appropriate localtions
        // returns nothing, takes no params
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

        // if the user is placing a turret then move the tower place image to the current localtion of the mouse
        // returns nothing
        private void updateTowerPlace(object sender, EventArgs e)
        {
            if (isPlacing)
            {
                mousePos = Mouse.GetPosition(GameWindowCanvas);
                imagetowerplace.Margin = new Thickness(mousePos.X - (imagetowerplace.ActualWidth / 2), mousePos.Y - (imagetowerplace.ActualHeight / 2), 0, 0);
            }
        }

        // calculates the grid pos of the turret being placed base off of x and returns the new int x-value
        public int SnapToGridX(double x)
        {
            // gets X coordinate for tower placement
            int oldx = Convert.ToInt32(x);
            int tempx = oldx / 50;
            int newx = tempx * 50;
            return newx;
        }

        // calculates the grid pos of the turret being placed base off of y and returns the new int y-value
        public int SnapToGridY(double y)
        {
            // gets Y coordinate for tower placement
            int oldy = Convert.ToInt32(y);
            int tempy = oldy / 50;
            int newy = tempy * 50;
            return newy;
        }

        // event handler for when the user places a tower.
        // check for if the user is over a valid location then creates and places the selected turret at the position of the mouse but snaped to a postion on a grid of block of sise 50x50
        // updates the view with the new turret\
        // also removes the selected turret items from the screen if neccassarys
        // returns nothing
        private void MapImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //places selected tower on map
            mousePos = e.GetPosition(GameWindowCanvas);
            if (mousePos.X > 1000)
            {
                return;
            }
            if (isPlacing)
            {
                imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));

                // adds tower image to map
                Image image = new Image();
                isPlacing = false;
                image.RenderTransformOrigin = new Point(0.5, 0.5);
                image.Width = 50;
                image.Height = 50;

                double posX = SnapToGridX(mousePos.X);
                double posY = SnapToGridY(mousePos.Y);
                image.Margin = new Thickness(posX, posY, 0, 0);
                foreach(Image tI in turrets)
                {
                    if(tI.Margin == image.Margin)
                    {
                        imagetowerplace.Margin = new Thickness(-50, -50, 0, 0);
                        return;
                    }
                }
                for(int i=0; i<Map.coords.Count-2; i++)
                {
                    var pt1 = Map.coords[i];
                    var pt2 = Map.coords[i+1];
                    if (!IsOnPath(image.Margin, pt1, pt2))
                    {
                        imagetowerplace.Margin = new Thickness(-50, -50, 0, 0);
                        return;
                    }
                }
                image.MouseDown += SelectTurret;
                int index = turrets.Count;
           
                if (machinegunplace)
                {
                    Game.money -= 50;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/machine gun tower.png"));
                    MachineGun g = MachineGun.MakeMachineGun(posX, posY, index);
                    game.currentTurrets.Add(g);
                }
                else if (flakplace)
                {
                    Game.money -= 75;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flak tower.PNG"));
                    Flak g = Flak.MakeFlak(posX, posY, index);
                    game.currentTurrets.Add(g);
                }
                else if (mortarplace)
                {
                    Game.money -= 200;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mortar tower.PNG"));
                    Mortar g = Mortar.MakeMortar(posX, posY, index);
                    game.currentTurrets.Add(g);
                }
                else if (teslaplace)
                {
                    Game.money -= 175;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tesla tower.PNG"));
                    Tesla g = Tesla.MakeTesla(posX, posY, index);
                    game.currentTurrets.Add(g);
                }
                else if (laserplace)
                {
                    Game.money -= 125;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower.PNG"));
                    Laser g = Laser.MakeLaser(posX, posY, index);
                    game.currentTurrets.Add(g);
                }
                else
                {
                    Game.money -= 200;
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/stun tower.PNG"));
                    Stun g = Stun.MakeStun(posX, posY, index);
                    game.currentTurrets.Add(g);
                }
                turrets.Add(image);
                GameWindowCanvas.Children.Add(image);
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

        // checks the given location (margin) to see if the given location is on the path between the given intersections pt1 and pt2
        // reuturn true if the location is on the path, and false if it's not
        private bool IsOnPath(Thickness margin, Intersection pt1, Intersection pt2)
        {
            if (pt1.x == pt2.x)
            {
                if (margin.Left == pt1.x+25 || margin.Left == pt1.x - 25)
                {
                    if (margin.Top > pt1.y)
                    {
                        if (margin.Top < pt2.y)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (margin.Top > pt2.y)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (margin.Top == pt1.y +25 || margin.Top == pt1.y - 25)
                {
                    if (margin.Left > pt1.x)
                    {
                        if (margin.Left < pt2.x)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (margin.Left > pt2.x)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        // switches to advanced tab
        // changes available towers
        // makes advanced tab button disabled
        // makes basic tab button enabled
        // returns nothing
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

        // switches to basic tab
        // changes available towers
        // makes basic tab button disabled
        // makes advanced tab button enabled
        // reuturns nothing
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

        // button to buy machine gun tower or tesla tower
        // checks if basic tab or advanced tab is selected
        // checks if player has enough money for the selected tower
        // then places the appropriate image on the screen at the current mouse positions and sets isPlacing to true
        // sets the selected tower's bool to true and all other tower bools to false
        // returns nothing
        private void btnMachineGunTeslaBuy_Click(object sender, RoutedEventArgs e)
        {            
            if (basic)
            {              
                if (Game.money >= 50)
                {               
                    machinegunplace = true;
                    flakplace = mortarplace = teslaplace = laserplace = false;
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
                    machinegunplace = flakplace = mortarplace = laserplace = false;
                    teslaplace = true;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/tesla tower place.png"));
                    mousePos = Mouse.GetPosition(GameWindowCanvas);
                    imagetowerplace.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
                    isPlacing = true;
                }
            }
        }


        // button to buy machine gun tower or tesla tower
        // checks if basic tab or advanced tab is selected
        // checks if player has enough money for the selected tower
        // then places the appropriate image on the screen at the current mouse positions and sets isPlacing to true
        // sets the selected tower's bool to true and all other tower bools to false
        // returns nothing
        private void btnFlakLaserBuy_Click(object sender, RoutedEventArgs e)
        {
            if (basic)
            {
                if (Game.money >= 75)
                {
                    machinegunplace = mortarplace = teslaplace = laserplace = false;
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
                    machinegunplace = flakplace = mortarplace = teslaplace = false;
                    laserplace = true;
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/laser tower place.png"));
                    mousePos = Mouse.GetPosition(GameWindowCanvas);
                    imagetowerplace.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
                    isPlacing = true;
                }
            }
        }
        // button to buy machine gun tower or tesla tower
        // checks if basic tab or advanced tab is selected
        // checks if player has enough money for the selected tower
        // then places the appropriate image on the screen at the current mouse positions and sets isPlacing to true
        // sets the selected tower's bool to true and all other tower bools to false
        // returns nothing
        private void btnMortarStunBuy_Click(object sender, RoutedEventArgs e)
        {
            if (basic)
            {
                if (Game.money >= 150)
                {
                    machinegunplace = flakplace = teslaplace = laserplace = false;
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
                    imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/stun tower place.png"));
                    mousePos = Mouse.GetPosition(GameWindowCanvas);
                    imagetowerplace.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
                    isPlacing = true;
                }
            }
        }
        // stops the gmae timer, cleans up state from the closing game and opens a new main menu instance and shows it
        // returns nothing
        private void GameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameTimer.Stop();
            int count = enemies.Count();
            for (int i = count - 1; i > -1; --i)
            {
                RemoveEnemy(game.currentEnemies[i], false);
            }
            int count2 = turrets.Count();
            for (int i = count2 - 1; i > -1; --i)
            {
                RemoveTurret(game.currentTurrets[i]);
            }
            Turret.RotateTurret -= RotateTurret;
            Enemy.RotateEnemy -= RotateEnemy;
            Spawner.DisplayWave -= DisplayWave;
            Turret.PlaySound -= soundHandler.Play;
            Turret.ChangeImage -= ChangeTowerImage;
            MainMenu mainMenu = new MainMenu(soundHandler);
            mainMenu.Show();
        }

        // increases the rate of the game timer by 2x to speed up the game
        // returns nothing
        private void btn_FastForward_Click(object sender, RoutedEventArgs e)
        {
            var obj = sender as Button;
            isFastForward = !isFastForward;
            if (isFastForward)
            {
                gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 8);
                obj.FontWeight = FontWeights.Bold;
            }
            else
            {
                gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 16);
                obj.FontWeight = FontWeights.Normal;
            }
        }

        // if selectedTurret is not null then call remove turret and give the player 80% of their money back
        // returns nothing
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
        // returns nothing
        public void SelectTurret(object sender, object e)
        {
            btn_Sell_Turret.IsEnabled = true;
        
            
            string cost = "Cost: ";
            string type = "Type: ";
            string dps = "Damge: ";
            string upDps = "Upgraded Dps: ";
            string lvl = "Level: ";

            for (int i = 0; i < turrets.Count; ++i)
            {
                if (sender == turrets[i])
                {
                    selectedTurret = game.currentTurrets[i];
                }
            }
            if (selectedTurret.upgradeLvl >= 4)
            {
                btn_Upgrade.IsEnabled = false;
            }
            else
            {
                btn_Upgrade.IsEnabled = true;
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
            btn_Upgrade.Visibility = Visibility.Visible;
            lb_cost_to_upgrade.Visibility = Visibility.Visible;
            lb_current_Dps.Visibility = Visibility.Visible;
            lb_selectedType.Visibility = Visibility.Visible;
            lb_upgraded_dps.Visibility = Visibility.Visible;
            lb_turret_lvl.Visibility = Visibility.Visible;

            selectedRing.Margin = new Thickness(selectedTurret.xPos - selectedTurret.range + 25, selectedTurret.yPos - selectedTurret.range + 25, 0, 0);
            selectedRing.Width = selectedTurret.range * 2;
            selectedRing.Height = selectedTurret.range * 2;
            if (!GameWindowCanvas.Children.Contains(selectedRing))
            {
                GameWindowCanvas.Children.Add(selectedRing);          // add the ring around the turret  
            }
        }

        // button for submitting new high score at the end of the match
        // message box appears if no name has been entered
        // sends name and score to HighScore class
        // returns nothing
        private void btnName_Click(object sender, RoutedEventArgs e)
        {     
            if (btnName.Content.ToString() == "")
            {             
                MessageBox.Show("Please enter a name or alias");
            }
            else
            {     
                string name = boxName.Text.ToString();
                int score = game.score;
                hs.CreateScore(name, score);
                Close();
            }
        }
        // checked if a turret is selected and if one is and the player has enough money
        // it upgrades the damage of the selected turret and displays that changes at the bottom right of the screen
        private void btn_Upgrade_Click(object sender, RoutedEventArgs e)
        {
            string cost = "Cost: ";
            string dps = "Damage: ";
            string upDps = "Upgraded Dps: ";
            string lvl = "Level: ";
            if (selectedTurret == null)
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
                    btn_Upgrade.IsEnabled = false;
                }                
            }
        }

        // makes the selected turret informations invisable and sets selecteTurret to null
        // and removes the highlighting ring from the game canvas
        // reuturns nothing
        public void Deselect(object sender, object e)
        {
            selectedTurret = null;
            b_upgrade_border.Visibility = Visibility.Hidden;
            btn_Upgrade.Visibility = Visibility.Hidden;
            lb_cost_to_upgrade.Visibility = Visibility.Hidden;
            lb_current_Dps.Visibility = Visibility.Hidden;
            lb_selectedType.Visibility = Visibility.Hidden;
            lb_upgraded_dps.Visibility = Visibility.Hidden;
            lb_turret_lvl.Visibility = Visibility.Hidden;

            if (GameWindowCanvas.Children.Contains(selectedRing))
            {
                GameWindowCanvas.Children.Remove(selectedRing);
            }
        }
        // sets the image tower place to a empty image and moves it off the screen
        // sets isPlaceing to false
        // returns nothing
        private void MapImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isPlacing = false;
            imagetowerplace.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empty.png"));
            imagetowerplace.Margin = new Thickness(-50, -50, 0, 0);
        }
    }
}