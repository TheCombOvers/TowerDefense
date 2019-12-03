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
            string aircraft = string.Format("{0},{1},{2},{3},{4}", "aircraft", posX, posY, health, pathProgress);
            return aircraft;
        }
        public override object Deserialize(string info)
        {
            string[] aInfo = info.Split(',');
            Aircraft a = MakeAircraft();
            a.posX = Convert.ToDouble(aInfo[1]);
            a.posY = Convert.ToDouble(aInfo[2]);
            a.health = Convert.ToDouble(aInfo[3]);
            a.pathProgress = Convert.ToInt32(aInfo[4]);
            return a;
        }
        public static Aircraft MakeAircraft()
        {
            Aircraft a = new Aircraft();
            a.imageID = 2;
            a.health = 20;
            a.rewardMoney = 10;
            a.speed = 50 / 60;
            a.posX = Map.coords[0].x;
            a.posY = Map.coords[0].y;
            a.pathProgress = 0;
            a.rewardScore = 8;
            a.type = "aircraft";  
            return a;
        }
    }
}
