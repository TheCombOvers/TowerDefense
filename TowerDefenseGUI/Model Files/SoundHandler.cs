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
                currentlyLoading = "FlakSound.wav";
                FlakSound = new SoundPlayer(@"..\..\Resources\FlakSound.wav");
                currentlyLoading = "MortarSound.wav";
                MortarSound = new SoundPlayer(@"..\..\Resources\MortarSound.wav");
                currentlyLoading = "TeslaSound.wav";
                TeslaSound = new SoundPlayer(@"..\..\Resources\TeslaSound.wav");
                currentlyLoading = "LaserSound.wav";
                LaserSound = new SoundPlayer(@"..\..\Resources\LaserSound.wav");
                currentlyLoading = "StunSound.wav";
                StunSound = new SoundPlayer(@"..\..\Resources\StunSound.wav");
            }
            catch (Exception e) { MessageBox.Show("Could not locate " + currentlyLoading + "\nStack Trace: " + e.Message); }
        }

        public void Play(object sender, string type)
        {
            switch(type)
            {
                case "machinegun":
                    try { MachineGunSound.Play(); }
                    catch(Exception e) { MessageBox.Show("MachineGunSound failed to play.\nStack Trace: " + e.Message); }
                    break;
                case "flak":
                    try { FlakSound.Play(); }
                    catch (Exception e) { MessageBox.Show("FlakSound failed to play.\nStack Trace: " + e.Message); }
                    break;
                case "mortar":
                    try { MortarSound.Play(); }
                    catch (Exception e) { MessageBox.Show("MortarSound failed to play.\nStack Trace: " + e.Message); }
                    break;
                case "tesla":
                    try { TeslaSound.Play(); }
                    catch (Exception e) { MessageBox.Show("TeslaSound failed to play.\nStack Trace: " + e.Message); }
                    break;
                case "laser":
                    try { LaserSound.Play(); }
                    catch (Exception e) { MessageBox.Show("LaserSound failed to play.\nStack Trace: " + e.Message); }
                    break;
                case "stun":
                    try { StunSound.Play(); }
                    catch (Exception e) { MessageBox.Show("StunSound failed to play.\nStack Trace: " + e.Message); }
                    break;
            }
        }
    }
}
