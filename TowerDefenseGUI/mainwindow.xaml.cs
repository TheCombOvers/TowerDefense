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

namespace TowerDefenseGUI
{
    /// <summary>
    /// Interaction logic for mainwindow.xaml
    /// </summary>
    public partial class mainwindow : Window
    {
        SoundHandler soundHandler = new SoundHandler();

        public mainwindow()
        {
            InitializeComponent();
            Task.Run(() => StartSplashScreen());
        }

        private void StartSplashScreen()
        {
            Thread.Sleep(3000);
            /*for (int i = 0; i < SoundHandler.AllowedSoundInstances; i++)
            {

                Dispatcher.Invoke(() =>
                {
                    
                });

            }*/
            Dispatcher.Invoke(() => EndSplashScreen());
        }

        public void EndSplashScreen()
        {
            MainMenu main = new MainMenu(soundHandler);
            main.Show();
            this.Close();
        }
    }
}
