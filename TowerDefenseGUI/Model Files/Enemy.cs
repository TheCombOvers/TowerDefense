// This file contains the Enemy class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Controls;

namespace TowerDefenseGUI
{
    // The enemy class contains the values for the enemy subclasses and the path AI methods.
    // It also contains abstract serialization methods.
    public abstract class Enemy : ISerializeObject
    {
        public int rewardMoney; // amount of money given upon death
        public int rewardScore; // amount of score given upon death
        public double health; // the health of the enemy
        public double speed; // the speed of the enemy
        public int stunned = 0; // the stun value on the enemy. defaults to 0
        public string type; // the type of enemy
        public int pathProgress; // the position within the map path
        public double posX; // the x position of the enemy
        public double posY; // the y position of the enemy
        public int imageID; // the reference used to determine what image file to use
        public int imageIndex; // the reference for the gui enemy list
        public static event EventHandler<int> RotateEnemy; // gui event to rotate the enemy

        // changes the x or y position of the enemy based off the CheckCoords direction.
        // doesn't move if stun value is > 0.
        public void UpdatePos()
        {
            var intersection = CheckCoords(Map.coords);
            if (stunned > 0)
            {
                stunned--;
            }
            else
            {
                if (intersection.direction == Map.Direction.RIGHT || intersection.direction == Map.Direction.LEFT)
                {
                    if (intersection.direction == Map.Direction.RIGHT)
                    {
                        RotateEnemy(this, 0);
                    }
                    else
                    {
                        RotateEnemy(this, 180);
                    }
                    posX += speed;
                }
                if (intersection.direction == Map.Direction.DOWN || intersection.direction == Map.Direction.UP)
                {
                    if (intersection.direction == Map.Direction.DOWN)
                    {
                        RotateEnemy(this, 90);
                    }
                    else
                    {
                        RotateEnemy(this, -90);
                    }
                    posY += speed;
                }
            }

        }

        // checks the enemy position compared to pathProgress direction in the map path.
        // tells the updatePos whether to modify the x or y coord of the enemy.
        // when the enemy position is close to the path change, it sets the speed to + or - and 
        // returns a string that says x or y. For example, if speed is negative and it returns 'x',
        // then the sprite will move to the left. Calls game takelife method and spawner remove method
        // if the enemy gets to the end of the path.
        public Intersection CheckCoords(List<Intersection> path)
        {
            int x = path[pathProgress].x;
            int y = path[pathProgress].y;
            if (type == "aboss" || type == "gboss")
            {
                if (x - 15 == posX && y - 15 == posY)
                {
                    pathProgress++;
                }
            }
            else if (x == posX && y == posY)
            {
                pathProgress++;
            }
            if (pathProgress > path.Count - 1)
            {
                Game.TakeLife();
                Spawner.RemoveEnemy(this, false);
                return new Intersection();
            }
            var dir = path[pathProgress].direction;
            if (dir == Map.Direction.RIGHT || dir == Map.Direction.DOWN)
            {
                if (speed < 0)
                {
                    speed *= -1;
                }
            }
            else if (dir == Map.Direction.LEFT || dir == Map.Direction.UP)
            {
                if (speed > 0)
                {
                    speed *= -1;
                }
            }
            return path[pathProgress];
        }

        // decrements enemy health by an amount
        public void TakeDamage(double amount)
        {
            health -= amount;
            if (health <= 0)
            {
                Spawner.RemoveEnemy(this, true);
                Game.money += rewardMoney;
            }
        }

        // method definition of parent serialize
        public abstract string Serialize();

        // method definition of parent deserialize
        public abstract object Deserialize(string info);
    }
}
