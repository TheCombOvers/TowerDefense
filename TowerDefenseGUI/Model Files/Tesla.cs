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
            return "";
        }
        public override object Deserialize(string info)
        {
            return new Vehicle();
        }
        public static Tesla MakeTesla()
        {
            Tesla t = new Tesla();

            return t;
        }
    }
}
