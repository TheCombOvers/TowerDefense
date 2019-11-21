using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Boss : Enemy
    {
        public override string Serialize()
        {
            return "";
        }
        public override object Deserialize(string info)
        {
            return new Boss();
        }

        public static Boss MakeBoss()
        {
            Boss b = new Boss();
           
            return b;
        }
    }
}
