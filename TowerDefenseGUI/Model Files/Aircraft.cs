// This file contains the Aircraft class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    // The Aircraft class contains a factory method and implements serialization methods.
    class Aircraft : Enemy
    {
        // serializes the object into a string of values and returns it.
        public override string Serialize()
        {
            string aircraft = string.Format("{0},{1},{2},{3},{4}", type, posX, posY, health, pathProgress);
            return aircraft;
        }

        // deserializes a string and converts it into an object with the specified values within that string.
        public override object Deserialize(string info)
        {
            string[] aInfo = info.Split(',');
            string s = aInfo[0] == "aaircraft" ? "a" : "b";
            Aircraft a = MakeAircraft(s);
            a.posX = Convert.ToDouble(aInfo[1]);
            a.posY = Convert.ToDouble(aInfo[2]);
            a.health = Convert.ToDouble(aInfo[3]);
            a.pathProgress = Convert.ToInt32(aInfo[4]);
            return a;
        }

        // receives a string and returns a default aircraft object based off the string
        public static Aircraft MakeAircraft(string type)
        {
            Aircraft a = new Aircraft();
            switch (type)
            {
                case "b":
                    a.imageID = 2;
                    a.health = 30;
                    a.rewardMoney = 10;
                    a.speed = 6.25; // decide speed for advance type, replace 4 with desired speed 
                    a.rewardScore = 10;
                    a.type = "baircraft";
                    break;
                case "a":
                    a.imageID = 6;
                    a.health = 90;
                    a.rewardMoney = 30;
                    a.speed = 3.125; // decide speed for advance type, replace 4 with desired speed 
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
