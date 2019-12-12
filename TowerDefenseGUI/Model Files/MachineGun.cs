// This file contains the Machine gun class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    // The Machinegun class contains an override method
    // for Attack and a factory method.
    // Also implements serialization methods.
    class MachineGun : Turret
    {
        // serializes the object into a string of values and returns it.
        public override string Serialize()
        {
            string machinegun = string.Format("{0},{1},{2},{3},{4}", "machinegun", xPos, yPos, imageIndex, upgradeLvl);
            return machinegun;
        }

        // deserializes a string and converts it into an object with the specified values within that string.
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            xPos = Convert.ToInt32(finfo[1]);
            yPos = Convert.ToInt32(finfo[2]);
            imageIndex = Convert.ToInt32(finfo[3]);
            upgradeLvl = Convert.ToInt32(finfo[4]);
            type = "machinegun";
            return this;
        }

        // takes the x and y coordinates of its gui counterpart as well as
        // the gui's list index and returns a default machinegun object.
        public static MachineGun MakeMachineGun(double x, double y, int index)
        {
            MachineGun m = new MachineGun();

            m.imageIndex = index;
            m.imageID = 0;
            m.fireRate = 10;
            m.xPos = x;
            m.yPos = y;
            m.cost = 50;
            m.upCost = Convert.ToInt32(m.cost / 2);
            m.damage = 2;
            m.range = 125;
            m.type = "machinegun";
            return m;
        }

        // takes a list of enemy objects, checks to see if it is in range,
        // checks to see if it is a ground or air unit, and then deals damage
        // to it at a set interval. Calls parent attack method.
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
