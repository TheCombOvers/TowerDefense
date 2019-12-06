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
            string boss = string.Format("{0},{1},{2},{3},{4}", type, posX, posY, health, pathProgress);
            return boss;
        }
        public override object Deserialize(string info)
        {
            string[] aInfo = info.Split(',');
            string s = aInfo[0] == "aboss" ? "a" : "g";
            Boss a = MakeBoss(s);
            a.posX = Convert.ToDouble(aInfo[1]);
            a.posY = Convert.ToDouble(aInfo[2]);
            a.health = Convert.ToDouble(aInfo[3]);
            a.pathProgress = Convert.ToInt32(aInfo[4]);
            return a;
        }

        public static Boss MakeBoss(string type)
        {
            Boss b = new Boss();
            switch (type)
            {
                case "g":
                    b.imageID = 3;
                    b.health = 100;
                    b.rewardMoney = 75;
                    b.speed = 1; // decide speed for advance type, replace 4 with desired speed 
                    b.rewardScore = 45;
                    b.type = "gboss";
                    break;
                case "a":
                    b.imageID = 7;
                    b.health = 150;
                    b.rewardMoney = 100;
                    b.speed = 1.25; // decide speed for advance type, replace 4 with desired speed 
                    b.rewardScore = 60;
                    b.type = "aboss";
                    break;
            }
            b.posX = Map.coords[0].x-15;
            b.posY = Map.coords[0].y-15;
            b.pathProgress = 0;
            return b;
        }
    }
}
