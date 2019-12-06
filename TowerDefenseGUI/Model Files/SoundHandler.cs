using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace TowerDefenseGUI
{
    public class SoundHandler
    {
        public SoundPlayer MachineGunSound = new SoundPlayer("..\\..\\Resources\\MachineGunSound.wav");
        public SoundPlayer FlakSound = new SoundPlayer("..\\..\\Resources\\FlakSound.wav");
        public SoundPlayer MortarSound = new SoundPlayer("..\\..\\Resources\\MortarSound.wav");
        public SoundPlayer TeslaSound = new SoundPlayer("..\\..\\Resources\\TeslaSound.wav");
        public SoundPlayer LaserSound = new SoundPlayer("..\\..\\Resources\\LaserSound.wav");
        public SoundPlayer StunSound = new SoundPlayer("..\\..\\Resources\\StunSound.wav");
        //private SoundPlayer

        public SoundHandler()
        {
            if (!MachineGunSound.IsLoadCompleted) { MachineGunSound.Load(); }
            /*if (!FlakSound.IsLoadCompleted) { FlakSound.Load(); }
            if (!MortarSound.IsLoadCompleted) { MortarSound.Load(); }
            if (!TeslaSound.IsLoadCompleted) { TeslaSound.Load(); }
            if (!LaserSound.IsLoadCompleted) { LaserSound.Load(); }
            if (!LaserSound.IsLoadCompleted) { StunSound.Load(); }*/
        }

        public void Play(object sender, string type)
        {
            switch (type)
            {
                case "machinegun":
                    MachineGunSound.Play();
                    //catch { MessageBox.Show("MachineGunSound failed to play.\nStack Trace: " + e.Message); }
                    break;
                case "flak":
                    try { FlakSound.Play(); }
                    catch { /*MessageBox.Show("FlakSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "mortar":
                    try { MortarSound.Play(); }
                    catch { /*MessageBox.Show("MortarSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "tesla":
                    try { TeslaSound.Play(); }
                    catch { /*MessageBox.Show("TeslaSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "laser":
                    try { LaserSound.Play(); }
                    catch { /*MessageBox.Show("LaserSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "stun":
                    try { StunSound.Play(); }
                    catch { /*MessageBox.Show("StunSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
            }
        }
    }
}
