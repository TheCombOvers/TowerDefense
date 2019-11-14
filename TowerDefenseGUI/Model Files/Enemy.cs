using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Controls;

namespace TowerDefenseGUI
{
    public interface ISerializeEnemy 
    {
        string Serialize(string type, double x, double y, double pathProg, double hp);
        Enemy Deserialize();
    }
   

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
        Timer updatePosTimer;

        public abstract void Initialize();

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
