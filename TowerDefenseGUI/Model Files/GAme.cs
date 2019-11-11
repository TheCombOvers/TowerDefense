using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    class Game
    {
        int waveTotal;
        int currentWave;
        bool isWaveOver;
        int money;
        public List<Map> maps;
        public List<Timer> gameTimers;
        Timer nextWaveTimer;

        public void NewGame(int difficulty, int mapIndex)
        {
        }

        public void NextWave()
        {

        }
        
        public void Pause()
        {

        }
    }
}
