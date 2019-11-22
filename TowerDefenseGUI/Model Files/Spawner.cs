using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TowerDefenseGUI
{
    class Spawner : Game
    {
        public List<Enemy> enemies;
        Timer spawnRate;

        public void Spawn(int wave)
        {
            int count = DetermineWave(ref wave);
            GenerateWave(wave, count);
        }

        private void GenerateWave(int wave, int count)
        {
            if (wave == 1)
            {
                for (int i = 0; i < count; i++)
                {
                    enemies.Add(new Infantry());
                }
            }
            if (wave == 3)
            {
                for (int i = 0; i < count; i++)
                {
                    enemies.Add(new Vehicle());
                }
            }
            if (wave == 5)
            {
                for (int i = 0; i < count; i++)
                {
                    enemies.Add(new Boss());
                }
            }
            if (wave == 7)
            {
                for (int i = 0; i < count; i++)
                {
                    enemies.Add(new Aircraft());
                }
            }
        }

        private int DetermineWave(ref int wave)
        {
            int count = 0;
            if (wave % 9 == 0)
            {
                count = wave / 9;
                wave = 9;
            }
            else if (wave % 7 == 0)
            {
                count = wave / 7;
                wave = 7;
            }
            else if (wave % 5 == 0)
            {
                count = wave / 5;
                wave = 5;
            }
            else if (wave % 3 == 0)
            {
                count = wave / 3;
                wave = 3;
            }
            else if (wave % 2 == 0)
            {
                count = wave / 2;
                wave = 2;
            }
            else
            {
                count = wave;
                wave = 1;
            }
            return count;
        }
    }
}
