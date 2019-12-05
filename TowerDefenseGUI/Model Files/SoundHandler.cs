using System;
using System.Media;
using System.Windows;

namespace TowerDefenseGUI
{
    public class SoundHandler
    {
        private SoundPlayer MachineGunSound;
        private SoundPlayer FlakSound;
        private SoundPlayer MortarSound;
        private SoundPlayer TeslaSound;
        private SoundPlayer LaserSound;
        private SoundPlayer StunSound;
        //private SoundPlayer

        public SoundHandler()
        {
            string currentlyLoading = null;
            try
            {
                currentlyLoading = "MachineGunSound.wav";
                MachineGunSound = new SoundPlayer(@"..\..\Resources\MachineGunSound.wav");
                MachineGunSound.Load();
                currentlyLoading = "FlakSound.wav";
                FlakSound = new SoundPlayer(@"..\..\Resources\FlakSound.wav");
                FlakSound.Load();
                currentlyLoading = "MortarSound.wav";
                MortarSound = new SoundPlayer(@"..\..\Resources\MortarSound.wav");
                MortarSound.Load();
                currentlyLoading = "TeslaSound.wav";
                TeslaSound = new SoundPlayer(@"..\..\Resources\TeslaSound.wav");
                TeslaSound.Load();
                currentlyLoading = "LaserSound.wav";
                LaserSound = new SoundPlayer(@"..\..\Resources\LaserSound.wav");
                LaserSound.Load();
                currentlyLoading = "StunSound.wav";
                StunSound = new SoundPlayer(@"..\..\Resources\StunSound.wav");
                StunSound.Load();
            }
            catch (Exception e) { MessageBox.Show("Could not locate " + currentlyLoading + "\nStack Trace: " + e.Message); }
        }

        public void Play(object sender, string type)
        {
            switch(type)
            {
                case "machinegun":
                    try { MachineGunSound.Play(); }
                    catch(Exception e) { /*MessageBox.Show("MachineGunSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "flak":
                    try { FlakSound.Play(); }
                    catch (Exception e) { /*MessageBox.Show("FlakSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "mortar":
                    try { MortarSound.Play(); }
                    catch (Exception e) { /*MessageBox.Show("MortarSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "tesla":
                    try { TeslaSound.Play(); }
                    catch (Exception e) { /*MessageBox.Show("TeslaSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "laser":
                    try { LaserSound.Play(); }
                    catch (Exception e) { /*MessageBox.Show("LaserSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "stun":
                    try { StunSound.Play(); }
                    catch (Exception e) { /*MessageBox.Show("StunSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
            }
        }
    }
}
