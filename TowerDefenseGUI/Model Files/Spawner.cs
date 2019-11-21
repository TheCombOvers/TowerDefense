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
            DetermineWave(wave);
            
        }

        private void DetermineWave(int wave)
        {
            if(wave % 3 == 0)
            {
                enemies.Add(new Vehicle());
            }
            else
            {
                enemies.Add(new Infantry());
            }
        }
    }
}
