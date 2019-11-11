using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TowerDefenseLibrary
{
    abstract class Turret
    {
        Image image;
        int cost;
        double damage;
        string type;
        Timer fireRate;

        public void Attack(Enemy e)
        {

        }

        public Enemy DetectEnemy(List<Enemy> enemies)
        {
            Enemy e = null;

            return e;
        }
    }
}
