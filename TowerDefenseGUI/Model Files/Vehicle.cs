using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Vehicle : Enemy
    {
        public override string Serialize()
        {
            string vehicle = string.Format("{0},{1},{2},{3},{4}", "vehicle", posX, posY, health, pathProgress);
            return vehicle;
        }
        public override object Deserialize(string info)
        {

            string[] aInfo = info.Split(',');
            Vehicle v = MakeVehicle();
            v.posX = Convert.ToDouble(aInfo[1]);
            v.posY = Convert.ToDouble(aInfo[2]);
            v.health = Convert.ToDouble(aInfo[3]);
            v.pathProgress = Convert.ToInt32(aInfo[4]);
            return v;
        }
        public static Vehicle MakeVehicle()
        {
            Vehicle v = new Vehicle();
            v.health = 30;
            v.rewardMoney = 10;
            v.speed = 80 / 60;
            v.posX = Map.coords[0].x;
            v.posY = Map.coords[0].y;
            v.pathProgress = 0;
            v.rewardScore = 8;
            v.type = "vehicle";
            return v;
        }
    }
}
