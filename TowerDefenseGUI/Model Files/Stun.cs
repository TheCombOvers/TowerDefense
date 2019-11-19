using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Stun : Turret, ISerializeObject
    {
        public string Serialize()
        {
            return "";
        }
        public object Deserialize(string info)
        {
            return new Vehicle();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
