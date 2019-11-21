using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Controls;

namespace TowerDefenseGUI
{
    public abstract class Enemy: ISerializeObject
    {
        Image image;
        public int rewardMoney;
        public int rewardScore;
        public double health;
        public double speed;
        public string type;
        public int pathProgress;
        public double posX;
        public double posY;
        public double updatePosRate;

        public void UpdatePos()
        {
            string direction = CheckCoords(Map.coords);
            if (direction.Equals("x"))
            {
                posX += speed;
            }
            if (direction.Equals("y"))
            {
                posY += speed;
            }
        }

        // check the enemy position compared to path direction change.
        // tells the updatePos whether to modify the x or y coord of the enemy.
        // when the enemy position is close to the path change, it sets the speed to + or - and 
        // returns a string that says x or y. For example, if speed is negative and it returns 'x',
        // then the sprite will move to the left.
        public string CheckCoords(List<Intersection> path)
        {
            string direction = null;
            int x = path[pathProgress].x;
            int y = path[pathProgress].y;
            if(x == posX && y == posY) {
                pathProgress++;
                x = path[pathProgress].x;
                y = path[pathProgress].y;
            }
            if(x == posX)
            {
                direction = "y";
                if(y < posY)
                {
                    speed *= -1;
                }
                else
                {
                    Math.Abs(speed);
                }
            }else if (y == posY)
            {
                direction = "x";
                if (x < posX)
                {
                    speed *= -1;
                }
                else
                {
                    Math.Abs(speed);
                }
            }
            return direction;
        }

        public void TakeDamage(double amount)
        {

        }
        public abstract string Serialize();
        public abstract object Deserialize(string info);    
    }
}
