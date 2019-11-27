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
        public int waveTotal; // number of waves required to win the game
        public int currentWave;
        public bool isWaveOver;
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


        public Game(int mapID, Action<Enemy> add, Action<Enemy> remove)
        {
            currentWave = 0;
            isWaveOver = false;
            waveProgress = 0;
            money = 200;
            score = 0;
            lives = 10;
            map = new Map(mapID);
            spawner = new Spawner();
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
            //foreach(Turret t in currentTurrets)
            //{
            //    t.Attack(DetectEnemy(currentEnemies));
            //}
        }

        public static void TakeLife()
        {
            if (lives > 0)
            {
                lives--;
            }
        }


        // loads a game that is saved in the file named "filename" and starts that saved game
        public static Game LoadGame(string fileName, Action<Enemy> add, Action<Enemy> remove)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string begin = reader.ReadLine(); // read the first line and check for a New Game or NG
                Game newGame = new Game(0, add, remove); // needs changed later 0 = map idex
                if (begin == "NG\t")
                {
                    // switch case here calling the different factory methods depending on which types we run into
                    // you gotta make the factory methods also ask schuab about this and how it relates to the intereface
                    string[] gameInfo = reader.ReadLine().Split(',');   // grab the game state information

                    map = new Map(Convert.ToInt32(gameInfo[0]));    // create a new map based on the mapid
                    newGame.currentWave = Convert.ToInt32(gameInfo[1]);
                    newGame.waveProgress = Convert.ToInt32(gameInfo[2]);
                    newGame.score = Convert.ToInt32(gameInfo[3]);
                    newGame.money = Convert.ToInt32(gameInfo[4]);
                    newGame.waveTotal = Convert.ToInt32(gameInfo[5]);       // need to change/fix this so that it is a difficulty instead of a int
                    if (gameInfo[6] == "false") { newGame.isWaveOver = false; }
                    else { newGame.isWaveOver = true; }
                    Game.lives = Convert.ToInt32(gameInfo[7]);

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
                                    case "boss":
                                        Boss b = Boss.MakeBoss();
                                        b.Deserialize(line);
                                        newGame.currentEnemies.Add(b);
                                        break;
                                    case "aircraft":
                                        Aircraft a = Aircraft.MakeAircraft();
                                        a.Deserialize(line);
                                        newGame.currentEnemies.Add(a);
                                        break;
                                    case "infantry":
                                        Infantry i = Infantry.MakeInfantry();
                                        i.Deserialize(line);
                                        newGame.currentEnemies.Add(i);
                                        break;
                                    case "vehicle":
                                        Vehicle v = Vehicle.MakeVehicle();
                                        v.Deserialize(line);
                                        newGame.currentEnemies.Add(v);
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
                                        MachineGun m = MachineGun.MakeMachineGun();
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
                string gameState = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", map.mapID, currentWave, waveProgress, score, money, waveTotal, waveOver, lives);
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
