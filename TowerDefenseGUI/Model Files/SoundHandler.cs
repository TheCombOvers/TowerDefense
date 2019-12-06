using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace TowerDefenseGUI
{
    public class SoundHandler
    {
        public SoundPlayer MachineGunSound = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\MachineGunSound.wav" };
        public SoundPlayer FlakSound = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\FlakSound.wav" };
        public SoundPlayer MortarSound = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\MortarSound.wav" };
        public SoundPlayer TeslaSound = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\TeslaSound.wav" };
        public SoundPlayer LaserSound = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\LaserSound.wav" };
        public SoundPlayer StunSound = new SoundPlayer() { SoundLocation = "..\\..\\Resources\\StunSound.wav" };
        //private SoundPlayer

        public SoundHandler()
        {
            MachineGunSound.Load();
            FlakSound.Load();
            MortarSound.Load();
            TeslaSound.Load();
            LaserSound.Load();
            StunSound.Load();
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
                    FlakSound.Play();
                    //catch { /*MessageBox.Show("FlakSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "mortar":
                    MortarSound.Play();
                    //catch { /*MessageBox.Show("MortarSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "tesla":
                    TeslaSound.Play();
                    //catch { /*MessageBox.Show("TeslaSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "laser":
                    LaserSound.Play();
                    //catch { /*MessageBox.Show("LaserSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
                case "stun":
                    StunSound.Play();
                    //catch { /*MessageBox.Show("StunSound failed to play.\nStack Trace: " + e.Message);*/ }
                    break;
            }
        }
    }
}
