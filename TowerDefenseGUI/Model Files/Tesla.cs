using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Tesla : Turret
    {
        public override string Serialize()
        {
            string tesla = string.Format("{0},{1},{2}", "tesla", xPos, yPos);
            return tesla;
        }
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            Tesla t = MakeTesla();
            t.xPos = Convert.ToInt32(finfo[1]);
            t.yPos = Convert.ToInt32(finfo[2]);
            return t;
        }
        public static Tesla MakeTesla()
        {
            Tesla t = new Tesla();
            t.cost = 175;
            t.damage = 3;
            t.range = 100;
            t.type = "tesla";
            return t;
        }
    }
}
