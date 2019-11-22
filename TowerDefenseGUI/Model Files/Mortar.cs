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
            Mortar m = MakeMortar();
            m.xPos = Convert.ToInt32(finfo[1]);
            m.yPos = Convert.ToInt32(finfo[2]);
            return m;
        }
        public static Mortar MakeMortar()
        {
            Mortar m = new Mortar();
            m.cost = 150;
            m.damage = 50;
            m.range = 375;
            m.type = "mortar";
            return m;
        }
    }
}
