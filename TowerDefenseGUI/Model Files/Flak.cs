using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Flak : Turret
    {
        public override string Serialize()
        {
            string flak = string.Format("{0},{1},{2},{3},{4}", "flak", xPos, yPos, imageIndex, upgradeLvl);
            return flak;
        }
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            xPos = Convert.ToInt32(finfo[1]);
            yPos = Convert.ToInt32(finfo[2]);          
            imageIndex = Convert.ToInt32(finfo[3]);
            return this;
        }
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
    }
}
