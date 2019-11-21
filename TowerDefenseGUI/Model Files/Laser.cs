using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Laser : Turret
    {
        public override string Serialize()
        {
            return "";
        }
        public override object Deserialize(string info)
        {
            return new Laser();
        }

        public static Laser MakeLaser()
        {
            Laser l = new Laser();

            return l;
        }
    }
}
