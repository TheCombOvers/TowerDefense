using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Laser : Turret, ISerializeTurret
    {
        // params may not even be nessacary considering all the information needed is accessable within the method
        public string Serialize(string type, double x, double y)
        {
            return "";
        }
        public Turret Deserialize(string info)
        {
            return new Laser();
        }
    }
}
