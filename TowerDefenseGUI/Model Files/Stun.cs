using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Stun : Turret
    {
        public override string Serialize()
        {
            return "";
        }
        public override object Deserialize(string info)
        {
            return new Vehicle();
        }
        public static Stun MakeStun()
        {
            Stun s = new Stun();

            return s;
        }
    }
}
