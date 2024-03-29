﻿// This file contains the Infantry class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{   
    // The Infantry class contains a factory method and implements serialization methods.
    class Infantry : Enemy
    {
        // serializes the object into a string of values and returns it.
        public override string Serialize()
        {
            string infantry = string.Format("{0},{1},{2},{3},{4}", type, posX, posY, health, pathProgress);
            return infantry;
        }

        // deserializes a string and converts it into an object with the specified values within that string.
        public override object Deserialize(string info)
        {
            string[] aInfo = info.Split(',');
            string s = aInfo[0] == "ainfantry" ? "a" : "b";
            Infantry i = MakeInfantry(s);
            i.posX = Convert.ToDouble(aInfo[1]);
            i.posY = Convert.ToDouble(aInfo[2]);
            i.health = Convert.ToDouble(aInfo[3]);
            i.pathProgress = Convert.ToInt32(aInfo[4]);
            return i;
        }

        // receives a string and returns a default infantry object based off the string
        public static Infantry MakeInfantry(string type)
        {
            Infantry i = new Infantry();
            switch (type)
            {
                case "b":
                    i.imageID = 0;
                    i.health = 20;
                    i.rewardMoney = 10;
                    i.speed = 3.125; // decide speed for advance type, replace 4 with desired speed 
                    i.rewardScore = 2;
                    i.type = "binfantry";
                    break;
                case "a":
                    i.imageID = 4;
                    i.health = 50;
                    i.rewardMoney = 25;
                    i.speed = 2.5; // decide speed for advance type, replace 4 with desired speed 
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
