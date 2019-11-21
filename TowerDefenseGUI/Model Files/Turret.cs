using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TowerDefenseGUI
{
    public abstract class Turret: ISerializeObject
    {
        public Image image;
        public int cost;
        public double damage;
        public string type;
        public double fireRate;
        public int xPos;
        public int yPos;
       
        public void Attack(Enemy e)
        {

        }

        public Enemy DetectEnemy(List<Enemy> enemies)
        {
            Enemy e = null;

            return e;
        }
        public abstract string Serialize();
        public abstract object Deserialize(string info);
    }
}
