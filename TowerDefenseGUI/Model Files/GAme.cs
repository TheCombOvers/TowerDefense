using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace TowerDefenseGUI
{
    public interface ISerializeObject
    {
        string Serialize();
        object Deserialize(string info);
    }
    class Game
    {
        public int waveTotal; // number of waves required to win the game
        public int currentWave;
        public bool isWaveOver;
        public int waveProgress;
        public int money;
        public int score;
        public static Map map;
        public Timer gameTimer;
        public List<Turret> currentTurrets; // list of turrets  currently on the screen
        Timer nextWaveTimer;
        public Spawner spawner;
        public void NewGame(int difficulty, int mapIndex, Map selcetedMap)
        {

        }

        public void NextWave()
        {

        }
        
        public void Pause()
        {
            
        }
        // loads a game that is saved in the file named "filename" and starts that saved game
        public static Game LoadGame(string fileName)
        {
            return new Game();
        }
        // save the current state of the game in the file "fileName" and returns a string of what we saved
        public string SaveGame(string fileName)
        {
            return "";
        }
    }
}
