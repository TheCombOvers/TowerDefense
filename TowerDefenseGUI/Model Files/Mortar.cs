using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Mortar : Turret
    {
        public override string Serialize()
        {
            string mortar = string.Format("{0},{1},{2},{3}", "mortar", xPos, yPos, imageIndex);
            return mortar;
        }
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            xPos = Convert.ToInt32(finfo[1]);
            yPos = Convert.ToInt32(finfo[2]);
            imageIndex = Convert.ToInt32(finfo[3]);
            return this;
        }
        public static Mortar MakeMortar(double x, double y, int index)
        {
            Mortar m = new Mortar();
            m.xPos = x;
            m.yPos = y;
            m.imageIndex = index;
            m.imageID = 3;
            m.fireRate = 300;
            m.cost = 150;
            m.upCost = Convert.ToInt32(m.cost / 2);
            m.damage = 25;
            m.range = 375;
            m.type = "mortar";
            return m;
        }

        public override void Attack(List<Enemy> enemies)
        {
            var target = DetectEnemy(enemies);
            if (target != null)
            {
                if (target.type.Contains("aircraft") || target.type == "aboss")
                {
                    return;
                }
            }
            else
            {
                return;
            }
            base.Attack(enemies);
            if (firstShot)
            {
                firstShot = false;
                fireTime = 300;
            }
            if (fireTime % fireRate == 0)
            {
                target.TakeDamage(damage);
                for (int i = 0; i < enemies.Count; i++)
                {
                    Enemy e = enemies[i];
                    var dist = CalculateDistance(target.posX, target.posY, e.posX, e.posY);
                    if (dist <= 100)
                    {
                        e.TakeDamage(10);
                    }
                }
            }
            ++fireTime;
        }
    }
}
