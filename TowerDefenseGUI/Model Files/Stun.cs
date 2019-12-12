// This file contains the Stun class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    // The Stun class contains an override method
    // for Attack and a factory method.
    // Also implements serialization methods.
    class Stun : Turret
    {
        // serializes the object into a string of values and returns it.
        public override string Serialize()
        {
            string stun = string.Format("{0},{1},{2},{3},{4}", "stun", xPos, yPos, imageIndex, upgradeLvl);
            return stun;
        }

        // deserializes a string and converts it into an object with the specified values within that string.
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            xPos = Convert.ToInt32(finfo[1]);
            yPos = Convert.ToInt32(finfo[2]);
            imageIndex = Convert.ToInt32(finfo[3]);
            upgradeLvl = Convert.ToInt32(finfo[4]);
            return this;
        }

        // takes the x and y coordinates of its gui counterpart as well as
        // the gui's list index and returns a default stun object.
        public static Stun MakeStun(double x, double y, int index)
        {
            Stun s = new Stun();
            s.xPos = x;
            s.yPos = y;
            s.imageID = 4;
            s.imageIndex = index;
            s.fireRate = 90;
            s.cost = 200;
            s.upCost = Convert.ToInt32(s.cost / 2);
            s.damage = 15;
            s.range = 200;
            s.type = "stun";
            return s;
        }

        // takes a list of enemy objects, checks to see if it is in range,
        // checks to see if it is a ground or air unit, and then deals damage to it
        // as well as sets the enemy's stun value at a set interval.
        // Calls parent attack method.
        public override void Attack(List<Enemy> enemies)
        {
            base.Attack(enemies);
            Enemy e = DetectEnemy(enemies);
            if (e == null)
            {
                return;
            }
            if (firstShot)
            {
                fireTime = 90;
                firstShot = false;
            }
            if (fireTime % fireRate == 0)
            {
                e.stunned = 30;
                e.TakeDamage(damage);
            }
            ++fireTime;
        }
    }
}
