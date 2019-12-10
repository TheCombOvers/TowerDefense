using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Tesla : Turret
    {
        public override string Serialize()
        {
            string tesla = string.Format("{0},{1},{2},{3},{4}", "tesla", xPos, yPos, imageIndex, upgradeLvl);
            return tesla;
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
        public static Tesla MakeTesla(double x, double y, int index)
        {
            Tesla t = new Tesla();
            t.xPos = x;
            t.yPos = y;
            t.imageID = 5;
            t.imageIndex = index;
            t.fireRate = 5;
            t.cost = 175;
            t.upCost = Convert.ToInt32(t.cost / 2);
            t.damage = 1;
            t.range = 100;
            t.type = "tesla";
            return t;
        }

        public override void Attack(List<Enemy> enemies)
        {
            var targets = DetectEnemies(enemies);
            base.Attack(enemies);
            if (targets.Count == 0)
            {
                return;
            }
            if (firstShot)
            {
                firstShot = false;
                fireTime = 5;
            }
            if (fireTime % fireRate == 0)
            {
                foreach (Enemy e in targets)
                {
                    e.TakeDamage(damage);
                }
            }
            ++fireTime;
        }

        public List<Enemy> DetectEnemies(List<Enemy> enemies)
        {
            List<Enemy> targets = new List<Enemy>();
            foreach (Enemy e in enemies)
            {
                double dist = CalculateDistance(xPos, yPos, e.posX, e.posY);
                if (range >= dist)
                {
                    if (!e.type.Contains("aircraft") && e.type != "aboss")
                    {
                        targets.Add(e);
                    }
                }
            }
            return targets;
        }
    }
}
