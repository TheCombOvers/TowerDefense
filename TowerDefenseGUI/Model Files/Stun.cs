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
            string stun = string.Format("{0},{1},{2},{3},{4}", "stun", xPos, yPos, imageIndex, upgradeLvl);
            return stun;
        }
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            xPos = Convert.ToInt32(finfo[1]);
            yPos = Convert.ToInt32(finfo[2]);
            imageIndex = Convert.ToInt32(finfo[3]);
            upgradeLvl = Convert.ToInt32(finfo[4]);
            return this;
        }
        public static Stun MakeStun(double x, double y, int index)
        {
            Stun s = new Stun();
            s.xPos = x;
            s.yPos = y;
            s.imageID = 4;
            s.imageIndex = index;
            s.fireRate = 90;
            s.cost = 200;
            s.upCost = Convert.ToInt32(s.cost / 2);
            s.damage = 15;
            s.range = 200;
            s.type = "stun";
            return s;
        }

        public override void Attack(List<Enemy> enemies)
        {
            base.Attack(enemies);
            Enemy e = DetectEnemy(enemies);
            if (e == null)
            {
                return;
            }
            if (firstShot)
            {
                fireTime = 90;
                firstShot = false;
            }
            if (fireTime % fireRate == 0)
            {
                e.stunned = 30;
                e.TakeDamage(damage);
            }
            ++fireTime;
        }
    }
}
