using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TowerDefenseGUI
{
    class Spawner
    {
        public static List<Enemy> enemies = new List<Enemy>();

        public void Spawn(int wave, Action<Enemy> AddEnemy)
        {
            Enemy e = Infantry.MakeInfantry();
            enemies.Add(e);
            AddEnemy(e);
            //int count = DetermineWave(wave);
            //GenerateWave(wave, count);
            //foreach(Enemy en in enemies)
            //{
            //    AddEnemy(en);
            //}
        }

        private void GenerateWave(int wave, int count)
        {
            if (wave == 2)
            {
                for (int i = 0; i < count; i++)
                {
                    enemies.Add(Infantry.MakeInfantry());
                }
            }
            if (wave == 3)
            {
                for (int i = 0; i < count; i++)
                {
                    enemies.Add(Vehicle.MakeVehicle());
                }
            }
            if (wave == 5)
            {
                for (int i = 0; i < count; i++)
                {
                    enemies.Add(Boss.MakeBoss());
                }
            }
            if (wave == 7)
            {
                for (int i = 0; i < count; i++)
                {
                    enemies.Add(Aircraft.MakeAircraft());
                }
            }
        }

        private int DetermineWave(int wave)
        {
            int count = 0;
            if (wave % 9 == 0)
            {
                count = wave / 9;
                if(count > 1)
                {
                    count = (count - 1) * 2;
                }
                else
                {
                    count = 1;
                }
            }
            else if (wave % 7 == 0)
            {
                count = wave / 7;
                if (count > 1)
                {
                    count = (count - 1) * 2;
                }
                else
                {
                    count = 1;
                }
            }
            else if (wave % 5 == 0)
            {
                count = wave / 5;
                if (count > 1)
                {
                    count = (count - 1) * 2;
                }
                else
                {
                    count = 1;
                }
            }
            else if (wave % 3 == 0)
            {
                count = wave / 3;
                if (count > 1)
                {
                    count = (count - 1) * 2;
                }
                else
                {
                    count = 1;
                }
            }
            else if (wave % 2 == 0)
            {
                count = wave / 2;
                if (count > 1)
                {
                    count = (count - 1) * 2;
                }
                else
                {
                    count = 1;
                }
            }
            else
            {
                count = wave;
            }
            return count;
        }

        public static void Remove(Enemy enemy)
        {
            enemies.Remove(enemy);
        }
    }
}
