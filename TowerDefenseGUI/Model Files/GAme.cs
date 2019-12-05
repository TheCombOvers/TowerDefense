using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace TowerDefenseGUI
{
    public interface ISerializeObject
    {
        string Serialize();
        object Deserialize(string info);
    }
    class Game
    {
        public int difficulty; // number of waves required to win the game
        public int currentWave;
        public bool isWaveOver;
        static public bool cheatMode;
        public int waveProgress;
        public int money;
        public int score;
        public static int lives;
        public static Map map;
        public List<Turret> currentTurrets = new List<Turret>(); // list of turrets  currently on the screen
        public List<Enemy> currentEnemies = new List<Enemy>();  // list of enemies currently on the field
        public Spawner spawner;
        public Action<Enemy> addEnemy;
        public Action<Enemy> removeEnemy;


        public Game(int mapID,bool cheat, Action<Enemy> add, Action<Enemy> remove, int diff)
        {
            difficulty = diff;
            cheatMode = cheat;
            currentWave = 0;
            isWaveOver = false;
            waveProgress = 0;         
            money = cheat == true ? 999999: 200;
            score = 0;
            lives = 10;
            map = new Map(mapID);
            spawner = new Spawner(add, remove);
            addEnemy = add;
            removeEnemy = remove;
        }
        public void NewGame(int difficulty, int mapIndex, Map selcetedMap)
        {

        }

        public void NextWave()
        {
            waveProgress++;
            spawner.Spawn(waveProgress, addEnemy, removeEnemy);
        }

        public void UpdateModel()
        {
            if (Spawner.enemies.Count > 0)
            {
                currentEnemies = Spawner.enemies;
                for(int i=0;i<currentEnemies.Count;i++)
                {
                    currentEnemies[i].UpdatePos();
                }
            }
            foreach (Turret t in currentTurrets)
            {
                t.Attack(t.DetectEnemy(currentEnemies));
            }
        }

        public static void TakeLife()
        {
            if (lives > 0 && cheatMode != true)
            {
                lives--;
            }
        }

        // loads a game that is saved in the file named "filename" and returns that game
        public static Game LoadGame(string fileName, Action<Enemy> add, Action<Enemy> remove)
        {
            using (StreamReader reader = new StreamReader(fileName))
            { 
                string begin = reader.ReadLine(); // read the first line and check for a New Game or NG
                Game newGame = new Game(0, true, add, remove, 0);
                if (begin == "NG")
                {
                  
                    string[] gameInfo = reader.ReadLine().Split(',');   // grab the game state information
                    
                    map = new Map(Convert.ToInt32(gameInfo[0]));    // create a new map based on the mapid
                    newGame.currentWave = Convert.ToInt32(gameInfo[1]);
                    newGame.waveProgress = Convert.ToInt32(gameInfo[2]);
                    newGame.score = Convert.ToInt32(gameInfo[3]);
                    newGame.money = Convert.ToInt32(gameInfo[4]);
                    newGame.difficulty = Convert.ToInt32(gameInfo[5]);       // need to change/fix this so that it is a difficulty instead of a int
                    if (gameInfo[6] == "false") { newGame.isWaveOver = false; }
                    else { newGame.isWaveOver = true; }
                    Game.lives = Convert.ToInt32(gameInfo[7]);
                    if (gameInfo[8] == "false") { cheatMode = false; } // cheatmode is static btw

                    while (true)
                    {
                        string section = reader.ReadLine(); // read the section header
                        if (section.Trim() == "END") { break; }    // if its END we're done

                        if (section.Trim() == "ENEMIES")
                        {
                            while (true)
                            {
                                string line = reader.ReadLine(); // read a line
                                if (line.Trim() == "ENDENEMIES") { break; } // see if we're at the end yet break if we are
                                string[] ele = line.Split(',');
                                switch (ele[0])                 // grab the type and call methods based on it
                                {
                                    case "gboss":
                                        Boss bG = Boss.MakeBoss("g");
                                        bG.Deserialize(line);
                                        Spawner.enemies.Add(bG);
                                        break;
                                    case "aboss":
                                        Boss bA = Boss.MakeBoss("a");
                                        bA.Deserialize(line);
                                        Spawner.enemies.Add(bA);
                                        break;
                                    case "baircraft":
                                        Aircraft a = Aircraft.MakeAircraft("b");
                                        a.Deserialize(line);
                                        Spawner.enemies.Add(a);
                                        break;
                                    case "aaircraft":
                                        Aircraft aA = Aircraft.MakeAircraft("a");
                                        aA.Deserialize(line);
                                        Spawner.enemies.Add(aA);
                                        break;
                                    case "binfantry":
                                        Infantry i = Infantry.MakeInfantry("b");
                                        i.Deserialize(line);
                                        Spawner.enemies.Add(i);
                                        break;
                                    case "ainfantry":
                                        Infantry iA = Infantry.MakeInfantry("a");
                                        iA.Deserialize(line);
                                        Spawner.enemies.Add(iA);
                                        break;
                                    case "bvehicle":
                                        Vehicle v = Vehicle.MakeVehicle("b");
                                        v.Deserialize(line);
                                        Spawner.enemies.Add(v);
                                        break;
                                    case "avehicle":
                                        Vehicle vA = Vehicle.MakeVehicle("a");
                                        vA.Deserialize(line);
                                        Spawner.enemies.Add(vA);
                                        break;
                                    default:
                                        Debug.WriteLine("The enemy type was not correct.");
                                        break;
                                }
                            }
                        }
                        else if (section.Trim() == "TURRETS")
                        {
                            while (true)
                            {
                                string lineT = reader.ReadLine(); // read a line
                                if (lineT.Trim() == "ENDTURRETS") { break; } // see if we're at the end yet break if we are
                                string[] eleT = lineT.Split(',');
                                switch (eleT[0])                 // grab the type and call methods based on it
                                {
                                    case "flak":
                                        Flak f = Flak.MakeFlak();
                                        f.Deserialize(lineT);
                                        newGame.currentTurrets.Add(f);
                                        break;
                                    case "laser":
                                        Laser l = Laser.MakeLaser();
                                        l.Deserialize(lineT);
                                        newGame.currentTurrets.Add(l);
                                        break;
                                    case "machinegun":
                                        MachineGun m = new MachineGun();
                                        m.Deserialize(lineT);
                                        newGame.currentTurrets.Add(m);
                                        break;
                                    case "mortar":
                                        Mortar mo = Mortar.MakeMortar();
                                        mo.Deserialize(lineT);
                                        newGame.currentTurrets.Add(mo);
                                        break;
                                    case "stun":
                                        Stun s = Stun.MakeStun();
                                        s.Deserialize(lineT);
                                        newGame.currentTurrets.Add(s);
                                        break;
                                    case "tesla":
                                        Tesla t = Tesla.MakeTesla();
                                        t.Deserialize(lineT);
                                        newGame.currentTurrets.Add(t);
                                        break;
                                    default:
                                        Debug.WriteLine("The turret type was not correct.");
                                        break;
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < Spawner.enemies.Count; ++i)
                {
                    newGame.addEnemy(Spawner.enemies[i]);
                }
                return newGame;
            }
        }
        // save the current state of the game in the file "fileName" and returns a string of what we saved
        public void SaveGame(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("NG");
                string waveOver = isWaveOver == true ? "true" : "false";
                string gameState = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", map.mapID, currentWave, waveProgress, score, money, difficulty, waveOver, lives, cheatMode);
                writer.WriteLine(gameState);
                if (currentEnemies.Count != 0)
                {
                    writer.WriteLine("ENEMIES");
                    for (int i = 0; i < currentEnemies.Count; ++i)
                    {
                        string line = currentEnemies[i].Serialize();
                        writer.WriteLine(line);
                    }
                    writer.WriteLine("ENDENEMIES");
                }

                if (currentTurrets.Count != 0)
                {
                    writer.WriteLine("TURRETS");
                    for (int i = 0; i < currentTurrets.Count; ++i)
                    {
                        string line = currentTurrets[i].Serialize();
                        writer.WriteLine(line);
                    }
                    writer.WriteLine("ENDTURRETS");
                }
                writer.WriteLine("END");
            }
        }
    }
}
