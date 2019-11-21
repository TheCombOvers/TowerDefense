using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Vehicle : Enemy
    {
        public override string Serialize()
        {
            return "";
        }
        public override object Deserialize(string info)
        {
            return new Vehicle();
        }
        public static Vehicle MakeVehicle()
        {
            Vehicle v = new Vehicle();

            return v;
        }
    }
}
