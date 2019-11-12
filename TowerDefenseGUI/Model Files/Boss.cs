using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Boss : Enemy, ISerializeEnemy
    {
        public string Serialize(string type, double x, double y, double pathProg, double hp)
        {
            return "";
        }
        public Enemy Deserialize()
        {
            return new Infantry();
        }
    }
}
