using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Mortar : Turret, ISerializeObject
    {
        public string Serialize()
        {
            return "";
        }
        public object Deserialize(string info)
        {
            return new Mortar();
        }
        public static Mortar MakeMortar()
        {
            Mortar mo = new Mortar();

            return mo;
        }
    }
}
