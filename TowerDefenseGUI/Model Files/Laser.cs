// This file contains the Laser class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    // The Laser class contains an override method
    // for Attack and a factory method.
    // Also implements serialization methods.
    class Laser : Turret
    {
        // serializes the object into a string of values and returns it.
        public override string Serialize()
        {
            string laser = string.Format("{0},{1},{2},{3},{4}", "laser", xPos, yPos, imageIndex, upgradeLvl);
            return laser;
        }

        // deserializes a string and converts it into an object with the specified values within that string.
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            xPos = Convert.ToInt32(finfo[1]);
            yPos = Convert.ToInt32(finfo[2]);
            imageIndex = Convert.ToInt32(finfo[3]);
            upgradeLvl = Convert.ToInt32(finfo[4]);
            type = "laser";
            return this;
        }

        // takes the x and y coordinates of its gui counterpart as well as
        // the gui's list index and returns a default laser object.
        public static Laser MakeLaser(double x, double y, int index)
        {
            Laser l = new Laser();
            l.xPos = x;
            l.yPos = y;
            l.imageIndex = index;
            l.imageID = 2;
            l.fireRate = 10;
            l.cost = 200;
            l.damage = 5;
            l.upCost = Convert.ToInt32(l.cost / 2);
            l.range = 175;
            l.type = "laser";
            return l;
        }

        // takes a list of enemy objects, checks to see if it is in range,
        // checks to see if it is a ground or air unit, and then deals damage
        // to it at a set interval. Calls parent attack method.
        public override void Attack(List<Enemy> enemies)
        {
            base.Attack(enemies);
            var target = DetectEnemy(enemies);
            if (target == null)
            {
                return;
            }
            if (firstShot)
            {
                firstShot = false;
                fireTime = 10;
            }
            if (fireTime % fireRate == 0)
            {
                target.TakeDamage(damage);
            }
            ++fireTime;
        }
    }
}
