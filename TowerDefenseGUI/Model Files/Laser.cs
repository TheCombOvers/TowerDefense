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
            string laser = string.Format("{0},{1},{2}", "laser", xPos, yPos);
            return laser;
        }
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            xPos = Convert.ToInt32(finfo[1]);
            yPos = Convert.ToInt32(finfo[2]);
            type = "laser";
            return this;
        }

        public static Laser MakeLaser()
        {
            Laser l = new Laser();
            l.imageID = 2;
            l.cost = 125;
            l.damage = 10;
            l.range = 175;
            l.type = "laser";
            return l;
        }
    }
}
