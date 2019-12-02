using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class MachineGun : Turret
    {
        public override string Serialize()
        {
            string machinegun = string.Format("{0},{1},{2}", "machinegun", xPos, yPos);
            return machinegun;
        }
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            MachineGun m = MakeMachineGun(Convert.ToInt32(finfo[1]), Convert.ToInt32(finfo[2]));
            m.type = "machinegun";
            return m;
        }
        public static MachineGun MakeMachineGun(double x, double y)
        {
            MachineGun m = new MachineGun();
            m.xPos = x;
            m.yPos = y;
            m.cost = 50;
            m.damage = 4;
            m.range = 125;
            m.type = "machinegun";
            return m;
        }
    }
}
