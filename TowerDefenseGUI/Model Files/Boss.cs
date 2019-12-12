// This file contains the Boss class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    // The Boss class contains a factory method and implements serialization methods.
    class Boss : Enemy
    {
        // serializes the object into a string of values and returns it.
        public override string Serialize()
        {
            string boss = string.Format("{0},{1},{2},{3},{4}", type, posX, posY, health, pathProgress);
            return boss;
        }

        // deserializes a string and converts it into an object with the specified values within that string.
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

        // receives a string and returns a default boss object based off the string
        public static Boss MakeBoss(string type)
        {
            Boss b = new Boss();
            switch (type)
            {
                case "g":
                    b.imageID = 3;
                    b.health = 250;
                    b.rewardMoney = 100;
                    b.speed = 1; // decide speed for advance type, replace 4 with desired speed 
                    b.rewardScore = 45;
                    b.type = "gboss";
                    break;
                case "a":
                    b.imageID = 7;
                    b.health = 400;
                    b.rewardMoney = 200;
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
