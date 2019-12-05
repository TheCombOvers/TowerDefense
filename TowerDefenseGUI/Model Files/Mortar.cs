using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Mortar : Turret
    {
        public override string Serialize()
        {
            string mortar = string.Format("{0},{1},{2}", "mortar", xPos, yPos);
            return mortar;
        }
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            xPos = Convert.ToInt32(finfo[1]);
            yPos = Convert.ToInt32(finfo[2]);
            return this;
        }
        public static Mortar MakeMortar(double x, double y)
        {
            Mortar m = new Mortar();
            m.xPos = x;
            m.yPos = y;
            m.imageID = 3;
            m.cost = 150;
            m.damage = 50;
            m.range = 375;
            m.type = "mortar";
            return m;
        }
    }
}
