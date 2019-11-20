using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Aircraft : Enemy, ISerializeObject
    {
        public string Serialize()
        {
            return "";
        }
        public object Deserialize(string info)
        {
            return new Aircraft();
        }
        public static Aircraft MakeAircraft()
        {
            Aircraft a = new Aircraft();

            return a;
        }
    }
}
