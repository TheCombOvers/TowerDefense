using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
namespace TowerDefenseGUI
{
    class Infantry : Enemy
    {
        public override string Serialize()
        {
            string infantry = string.Format("{0},{1},{2},{3},{4}", type, posX, posY, health, pathProgress);
            return infantry;
        }
        public override object Deserialize(string info)
        {
            string[] aInfo = info.Split(',');
            Infantry i = MakeInfantry(aInfo[0]);
            i.posX = Convert.ToDouble(aInfo[1]);
            i.posY = Convert.ToDouble(aInfo[2]);
            i.health = Convert.ToDouble(aInfo[3]);
            i.pathProgress = Convert.ToInt32(aInfo[4]);
            return i;
        }
        public static Infantry MakeInfantry(string type)
        {
            Infantry i = new Infantry();
            switch (type)
            {
                case "b":
                    i.imageID = 0;
                    i.health = 20;
                    i.rewardMoney = 5;
                    i.speed = 2; // decide speed for advance type, replace 4 with desired speed 
                    i.rewardScore = 2;
                    i.type = "binfantry";
                    break;
                case "a":
                    i.imageID = 4;
                    i.health = 50;
                    i.rewardMoney = 15;
                    i.speed = 4; // decide speed for advance type, replace 4 with desired speed 
                    i.rewardScore = 5;
                    i.type = "ainfantry";
                    break;
            }
            i.posX = Map.coords[0].x;
            i.posY = Map.coords[0].y;
            i.pathProgress = 0;
            return i;
        }
    }
}
