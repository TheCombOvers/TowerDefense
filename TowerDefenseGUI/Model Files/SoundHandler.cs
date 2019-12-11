/*
 * SoundHandler Class
 *  designed by Micah Hanevich
 * 
 * This class manages and contains
 * all sound used and played inside
 * the game and menus.
 * 
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
        public enum MusicType { MainMenu, DifficultyMenu, Game }

        // Static variable determining how many of the same sound can be played simultaneously
        public static int AllowedSoundInstances { get; set; } = 5;

        // Static variable for whether sounds are muted or not
        public static bool Muted { get; set; } = false;

        private int[] CurrentSoundCounts;

        /*
         * LEGACY CODE
        public SoundPlayer MainMenuMusic = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\bensound-evolution.wav" }; // Music by Bensound.com
        public SoundPlayer DifficultyPageMusic = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\Actionable.wav" }; // Music by Bensound.com
        public SoundPlayer GameMusic = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\bensound-epic.wav" }; // Music by Bensound.com
        public SoundPlayer MachineGunSound = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\MachineGunSound.wav" };
        public SoundPlayer FlakSound = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\FlakSound.wav" };
        public SoundPlayer MortarSound = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\MortarSound.wav" };
        public SoundPlayer TeslaSound = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\TeslaSound.wav" };
        public SoundPlayer LaserSound = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\LaserSound.wav" };
        public SoundPlayer StunSound = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\StunSound.wav" };
        public SoundPlayer MenuButton = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\ClipSound.wav" };
        */

        // Setting up MediaPlayer variables for later
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
            /*
             * LEGACY CODE
            if (!MachineGunSound.IsLoadCompleted) { MachineGunSound.Load(); }
            if (!FlakSound.IsLoadCompleted) { FlakSound.Load(); }
            if (!MortarSound.IsLoadCompleted) { MortarSound.Load(); }
            if (!TeslaSound.IsLoadCompleted) { TeslaSound.Load(); }
            if (!LaserSound.IsLoadCompleted) { LaserSound.Load(); }
            if (!StunSound.IsLoadCompleted) { StunSound.Load(); }
            if (!MenuButton.IsLoadCompleted) { MenuButton.Load(); }
             * from SoundPlayer version
            */

            // Stores the current index of Media Player begin used for each sound
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

            for (int i = 0; i < AllowedSoundInstances; i++)
            {
                // Initialize Machine Gun Media Players
                MachineGunPlayers[i] = new MediaPlayer() { IsMuted = true };
                MachineGunPlayers[i].Open(new Uri("..\\..\\Resources\\MachineGunSound.wav", UriKind.Relative));
                MachineGunPlayers[i].Pause();
                MachineGunPlayers[i].Position = new TimeSpan(0);
                MachineGunPlayers[i].Volume = 0.35;

                // Initialize Flak Media Players
                FlakPlayers[i] = new MediaPlayer() { IsMuted = true };
                FlakPlayers[i].Open(new Uri("..\\..\\Resources\\FlakSound.wav", UriKind.Relative));
                FlakPlayers[i].Pause();
                FlakPlayers[i].Position = new TimeSpan(0);
                FlakPlayers[i].Volume = 0.4;

                // Initialize Mortar Media Players
                MortarPlayers[i] = new MediaPlayer() { IsMuted = true };
                MortarPlayers[i].Open(new Uri("..\\..\\Resources\\MortarSound.wav", UriKind.Relative));
                MortarPlayers[i].Pause();
                MortarPlayers[i].Position = new TimeSpan(0);
                MortarPlayers[i].Volume = 0.5;

                // Initialize Tesla Media Players
                TeslaPlayers[i] = new MediaPlayer() { IsMuted = true };
                TeslaPlayers[i].Open(new Uri("..\\..\\Resources\\TeslaSound.wav", UriKind.Relative));
                TeslaPlayers[i].Pause();
                TeslaPlayers[i].Position = new TimeSpan(0);
                TeslaPlayers[i].Volume = 0.3;

                // Initialize Laser Media Players
                LaserPlayers[i] = new MediaPlayer() { IsMuted = true };
                LaserPlayers[i].Open(new Uri("..\\..\\Resources\\LaserSound.wav", UriKind.Relative));
                LaserPlayers[i].Pause();
                LaserPlayers[i].Position = new TimeSpan(0);
                LaserPlayers[i].Volume = 0.45;

                // Initialize Stun Media Players
                StunPlayers[i] = new MediaPlayer() { IsMuted = true };
                StunPlayers[i].Open(new Uri("..\\..\\Resources\\StunSound.wav", UriKind.Relative));
                StunPlayers[i].Pause();
                StunPlayers[i].Position = new TimeSpan(0);
                StunPlayers[i].Volume = 0.5;

                // Initialize Menu Button Media Players
                ButtonPlayers[i] = new MediaPlayer() { IsMuted = true };
                ButtonPlayers[i].Open(new Uri("..\\..\\Resources\\ShotgunSound.wav", UriKind.Relative));
                ButtonPlayers[i].Pause();
                ButtonPlayers[i].Position = new TimeSpan(0);
                ButtonPlayers[i].Volume = 0.4;

                // Initialize Back Button Media Players
                BackButtonPlayers[i] = new MediaPlayer() { IsMuted = true };
                BackButtonPlayers[i].Open(new Uri("..\\..\\Resources\\ClipSound.wav", UriKind.Relative));
                BackButtonPlayers[i].Pause();
                BackButtonPlayers[i].Position = new TimeSpan(0);
                BackButtonPlayers[i].Volume = 0.5;
            }

            MusicPlayer.MediaEnded += MusicPlayer_Loop;
        }

        private void MusicPlayer_Loop(object sender, EventArgs e)
        {
            MusicPlayer.Position = new TimeSpan(0);
            MusicPlayer.Play();
        }

        public void Play(object sender, string type)
        {
            // Play a sound depending on the sender type
            switch (type)
            {
                // Handles machinegun code
                case "machinegun":
                    MachineGunPlayers[CurrentSoundCounts[0]].IsMuted = Muted;
                    MachineGunPlayers[CurrentSoundCounts[0]].Play();
                    if (CurrentSoundCounts[0] < AllowedSoundInstances - 1) 
                    {
                        MachineGunPlayers[CurrentSoundCounts[0] + 1].Position = new TimeSpan(0);
                        CurrentSoundCounts[0]++; 
                    }
                    else { CurrentSoundCounts[0] = 0; MachineGunPlayers[0].Position = new TimeSpan(0); }
                    break;

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
            switch (type)
            {
                case MusicType.MainMenu:
                    MusicPlayer.Open(new Uri("..\\..\\Resources\\bensound-evolution.wav", UriKind.Relative)); // .wav file from bensound.com
                    break;

                case MusicType.DifficultyMenu:
                    MusicPlayer.Open(new Uri("..\\..\\Resources\\Actionable.wav", UriKind.Relative)); // .wav file from bensound.com
                    break;

                case MusicType.Game:
                    MusicPlayer.Open(new Uri("..\\..\\Resources\\bensound-epic.wav", UriKind.Relative)); // .wav file from from bensound.com
                    break;
            }
            MusicPlayer.Pause();
            MusicPlayer.Volume = 0.18;
            MusicPlayer.IsMuted = Muted;
            MusicPlayer.Position = new TimeSpan(0);
            MusicPlayer.Play();
        }
    }
}