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
        public double fireTime;
        public bool firstShot = true;
        public double xPos;
        public double yPos;
        public int imageID;
        public int imageIndex;
        public int upgradeLvl = 1;
        public int upCost;
        public static event EventHandler<int> RotateTurret;
        public static event EventHandler<string> PlaySound;

        public virtual void Attack(List<Enemy> enemies)
        {
            Enemy e = DetectEnemy(enemies);
            if (e == null)
            {
                return;
            }
            else
            { 
                int deg = CalculateRotation(xPos, yPos, e.posX, e.posY);
                RotateTurret(this, deg);
                if (fireTime % fireRate == 0)
                {
                    PlaySound(this, type);
                }
            }
        }
        
        public virtual Enemy DetectEnemy(List<Enemy> enemies)
        {
            Enemy target = null;
            foreach (Enemy e in enemies)
            {
                double dist = CalculateDistance(xPos, yPos, e.posX, e.posY);
                if (range >= dist)
                {
                    if (target == null)
                    {
                        target = e;
                    }
                    else
                    {
                        if(e.pathProgress > target.pathProgress)
                        {
                            target = e;
                        }
                    }
                }
            }
            return target;
        }

        public double CalculateDistance(double xPos, double yPos, double posX, double posY)
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

        public void Upgrade()
        {
            switch (upgradeLvl)
            {
                case 1:
                    damage += (damage * .2); // increase by 20%
                    break;
                case 2:
                    damage += (damage * .3); // increase by 30%                                     
                    break;
                case 3:
                    damage += (damage * .4); // increase by 40%
                    break;
                default:
                    return;
            }
            upCost += Convert.ToInt32(upCost * .3);
            ++upgradeLvl;
            return;
        }
    }
}
