using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TowerDefenseLibrary
{
    abstract class Enemy
    {
        Image image;
        int rewardMoney;
        int rewardScore;
        double health;
        double speed;
        string type;
        double pathProgress;
        double posX;
        double posY;
        Timer updatePosTimer;

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
