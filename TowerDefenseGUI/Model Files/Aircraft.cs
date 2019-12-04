using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Aircraft : Enemy
    {
        public override string Serialize()
        {
            string aircraft = string.Format("{0},{1},{2},{3},{4}", type, posX, posY, health, pathProgress);
            return aircraft;
        }
        public override object Deserialize(string info)
        {
            string[] aInfo = info.Split(',');
            Aircraft a = MakeAircraft(aInfo[0]);
            a.posX = Convert.ToDouble(aInfo[1]);
            a.posY = Convert.ToDouble(aInfo[2]);
            a.health = Convert.ToDouble(aInfo[3]);
            a.pathProgress = Convert.ToInt32(aInfo[4]);
            return a;
        }
        public static Aircraft MakeAircraft(string type)
        {
            Aircraft a = new Aircraft();
            switch (type)
            {
                case "b":
                a.imageID = 2;
                a.health = 20;
                a.rewardMoney = 10;
                a.speed = 2; // decide speed for advance type, replace 4 with desired speed 
                a.rewardScore = 10;
                a.type = "baircraft";
                break;
                case "a":
                a.imageID = 6;
                a.health = 60;
                a.rewardMoney = 20;
                a.speed = 4; // decide speed for advance type, replace 4 with desired speed 
                a.rewardScore = 20;
                a.type = "aaircraft";
                break;
            }
            a.posX = Map.coords[0].x;
            a.posY = Map.coords[0].y;
            a.pathProgress = 0;
            return a;
        }
    }
}
