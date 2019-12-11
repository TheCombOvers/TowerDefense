/*
 * #SoundHandler Class#
 * 
 *  This class manages and contains
 *  all sound used and played inside
 *  the game and menus.
 *  
 *  This class belongs to the view,
 *  meaning it is allowed to access
 *  both view and model files.
 */

using System;
using System.Threading;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Threading;

namespace TowerDefenseGUI
{
    public class SoundHandler
    {
        // For providing which music should be played to the PlayMusic() method
        public enum MusicType { MainMenu, DifficultyMenu, Game }

        // Static variable determining how many of the same sound can be played simultaneously
        public static int AllowedSoundInstances { get; set; } = 5;

        // Static variable for whether sounds are muted or not
        public static bool Muted { get; set; } = false;

        // private list to track last used MediaPlayers for each sound
        private int[] CurrentSoundCounts;

        // Setting up MediaPlayer variable lists for later
        public MediaPlayer MusicPlayer = new MediaPlayer() { IsMuted = false };
        public MediaPlayer[] MachineGunPlayers;
        public MediaPlayer[] FlakPlayers;
        public MediaPlayer[] MortarPlayers;
        public MediaPlayer[] TeslaPlayers;
        public MediaPlayer[] LaserPlayers;
        public MediaPlayer[] StunPlayers;
        public MediaPlayer[] ButtonPlayers;
        public MediaPlayer[] BackButtonPlayers;

        public SoundHandler()
        {
            // Class initializer
            // Returns: nothing
            // Params:
            //  None

            // Stores the current index of Media Player being used for each sound
            CurrentSoundCounts = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            // Initialize array sizes
            MachineGunPlayers = new MediaPlayer[AllowedSoundInstances];
            FlakPlayers = new MediaPlayer[AllowedSoundInstances];
            MortarPlayers = new MediaPlayer[AllowedSoundInstances];
            TeslaPlayers = new MediaPlayer[AllowedSoundInstances];
            LaserPlayers = new MediaPlayer[AllowedSoundInstances];
            StunPlayers = new MediaPlayer[AllowedSoundInstances];
            ButtonPlayers = new MediaPlayer[AllowedSoundInstances];
            BackButtonPlayers = new MediaPlayer[AllowedSoundInstances];

            // Loop through the arrays and initialize all the variables
            for (int i = 0; i < AllowedSoundInstances; i++)
            {
                // This structure is followed for the rest of the variables each loop:

                // Initialize the object as muted (will unmute upon play request)
                MachineGunPlayers[i] = new MediaPlayer() { IsMuted = true };

                // Open and load the proper sound file
                MachineGunPlayers[i].Open(new Uri("..\\..\\Resources\\MachineGunSound.wav", UriKind.Relative));

                // Pause the sound in case it started playing
                MachineGunPlayers[i].Pause();

                // Set it back to the beginning
                MachineGunPlayers[i].Position = new TimeSpan(0);

                // Set the volume to an appropriate level
                MachineGunPlayers[i].Volume = 0.35;


                // Initialize Flak Sound Players
                FlakPlayers[i] = new MediaPlayer() { IsMuted = true };
                FlakPlayers[i].Open(new Uri("..\\..\\Resources\\FlakSound.wav", UriKind.Relative));
                FlakPlayers[i].Pause();
                FlakPlayers[i].Position = new TimeSpan(0);
                FlakPlayers[i].Volume = 0.4;

                // Initialize Mortar Sound Players
                MortarPlayers[i] = new MediaPlayer() { IsMuted = true };
                MortarPlayers[i].Open(new Uri("..\\..\\Resources\\MortarSound.wav", UriKind.Relative));
                MortarPlayers[i].Pause();
                MortarPlayers[i].Position = new TimeSpan(0);
                MortarPlayers[i].Volume = 0.5;

                // Initialize Tesla Sound Players
                TeslaPlayers[i] = new MediaPlayer() { IsMuted = true };
                TeslaPlayers[i].Open(new Uri("..\\..\\Resources\\TeslaSound.wav", UriKind.Relative));
                TeslaPlayers[i].Pause();
                TeslaPlayers[i].Position = new TimeSpan(0);
                TeslaPlayers[i].Volume = 0.3;

                // Initialize Laser Sound Players
                LaserPlayers[i] = new MediaPlayer() { IsMuted = true };
                LaserPlayers[i].Open(new Uri("..\\..\\Resources\\LaserSound.wav", UriKind.Relative));
                LaserPlayers[i].Pause();
                LaserPlayers[i].Position = new TimeSpan(0);
                LaserPlayers[i].Volume = 0.45;

                // Initialize Stun Sound Players
                StunPlayers[i] = new MediaPlayer() { IsMuted = true };
                StunPlayers[i].Open(new Uri("..\\..\\Resources\\StunSound.wav", UriKind.Relative));
                StunPlayers[i].Pause();
                StunPlayers[i].Position = new TimeSpan(0);
                StunPlayers[i].Volume = 0.5;

                // Initialize Menu Button Sound Players
                ButtonPlayers[i] = new MediaPlayer() { IsMuted = true };
                ButtonPlayers[i].Open(new Uri("..\\..\\Resources\\ShotgunSound.wav", UriKind.Relative));
                ButtonPlayers[i].Pause();
                ButtonPlayers[i].Position = new TimeSpan(0);
                ButtonPlayers[i].Volume = 0.4;

                // Initialize Back Button Sound Players
                BackButtonPlayers[i] = new MediaPlayer() { IsMuted = true };
                BackButtonPlayers[i].Open(new Uri("..\\..\\Resources\\ClipSound.wav", UriKind.Relative));
                BackButtonPlayers[i].Pause();
                BackButtonPlayers[i].Position = new TimeSpan(0);
                BackButtonPlayers[i].Volume = 0.5;
            }

            // Make the music loop if it hits the end of the .wav file
            MusicPlayer.MediaEnded += MusicPlayer_Loop;
        }

        private void MusicPlayer_Loop(object sender, EventArgs e)
        {
            // Loops the music when it hits the end of the file.
            // Returns: nothing
            MusicPlayer.Position = new TimeSpan(0);
            MusicPlayer.Play();
        }

        public void Play(object sender, string type)
        {
            // Play a sound depending on the sound type
            // Returns: nothing
            // Params:
            //  - Object sender : not used. Simply to allow for linking to an Event Handler
            //  - String type   : used to identify what sound needs to be played; is a string to simplify reading from turret

            switch (type)
            {
                // Handles machinegun sound playing
                case "machinegun":

                    // This structure is followed for the rest of these cases:

                    // Unmute the sound (sounds are loaded in muted, as they occaisionally play while loading)
                    MachineGunPlayers[CurrentSoundCounts[0]].IsMuted = Muted;

                    // Begin the sound playing. CurrentSoundCounts[0] is the index in CurrentSoundCounts that
                    //  stores which MediaPlayer we should use.
                    MachineGunPlayers[CurrentSoundCounts[0]].Play();

                    // If we're not at the end of the array of MediaPlayers for this sound:
                    if (CurrentSoundCounts[0] < AllowedSoundInstances - 1) 
                    {
                        // Store that the next MediaPlayer should be used the next time the sound is called, and
                        //  ensure that the sound will start from the beginning.
                        MachineGunPlayers[CurrentSoundCounts[0] + 1].Position = new TimeSpan(0);
                        CurrentSoundCounts[0]++; 
                    }
                    // else, start back at the beginning of the MediaPlayers array and ensure it starts from
                    //  the beginning of the sound.
                    else { CurrentSoundCounts[0] = 0; MachineGunPlayers[0].Position = new TimeSpan(0); }
                    break;

                // Handles flak sound playing
                case "flak":
                    FlakPlayers[CurrentSoundCounts[1]].IsMuted = Muted;
                    FlakPlayers[CurrentSoundCounts[1]].Play();
                    if (CurrentSoundCounts[1] < AllowedSoundInstances - 1) 
                    {
                        FlakPlayers[CurrentSoundCounts[1] + 1].Position = new TimeSpan(0);
                        CurrentSoundCounts[1]++; 
                    }
                    else { CurrentSoundCounts[1] = 0; FlakPlayers[0].Position = new TimeSpan(0); }
                    break;

                // Handles mortar sound playing
                case "mortar":
                    MortarPlayers[CurrentSoundCounts[2]].IsMuted = Muted;
                    MortarPlayers[CurrentSoundCounts[2]].Play();
                    if (CurrentSoundCounts[2] < AllowedSoundInstances - 1) 
                    {
                        MortarPlayers[CurrentSoundCounts[2] + 1].Position = new TimeSpan(0);
                        CurrentSoundCounts[2]++;
                    }
                    else { CurrentSoundCounts[2] = 0; MortarPlayers[0].Position = new TimeSpan(0); }
                    break;

                // Handles tesla sound playing
                case "tesla":
                    TeslaPlayers[CurrentSoundCounts[3]].IsMuted = Muted;
                    TeslaPlayers[CurrentSoundCounts[3]].Play();
                    if (CurrentSoundCounts[3] < AllowedSoundInstances - 1) 
                    {
                        TeslaPlayers[CurrentSoundCounts[3] + 1].Position = new TimeSpan(0);
                        CurrentSoundCounts[3]++; 
                    }
                    else { CurrentSoundCounts[3] = 0; TeslaPlayers[0].Position = new TimeSpan(0); }
                    break;

                // Handles laser sound playing
                case "laser":
                    LaserPlayers[CurrentSoundCounts[4]].IsMuted = Muted;
                    LaserPlayers[CurrentSoundCounts[4]].Play();
                    if (CurrentSoundCounts[4] < AllowedSoundInstances - 1) 
                    {
                        LaserPlayers[CurrentSoundCounts[4] + 1].Position = new TimeSpan(0);
                        CurrentSoundCounts[4]++;
                    }
                    else { CurrentSoundCounts[4] = 0; LaserPlayers[0].Position = new TimeSpan(0); }
                    break;

                // Handles stun sound playing
                case "stun":
                    StunPlayers[CurrentSoundCounts[5]].IsMuted = Muted;
                    StunPlayers[CurrentSoundCounts[5]].Play();
                    if (CurrentSoundCounts[5] < AllowedSoundInstances - 1) 
                    {
                        StunPlayers[CurrentSoundCounts[5] + 1].Position = new TimeSpan(0);
                        CurrentSoundCounts[5]++; 
                    }
                    else { CurrentSoundCounts[5] = 0; StunPlayers[0].Position = new TimeSpan(0); }
                    break;

                // Handles menubutton sound playing
                case "menubutton":
                    ButtonPlayers[CurrentSoundCounts[6]].IsMuted = Muted;
                    ButtonPlayers[CurrentSoundCounts[6]].Play();
                    if (CurrentSoundCounts[6] < AllowedSoundInstances - 1) 
                    {
                        ButtonPlayers[CurrentSoundCounts[6] + 1].Position = new TimeSpan(0);
                        CurrentSoundCounts[6]++; 
                    }
                    else { CurrentSoundCounts[6] = 0; ButtonPlayers[0].Position = new TimeSpan(0); }
                    break;

                // Handles backbutton sound playing
                case "backbutton":
                    BackButtonPlayers[CurrentSoundCounts[7]].IsMuted = Muted;
                    BackButtonPlayers[CurrentSoundCounts[7]].Play();
                    if (CurrentSoundCounts[7] < AllowedSoundInstances - 1)
                    {
                        BackButtonPlayers[CurrentSoundCounts[7] + 1].Position = new TimeSpan(0);
                        CurrentSoundCounts[7]++;
                    }
                    else { CurrentSoundCounts[7] = 0; BackButtonPlayers[0].Position = new TimeSpan(0); }
                    break;
            }
        }

        public void PlayMusic(MusicType type)
        {
            // Handles the playing of Music for pages & windows
            // Returns: nothing
            // Params:
            //  - MusicType type : provides which music should be played
            switch (type)
            {
                // Handles music playing for Main Menu
                case MusicType.MainMenu:
                    MusicPlayer.Open(new Uri("..\\..\\Resources\\bensound-evolution.wav", UriKind.Relative)); // .wav file from bensound.com
                    break;

                // Handles music playing for Difficulty Menu
                case MusicType.DifficultyMenu:
                    MusicPlayer.Open(new Uri("..\\..\\Resources\\Actionable.wav", UriKind.Relative)); // .wav file from bensound.com
                    break;

                // Handles music playing during gameplay
                case MusicType.Game:
                    MusicPlayer.Open(new Uri("..\\..\\Resources\\bensound-epic.wav", UriKind.Relative)); // .wav file from from bensound.com
                    break;
            }
            
            // Pause the sound once loaded, set it's volume, unmute it, start it from the beginning, then play it.
            MusicPlayer.Pause();
            MusicPlayer.Volume = 0.18;
            MusicPlayer.IsMuted = Muted;
            MusicPlayer.Position = new TimeSpan(0);
            MusicPlayer.Play();
        }
    }
}