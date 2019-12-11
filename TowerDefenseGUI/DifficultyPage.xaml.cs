/*
 * DifficultyPage.xaml's cs file
 * 
 * Handles buttons and methods
 * for the DifficultyPage
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TowerDefenseGUI
{
    
    // Page Logic for DifficultyPage

    // For references:
    //  Difficulty  - int, where 1 is easy, 2 is medium, 3 is hard
    //  Cheat       - bool, where true = cheats, and false = no cheats
    
    public partial class DifficultyPage : Page
    {
        // Stores whether cheat mode is on or off
        public bool Cheat = true;

        // Stores the difficulty selected. 1 - easy, 2 - medium, 3 - hard
        public int Difficulty = 1;

        // Stores a reference to the SoundHandler passed in
        public SoundHandler soundHandler;

        public DifficultyPage(SoundHandler sentSoundHandler)
        {
            // Class Initializer
            // Returns: nothing
            // Params:
            // - SoundHandler sentSoundHandler : a reference to our already prepared SoundHandler from the splash screen

            InitializeComponent();
            soundHandler = sentSoundHandler;

            // Start the difficulty page music
            soundHandler.PlayMusic(SoundHandler.MusicType.DifficultyMenu);
        }
        private void BtnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            // Event Handler for pressing the "Main Menu" btn
            // Returns: nothing
            // Params:
            // - object sender : not used. Only for enabling method as an Event Handler
            // - RoutedEventArgs e  : not used. Only for enabling method as an Event Handler

            // Plays the sound, opens a new main menu and closes the current window.

            soundHandler.Play(null, "backbutton");
            MainMenu mainMenu = new MainMenu(soundHandler);
            mainMenu.Show();
            Window.GetWindow(this).Close();
        }

        private void BtnMainMenu_Hover(object sender, RoutedEventArgs e)
        {
            // Event Handler for hovering over the "Main Menu" btn
            // Returns: nothing
            // Params:
            // - object sender : not used. Only for enabling method as an Event Handler
            // - RoutedEventArgs e  : not used. Only for enabling method as an Event Handler

            btnRect.Fill = Brushes.LightSkyBlue;
        }

        private void BtnMainMenu_UnHover(object sender, RoutedEventArgs e)
        {
            // Event Handler for no longer hovering over the "Main Menu" btn.
            //  Changes the color of the button to the normal color
            // Returns: nothing
            // Params:
            // - object sender : not used. Only for enabling method as an Event Handler
            // - RoutedEventArgs e  : not used. Only for enabling method as an Event Handler

            btnRect.Fill = Brushes.LightGray;
        }

        private void CheatMode_Click(object sender, RoutedEventArgs e)
        {
            // Event Handler for clicking the Cheat Mode btn; swaps between
            //  "Cheat Mode: On" and "Cheat Mode: Off" and changes variable
            //  appropriately
            // Returns: nothing
            // Params:
            // - object sender : not used. Only for enabling method as an Event Handler
            // - RoutedEventArgs e  : not used. Only for enabling method as an Event Handler

            if (CheatMode.Content.ToString() == "Cheat Mode: On")
            {
                CheatMode.Content = "Cheat Mode: Off";
                CheatMode.Foreground = Brushes.DarkRed;
                Cheat = false;
            }
            else
            {
                CheatMode.Content = "Cheat Mode: On";
                CheatMode.Foreground = Brushes.Red;
                Cheat = true;
            }
        }

        private void DifficultySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Difficulty = Convert.ToInt32(((Slider)sender).Value);
            switch (Difficulty)
            {
                case 1:
                    DifficultyDisplay.Text = "Easy";
                    DifficultyDisplay.Foreground = Brushes.LightGreen;
                    Difficulty = 1;
                    break;
                case 2:
                    DifficultyDisplay.Text = "Medium";
                    DifficultyDisplay.Foreground = Brushes.Orange;
                    Difficulty = 2;
                    break;
                case 3:
                    DifficultyDisplay.Text = "Hard";
                    DifficultyDisplay.Foreground = Brushes.Red;
                    Difficulty = 3;
                    break;
            }
        }
        private void BtnSelectMapEasy_Click(object sender, RoutedEventArgs e)
        {
            //soundHandler.DifficultyPageMusic.Stop();
            var gameWindow = new GameWindow(Cheat, false, Difficulty, soundHandler, 0);  
            gameWindow.Show();
            Window hostWindow = Window.GetWindow(this);
            hostWindow.Close();
        }

        private void BtnSelectMapMedium_Click(object sender, RoutedEventArgs e)
        {
            var gameWindow = new GameWindow(Cheat, false, Difficulty, soundHandler, 1); 
            gameWindow.Show();
            Window hostWindow = Window.GetWindow(this);
            //soundHandler.DifficultyPageMusic.Stop();
            hostWindow.Close();
        }

        private void BtnSelectMapHard_Click(object sender, RoutedEventArgs e)
        {
            var gameWindow = new GameWindow(Cheat, false, Difficulty, soundHandler, 2);  
            
            gameWindow.Show();
            Window hostWindow = Window.GetWindow(this);
            //soundHandler.DifficultyPageMusic.Stop();
            hostWindow.Close();
        }
    }
}
