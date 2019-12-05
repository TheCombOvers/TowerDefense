using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Stun : Turret
    {
        public override string Serialize()
        {
            string stun = string.Format("{0},{1},{2}", "stun", xPos, yPos);
            return stun;
        }
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            xPos = Convert.ToInt32(finfo[1]);
            yPos = Convert.ToInt32(finfo[2]);
            return this;
        }
        public static Stun MakeStun(int x, int y)
        {
            Stun s = new Stun();
            s.xPos = x;
            s.yPos = y;
            s.imageID = 4;
            s.cost = 200;
            s.damage = 15;
            s.range = 200;
            s.type = "stun";
            return s;
        }
    }
}
