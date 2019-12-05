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
        public int cost;
        public double damage;
        public double range;
        public string type;
        public double fireRate;
        public double xPos;
        public double yPos;
        public int imageID;
        public int imageIndex;
        public static event EventHandler<int> RotateTurret;
        
        public virtual void Attack(Enemy e)
        {
            int fireTime = 0;
            if (e == null)
            {

                if (type == "mortar")
                {
                    fireRate = 300;
                }
                else if (type == "stun")
                {
                    fireRate = 120;
                }
                else
                {
                    fireRate = 60;
                }
                return;
            }
            else
            {
                if (type == "mortar")
                {
                    fireTime = 300;
                }
                else if (type == "stun")
                {
                    fireTime = 120;
                }
                else
                {
                    fireTime = 60;
                }
                int deg = CalculateRotation(xPos, yPos, e.posX, e.posY);
                RotateTurret(this, deg);
                Console.WriteLine("Firerate = " + fireRate);

                if (fireRate % fireTime == 0)
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
        public int CalculateRotation(double xPos, double yPos, double posX, double posY)
        {
            int degree = 0;
            degree = (int) (Math.Atan2((posY- yPos), (posX- xPos)) * 180 / Math.PI)+90;
            return degree;
        }

        public abstract string Serialize();
        public abstract object Deserialize(string info);
    }
}
