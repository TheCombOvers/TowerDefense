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
        public double fireRate = 60;
        public double xPos;
        public double yPos;
        public event EventHandler<int> RotateTurret;

        public void Attack(Enemy e)
        {
            if (e == null)
            {
                fireRate = 60;
                return;
            }
            else
            {
                Console.WriteLine("Firerate = " + fireRate);
                if (fireRate % 60 == 0)
                {
                    Console.WriteLine("Attacking");
                    e.TakeDamage(damage);
                }
                fireRate++;
            }
        }

        public Enemy DetectEnemy(List<Enemy> enemies)
        {
            Enemy target = null;
            foreach (Enemy e in enemies)
            {
                double dist = CalculateDistance(xPos, yPos, e.posX, e.posY);
                int deg = 90;
                RotateTurret(this, deg);
                if (range >= dist)
                {
                    Console.WriteLine("Target in range");
                    target = e;
                    return target;
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
