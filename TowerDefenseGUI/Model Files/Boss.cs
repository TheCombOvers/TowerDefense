using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Boss : Enemy
    {
        public override string Serialize()
        {
            string boss = string.Format("{0},{1},{2},{3},{4}", "boss", posX, posY, health, pathProgress);
            return boss;
        }
        public override object Deserialize(string info)
        {
            string[] aInfo = info.Split(',');
            Boss a = MakeBoss();
            a.posX = Convert.ToDouble(aInfo[1]);
            a.posY = Convert.ToDouble(aInfo[2]);
            a.health = Convert.ToDouble(aInfo[3]);
            a.pathProgress = Convert.ToInt32(aInfo[4]);
            return a;
        }

        public static Boss MakeBoss()
        {
            Boss b = new Boss();
            b.health = 100;
            b.rewardMoney = 50;
            b.speed = 20 / 60;
            b.posX = Map.coords[0].x;
            b.posY = Map.coords[0].y;
            b.pathProgress = 0;
            b.rewardScore = 45;
            b.type = "boss";
            return b;
        }
    }
}
