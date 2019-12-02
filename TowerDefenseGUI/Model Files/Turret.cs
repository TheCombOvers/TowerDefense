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
        public double xPos;
        public double yPos;

        public void Attack(Enemy e)
        {
            if (e == null)
            {
                return;
            }
            else
            {
                e.TakeDamage(damage);
            }
        }

        public Enemy DetectEnemy(List<Enemy> enemies)
        {
            Enemy target = null;
            foreach (Enemy e in enemies)
            {
                double dist = CalculateDistance(xPos, yPos, e.posX, e.posY);
                if (range >= dist)
                {
                    if (target == null)
                    {
                        return target;
                    }
                }
            }
            return target;
        }

        private double CalculateDistance(double xPos, double yPos, double posX, double posY)
        {
            double x = xPos - posX;
            double y = yPos - posY;
            double dist = Math.Sqrt((y * y) + (x * x));
            return dist;
        }

        public abstract string Serialize();
        public abstract object Deserialize(string info);
    }
}
