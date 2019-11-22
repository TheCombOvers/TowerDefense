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
        public Image image;
        public int rewardMoney;
        public int rewardScore;
        public double health;
        public double speed;
        public string type;
        public int pathProgress;
        public double posX;
        public double posY;

        public void UpdatePos()
        {
            var intersection = CheckCoords(Map.coords);
            if (intersection.direction == Map.Direction.RIGHT || intersection.direction == Map.Direction.LEFT)
            {
                posX += speed;
            }
            if (intersection.direction == Map.Direction.DOWN || intersection.direction == Map.Direction.UP)
            {
                posY += speed;
            }
        }

        // check the enemy position compared to path direction change.
        // tells the updatePos whether to modify the x or y coord of the enemy.
        // when the enemy position is close to the path change, it sets the speed to + or - and 
        // returns a string that says x or y. For example, if speed is negative and it returns 'x',
        // then the sprite will move to the left.
        public Intersection CheckCoords(List<Intersection> path)
        {
            int x = path[pathProgress].x;
            int y = path[pathProgress].y;
            if (x == posX && y == posY)
            {
                pathProgress++;
            }
            var dir = path[pathProgress].direction;
            if (dir == Map.Direction.RIGHT || dir == Map.Direction.DOWN)
            {
                Math.Abs(speed);
            }
            else if (dir == Map.Direction.LEFT || dir == Map.Direction.UP)
            {
                speed *= -1;
            }
            return path[pathProgress];
        }

        public void TakeDamage(double amount)
        {

        }
        public abstract string Serialize();
        public abstract object Deserialize(string info);    
    }
}
