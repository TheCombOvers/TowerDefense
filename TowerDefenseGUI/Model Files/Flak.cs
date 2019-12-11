using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Flak : Turret
    {
        // serializes the object into a string of values and returns it.
        public override string Serialize()
        {
            string flak = string.Format("{0},{1},{2},{3},{4}", "flak", xPos, yPos, imageIndex, upgradeLvl);
            return flak;
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
        // the gui's list index and returns a default flak object.
        public static Flak MakeFlak(double x, double y, int index)
        {
            Flak f = new Flak();
            f.xPos = x;
            f.yPos = y;
            f.imageIndex = index;
            f.imageID = 1;
            f.fireRate = 65;
            f.cost = 75;
            f.upCost = Convert.ToInt32(f.cost / 2);
            f.damage = 7;
            f.range = 250;
            f.type = "flak";
            return f;
        }

        // takes a list of enemy objects, checks to see if it is in range,
        // checks to see if it is a ground or air unit, and then deals damage
        // to it at a set interval. Calls parent attack method.
        public override void Attack(List<Enemy> enemies)
        {
            var target = DetectEnemy(enemies);
            if (target != null)
            {
                if (target.type.Contains("infantry") || target.type.Contains("vehicle") || target.type == "gboss")
                {
                    return;
                }
            }
            else
            {
                return;
            }
            base.Attack(enemies);
            if (firstShot)
            {
                firstShot = false;
                fireTime = 60;
            }
            if (fireTime % fireRate == 0)
            {
                target.TakeDamage(damage);
            }
            ++fireTime;
        }
    }
}
