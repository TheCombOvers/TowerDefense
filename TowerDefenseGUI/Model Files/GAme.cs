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
        // number of total waves player must play through. difficulty
        int waveTotal;
        // wave player is currently on
        int currentWave;
        bool isWaveOver;
        int money;
        public Map selectedMap;
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
