// This file contains the Tesla class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    // The Tesla class contains an override method
    // for Attack, DetectEnemy, and a factory method.
    // Also implements serialization methods.
    class Tesla : Turret
    {
        // serializes the object into a string of values and returns it.
        public override string Serialize()
        {
            string tesla = string.Format("{0},{1},{2},{3},{4}", "tesla", xPos, yPos, imageIndex, upgradeLvl);
            return tesla;
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
        // the gui's list index and returns a default tesla object.
        public static Tesla MakeTesla(double x, double y, int index)
        {
            Tesla t = new Tesla();
            t.xPos = x;
            t.yPos = y;
            t.imageID = 5;
            t.imageIndex = index;
            t.fireRate = 5;
            t.cost = 175;
            t.upCost = Convert.ToInt32(t.cost / 2);
            t.damage = 1;
            t.range = 100;
            t.type = "tesla";
            return t;
        }

        // takes a list of enemy objects, checks to see if any are in range,
        // checks to see if they are ground or air units, and then deals damage
        // to all of them at a set interval. Calls parent attack method.
        public override void Attack(List<Enemy> enemies)
        {
            var targets = DetectEnemies(enemies);
            base.Attack(targets);
            if (targets.Count == 0)
            {
                return;
            }
            if (firstShot)
            {
                firstShot = false;
                fireTime = 5;
            }
            if (fireTime % fireRate == 0)
            {
                foreach (Enemy e in targets)
                {
                    e.TakeDamage(damage);
                }
            }
            ++fireTime;
        }

        // calculates the distance between the turret and the enemies.
        // if the enemy is within range, then it adds the enemy to a list
        // and returns the list.
        public List<Enemy> DetectEnemies(List<Enemy> enemies)
        {
            List<Enemy> targets = new List<Enemy>();
            foreach (Enemy e in enemies)
            {
                double dist = CalculateDistance(xPos, yPos, e.posX, e.posY);
                if (range >= dist)
                {
                    if (!e.type.Contains("aircraft") && e.type != "aboss")
                    {
                        targets.Add(e);
                    }
                }
            }
            return targets;
        }
    }
}
