using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Infantry : Enemy
    {
        public override string Serialize()
        {
            return "";
        }
        public override object Deserialize(string info)
        {
            return new Infantry();
        }
        public static Infantry MakeInfantry()
        {
            Infantry i = new Infantry();

            return i;
        }
    }
}
