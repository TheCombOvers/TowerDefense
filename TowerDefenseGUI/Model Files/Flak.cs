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
            string flak = string.Format("{0},{1},{2}", "turret", xPos, yPos);
            return flak;
        }
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            Flak f = MakeFlak();
            f.xPos = Convert.ToInt32(finfo[1]);
            f.yPos = Convert.ToInt32(finfo[2]);
            f.type = "flak";           
            return f;
        }
        public static Flak MakeFlak()
        {
            Flak f = new Flak();
            f.cost = 75;
            f.damage = 2;
            f.range = 250/2;
            f.type = "flak";           
            return f;
        }
    }
}
