// This file contains the Spawner class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace TowerDefenseGUI
{
    // The spawner class handles the creation of waves, generating of enemies, and removing of enemies
    class Spawner
    {
        public static List<Enemy> enemies = new List<Enemy>(); // list of the spawned enemies
        static Action<Enemy, bool> Remove; // the gui delegate to remove enemies
        public static int[] count = { 0, 0 }; // the number of units to spawn separated into two types
        public static string[] types = { "", "" }; // the two types of units to spawn
        public static event EventHandler<Enemy> DisplayWave; // gui event to spawn enemies

        // initializes the gui remove method
        public Spawner(Action<Enemy, bool> RemoveEnemy)
        {
            Remove = RemoveEnemy;
        }

        // sets count values and types values and calls the gui event to spawn the wave
        public void Spawn(int wave)
        {
            count = new int[2] { 0, 0 };
            types = new string[2] { "", "" };
            count = DetermineWaveNumbers(wave);
            types = DetermineWave(wave, count);
            Console.WriteLine(count[0] + " " + types[0] + ", " + count[1] + " " + types[1]);
            DisplayWave(this, null);
        }

        // returns an enemy object and decrements the count
        public static Enemy GenerateEnemy()
        {
            Console.WriteLine("Generating enemy");
            Enemy e = null;
            if (count[0] > 0)
            {
                switch (types[0])
                {
                    case "bI":
                        enemies.Add(Infantry.MakeInfantry("b"));
                        e = enemies[enemies.Count - 1];
                        count[0]--;
                        break;
                    case "aI":
                        enemies.Add(Infantry.MakeInfantry("a"));
                        e = enemies[enemies.Count - 1];
                        count[0]--;
                        break;
                    case "bV":
                        enemies.Add(Vehicle.MakeVehicle("b"));
                        e = enemies[enemies.Count - 1];
                        count[0]--;
                        break;
                    case "aV":
                        enemies.Add(Vehicle.MakeVehicle("b"));
                        e = enemies[enemies.Count - 1];
                        count[0]--;
                        break;
                    case "bA":
                        enemies.Add(Aircraft.MakeAircraft("b"));
                        e = enemies[enemies.Count - 1];
                        count[0]--;
                        break;
                    case "gB":
                        enemies.Add(Boss.MakeBoss("g"));
                        e = enemies[enemies.Count - 1];
                        count[0]--;
                        break;
                }
            }
            else if (count[1] > 0)
            {
                switch (types[1])
                {
                    case "aI":
                        enemies.Add(Infantry.MakeInfantry("a"));
                        e = enemies[enemies.Count - 1];
                        count[1]--;
                        break;
                    case "aV":
                        enemies.Add(Vehicle.MakeVehicle("a"));
                        e = enemies[enemies.Count - 1];
                        count[1]--;
                        break;
                    case "aA":
                        enemies.Add(Aircraft.MakeAircraft("a"));
                        e = enemies[enemies.Count - 1];
                        count[1]--;
                        break;
                    case "aB":
                        enemies.Add(Boss.MakeBoss("a"));
                        e = enemies[enemies.Count - 1];
                        count[1]--;
                        break;
                }
            }
            else
            {
                Console.WriteLine("no enemy created");
                return null;
            }
            Console.WriteLine(e.type + " was created");
            return e;
        }

        // evaluates the count values based off the wave and returns an array with the count
        private int[] DetermineWaveNumbers(int wave)
        {
            int[] num = new int[2];
            int count;
            if (wave % 7 == 0)
            {
                count = wave / 7;
                if (count > 1)
                {
                    num[0] = (count - 1) * 2;
                    if (Convert.ToDouble(count) / 2 > 1)
                    {

                        num[1] = (count - 2) * 2;
                    }
                    else
                    {
                        num[0] = 0;
                        num[1] = 1;
                    }
                }
                else
                {
                    num[0] = 1;
                }
            }
            else if (wave % 5 == 0)
            {
                count = wave / 5;
                if (count > 1)
                {
                    num[0] = (count - 1) * 2;
                    if (Convert.ToDouble(count) / 2 > 1)
                    {
                        num[1] = (count - 2) * 2;
                    }
                    else
                    {
                        num[0] = 0;
                        num[1] = 1;
                    }
                }
                else
                {
                    num[0] = 1;
                }
            }
            else if (wave % 3 == 0)
            {
                count = wave / 3;
                if (count > 1)
                {
                    num[0] = (count - 1) * 2;
                    if (Convert.ToDouble(count) / 2 > 1)
                    {
                        num[1] = (count - 2) * 2;
                    }
                    else
                    {
                        num[0] = 0;
                        num[1] = 1;
                    }
                }
                else
                {
                    num[0] = 1;
                }
            }
            else if (wave % 2 == 0)
            {
                count = wave / 2;
                if (count > 1)
                {
                    num[0] = (count - 1) * 2;
                    if (Convert.ToDouble(count) / 2 > 1)
                    {
                        num[1] = (count - 2) * 2;
                    }
                    else
                    {
                        num[0] = 0;
                        num[1] = 1;
                    }
                }
                else
                {
                    num[0] = 2;
                }
            }
            else if (wave > 1)
            {
                num = new int[2] { wave / 2, wave / 4 };
            }
            else
            {
                num = new int[2] { 1, 0 };
            }
            return num;
        }

        // evaluates the types values based off the wave and returns an array with the types
        private string[] DetermineWave(int wave, int[] count)
        {
            string[] types = new string[2];
            if (wave % 7 == 0)
            {
                types[0] = "bA";
                types[1] = "aA";
            }
            else if (wave % 5 == 0)
            {
                types[0] = "gB";
                types[1] = "aB";
            }
            else if (wave % 3 == 0)
            {
                types[0] = "bV";
                types[1] = "aV";
            }
            else if (wave % 2 == 0)
            {
                types[0] = "bI";
                types[1] = "aI";
            }
            else if (wave > 1)
            {
                int rand;
                if(wave == 13)
                {
                    rand = 0;

                }
                else
                {
                    rand = new Random().Next(0, 2);

                }
                if (rand == 0)
                {
                    types[0] = "aI";
                    types[1] = "aV";
                }
                else if (rand == 1)
                {
                    types[0] = "aI";
                    types[1] = "aA";
                }
                else
                {
                    types[0] = "aV";
                    types[1] = "aA";
                }
            }
            else
            {
                types[0] = "bI";
                types[1] = "";
            }
            return types;
        }

        // calls the gui remove delegate
        public static void RemoveEnemy(Enemy enemy, bool isKill)
        {
            Remove(enemy, isKill);
        }
    }
}
