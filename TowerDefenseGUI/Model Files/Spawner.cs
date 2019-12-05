using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace TowerDefenseGUI
{
    class Spawner
    {
        public static List<Enemy> enemies = new List<Enemy>();
        static Action<Enemy> Remove;
        static Action<Enemy> Add;
        public Spawner(Action<Enemy> AddEnemy, Action<Enemy> RemoveEnemy)
        {
            Remove = RemoveEnemy;
            Add = AddEnemy;
        }
        public void Spawn(int wave)
        {
            int[] count = DetermineWave(wave);
            GenerateWave(wave, count);
            Console.WriteLine(enemies.Count);
            foreach (Enemy en in enemies)
            {
                Add(en);
            }
        }

        private int[] DetermineWave(int wave)
        {
            int[] num = new int[2];
            int count;
            if (wave % 7 == 0)
            {
                count = wave / 7;
                if (count > 1)
                {
                    num[0] = (count - 1) * 2;
                    if (count / 2 > 1)
                    {

                        num[1] = (count - 1) * 2;
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
                    if (count / 2 > 1)
                    {

                        num[1] = (count - 1) * 2;
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
                    if (count / 2 > 1)
                    {

                        num[1] = (count - 1) * 2;
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
                    if (count / 2 > 1)
                    {

                        num[1] = (count - 1) * 2;
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
            else if(wave > 1)
            {
                num = new int[2] { wave/2, wave/4 };
            }
            else
            {
                num = new int[2] { 1, 0 };
            }
            return num;
        }

        private void GenerateWave(int wave, int[] count)
        {
            Console.WriteLine(count[0] + " " + count[1]);
            if (wave % 7 == 0)
            {
                for (int i = 0; i < count[0]; i++)
                {
                    enemies.Add(Aircraft.MakeAircraft("b"));
                }
                for (int i = 0; i < count[1]; i++)
                {
                    enemies.Add(Aircraft.MakeAircraft("a"));
                }
            }
            else if (wave % 5 == 0)
            {
                for (int i = 0; i < count[0]; i++)
                {
                    enemies.Add(Boss.MakeBoss("g"));
                }
                for (int i = 0; i < count[1]; i++)
                {
                    enemies.Add(Boss.MakeBoss("a"));
                }
            }
            else if (wave % 3 == 0)
            {
                for (int i = 0; i < count[0]; i++)
                {
                    enemies.Add(Vehicle.MakeVehicle("b"));
                }
                for (int i = 0; i < count[1]; i++)
                {
                    enemies.Add(Vehicle.MakeVehicle("a"));
                }
            }
            else if (wave % 2 == 0)
            {
                for (int i = 0; i < count[0]; i++)
                {
                    enemies.Add(Infantry.MakeInfantry("b"));
                }
                for (int i = 0; i < count[1]; i++)
                {
                    enemies.Add(Infantry.MakeInfantry("a"));
                }
            }
            else if (wave > 1)
            {
                int rand = new Random().Next(0,2);
                if (rand == 0)
                {
                    for (int i = 0; i < count[0]; i++)
                    {
                        enemies.Add(Infantry.MakeInfantry("a"));
                    }
                    for (int i = 0; i < count[1]; i++)
                    {
                        enemies.Add(Vehicle.MakeVehicle("a"));
                    }
                }
                else if (rand == 1)
                {
                    for (int i = 0; i < count[0]; i++)
                    {
                        enemies.Add(Infantry.MakeInfantry("a"));
                    }
                    for (int i = 0; i < count[1]; i++)
                    {
                        enemies.Add(Aircraft.MakeAircraft("a"));
                    }
                }
                else
                {
                    for (int i = 0; i < count[0]; i++)
                    {
                        enemies.Add(Vehicle.MakeVehicle("a"));
                    }
                    for (int i = 0; i < count[1]; i++)
                    {
                        enemies.Add(Aircraft.MakeAircraft("a"));
                    }
                }
            }
            else
            {
                enemies.Add(Infantry.MakeInfantry("b"));
            }
        }


        public static void RemoveEnemy(Enemy enemy)
        {
            Remove(enemy);
            enemies.Remove(enemy);
        }
    }
}
