// This file contains the Turret class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TowerDefenseGUI
{
    // The turret class contains the values for the turret subclasses
    // and base attack enemy and detect enemy methods. Also contains, the upgrade method.
    // It also contains abstract serialization methods.
    public abstract class Turret : ISerializeObject
    {
        public int cost; // the cost of the turret
        public double damage; // the damage value of the turret
        public double range; // the range value of the turret
        public string type; // the type of the turret
        public double fireRate; // the firerate value of the turret
        public double fireTime; // the reload time of the turret
        public bool firstShot = true; // the starting shot boolean of the turret
        public double xPos; // the x position of the turret
        public double yPos; // the y position of the turret
        public int imageID; // the image index for the type of turret
        public int imageIndex; // the index of the turret's gui twin
        public int upgradeLvl = 1; // the current upgrade level of the turret
        public int upCost; // the upgrade cost of the turret
        public static event EventHandler<int> RotateTurret; // the gui rotate turret event
        public static event EventHandler<string> PlaySound; // the gui sound event for turret firing
        public static Action<string, int, bool> ChangeImage; // the gui event that animates turret firing

        // searches for an enemy in range and points to it.
        // plays sound and animates muzzle flash when firing.
        public virtual void Attack(List<Enemy> enemies)
        {
            Enemy e = DetectEnemy(enemies);
            if (e == null)
            {
                ChangeImage(type, imageIndex, false);
            }
            else
            {
                int deg = CalculateRotation(xPos, yPos, e.posX, e.posY);
                RotateTurret(this, deg);
                if (fireTime % fireRate == 0)
                {
                    PlaySound(this, type);
                    ChangeImage(type, imageIndex, true);
                }
                else
                {
                    ChangeImage(type, imageIndex, false);
                }
            }
        }

        // calculates the distance between the turret and the enemies.
        // if the enemy is within range and furthest along the path, then it returns that enemy.
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

        // calculates the distance between two x and y coordinates and returns it. 
        public double CalculateDistance(double xPos, double yPos, double posX, double posY)
        {
            double x = xPos - posX;
            double y = yPos - posY;
            double dist = Math.Sqrt((y * y) + (x * x));
            return dist;
        }

        // calculates the angle between two x and y coordinates and returns it.
        public int CalculateRotation(double xPos, double yPos, double posX, double posY)
        {
            int degree = 0;
            degree = (int) (Math.Atan2((posY- yPos), (posX- xPos)) * 180 / Math.PI)+90;
            return degree;
        }

        // increases the turret's damage and upgrade cost values.
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

        // method definition of parent serialize
        public abstract string Serialize();
        // method definition of parent deserialize
        public abstract object Deserialize(string info);

    }
}
