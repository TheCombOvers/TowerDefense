﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Flak : Turret
    {
        public override string Serialize()
        {
            string flak = string.Format("{0},{1},{2}", "flak", xPos, yPos);
            return flak;
        }
        public override object Deserialize(string info)
        {
            string[] finfo = info.Split(',');
            xPos = Convert.ToInt32(finfo[1]);
            yPos = Convert.ToInt32(finfo[2]);          
            return this;
        }
        public static Flak MakeFlak(int x, int y)
        {
            Flak f = new Flak();
            f.xPos = x;
            f.yPos = y;
            f.imageID = 1;
            f.cost = 75;
            f.damage = 2;
            f.range = 250/2;
            f.type = "flak";           
            return f;
        }
    }
}
