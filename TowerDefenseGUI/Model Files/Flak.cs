using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Flak : Turret
    {
        public override string Serialize()
        {
            return "";
        }
        public override object Deserialize(string info)
        {
            return new Flak();
        }
        public static Flak MakeFlak()
        {
            Flak f = new Flak();
            
            return f;
        }
    }
}
