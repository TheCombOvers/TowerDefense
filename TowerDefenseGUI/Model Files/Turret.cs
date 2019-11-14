using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TowerDefenseGUI
{
    public interface ISerializeTurret
    {
        string Serialize(string type, double x, double y);
        Turret Deserialize(string info);
    }
    public abstract class Turret
    {
        public Image image;
        public int cost;
        public double damage;
        public string type;
        public Timer fireRate;
        public int xPos;
        public int yPos;

        public abstract void Initialize();
       
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
