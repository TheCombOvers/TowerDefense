using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Laser : Turret
    {
        public override string Serialize()
        {
            string laser = string.Format("{0},{1},{2},{3},{4}", "laser", xPos, yPos, imageIndex, upgradeLvl);
            return laser;
        }
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            xPos = Convert.ToInt32(finfo[1]);
            yPos = Convert.ToInt32(finfo[2]);
            imageIndex = Convert.ToInt32(finfo[3]);
            type = "laser";
            return this;
        }

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
    }
}
