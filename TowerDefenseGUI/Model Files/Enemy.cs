using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Controls;

namespace TowerDefenseGUI
{
    public abstract class Enemy
    {
        Image image;
        public int rewardMoney;
        public int rewardScore;
        public double health;
        public double speed;
        public string type;
        public double pathProgress;
        public double posX;
        public double posY;
        public double updatePosRate;

        public void UpdatePos()
        {

        }
        public void CheckCoords(List<int[]> path)
        {

        }
        public void Pause(Timer timer)
        {
            timer.Stop();
        }

        public void TakeDamage(double amount)
        {

        }
    }
}
