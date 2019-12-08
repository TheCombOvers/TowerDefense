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
            string machinegun = string.Format("{0},{1},{2},{3}", "machinegun", xPos, yPos, imageIndex);
            return machinegun;
        }
        public override object Deserialize(string info)
        {// need to make all turret desearilization look like this one...
            string[] finfo = info.Split(',');
            xPos = Convert.ToInt32(finfo[1]);
            yPos = Convert.ToInt32(finfo[2]);   
            imageIndex = Convert.ToInt32(finfo[3]);
            type = "machinegun";
            return this;
        }
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
    }
}
