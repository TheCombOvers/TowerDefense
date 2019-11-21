using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TowerDefenseGUI
{
    public abstract class Turret : ISerializeObject
    {
        public Image image;
        public int cost;
        public double damage;
        public double range;
        public string type;
        public double fireRate;
        public int xPos;
        public int yPos;

        public void Attack(Enemy e)
        {
            e.health -= damage;
        }

        public void DetectEnemy(List<Enemy> enemies)
        {
            Enemy target = null;
            foreach (Enemy e in enemies)
            {
                double dist = CalculateDistance(xPos, yPos, e.posX, e.posY);
                if (range >= dist)
                {
                    //if (target != null)
                    //{
                    //    Attack(target);
                    //}
                    //else
                    //{
                    //    target = e;
                    //    Attack(target);
                    //}
                }
            }
        }

        private double CalculateDistance(int xPos, int yPos, double posX, double posY)
        {
            int x = xPos - (int)posX;
            int y = yPos - (int)posY;
            double dist = Math.Sqrt((y * y) + (x * x));
            return dist;
        }

        public abstract string Serialize();
        public abstract object Deserialize(string info);
    }
}
