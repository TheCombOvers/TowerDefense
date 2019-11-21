using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class MachineGun : Turret, ISerializeObject
    {
        public string Serialize()
        {
            return "";
        }
        public object Deserialize(string info)
        {
            return new MachineGun();
        }
        public static MachineGun MakeMachineGun()
        {
            MachineGun m = new MachineGun();

            return m;
        }
    }
}
