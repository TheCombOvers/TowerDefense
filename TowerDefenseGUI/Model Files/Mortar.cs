using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Mortar : Turret
    {        
        // serializes the object into a string of values and returns it.
        public override string Serialize()
        {
            string mortar = string.Format("{0},{1},{2},{3},{4}", "mortar", xPos, yPos, imageIndex, upgradeLvl);
            return mortar;
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
        // the gui's list index and returns a default mortar object.
        public static Mortar MakeMortar(double x, double y, int index)
        {
            Mortar m = new Mortar();
            m.xPos = x;
            m.yPos = y;
            m.imageIndex = index;
            m.imageID = 3;
            m.fireRate = 300;
            m.cost = 150;
            m.upCost = Convert.ToInt32(m.cost / 2);
            m.damage = 25;
            m.range = 375;
            m.type = "mortar";
            return m;
        }

        // takes a list of enemy objects, checks to see if it is in range,
        // checks to see if it is a ground or air unit, and then deals damage
        // to enemies within a small range at a set interval. Calls parent attack method.
        public override void Attack(List<Enemy> enemies)
        {
            var target = DetectEnemy(enemies);
            if (target != null)
            {
                if (target.type.Contains("aircraft") || target.type == "aboss")
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
                fireTime = 300;
            }
            if (fireTime % fireRate == 0)
            {
                target.TakeDamage(damage);
                for (int i = 0; i < enemies.Count; i++)
                {
                    Enemy e = enemies[i];
                    var dist = CalculateDistance(target.posX, target.posY, e.posX, e.posY);
                    if (dist <= 100)
                    {
                        e.TakeDamage(10);
                    }
                }
            }
            ++fireTime;
        }
    }
}
