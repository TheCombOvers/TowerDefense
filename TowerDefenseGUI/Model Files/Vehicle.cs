﻿// This file contains the Vehicle class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    // The Vehicle class contains a factory method and implements serialization methods.
    class Vehicle : Enemy
    {
        // serializes the object into a string of values and returns it.
        public override string Serialize()
        {
            string vehicle = string.Format("{0},{1},{2},{3},{4}", type, posX, posY, health, pathProgress);
            return vehicle;
        }

        // deserializes a string and converts it into an object with the specified values within that string.
        public override object Deserialize(string info)
        {

            string[] aInfo = info.Split(',');
            string s = aInfo[0] == "avehicle" ? "a" : "b";
            Vehicle v = MakeVehicle(s);
            v.posX = Convert.ToDouble(aInfo[1]);
            v.posY = Convert.ToDouble(aInfo[2]);
            v.health = Convert.ToDouble(aInfo[3]);
            v.pathProgress = Convert.ToInt32(aInfo[4]);
            return v;
        }

        // receives a string and returns a default vehicle object based off the string
        public static Vehicle MakeVehicle(string type)
        {
            Vehicle v = new Vehicle();
            switch (type)
            {
                case "b":
                    v.imageID = 1;
                    v.health = 30;
                    v.rewardMoney = 15;
                    v.speed = 6.25; // decide speed for advance type, replace 4 with desired speed 
                    v.rewardScore = 8;
                    v.type = "bvehicle";
                    break;
                case "a":
                    v.imageID = 5;
                    v.health = 80;
                    v.rewardMoney = 40;
                    v.speed = 5; // decide speed for advance type, replace 4 with desired speed 
                    v.rewardScore = 15;
                    v.type = "avehicle";
                    break;
            }           
            v.posX = Map.coords[0].x;
            v.posY = Map.coords[0].y;
            v.pathProgress = 0;
            return v;
        }
    }
}
