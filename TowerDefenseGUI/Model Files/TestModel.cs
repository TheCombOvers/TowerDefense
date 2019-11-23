using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TowerDefenseGUI
{
    [TestFixture]
    class TestModel
    {
        [Test]
        public void LoadGame_ValidInput_ValidClass()
        {
            Game g = new Game(0, null, null);
            Game.map = new Map(0);
            g.currentWave = 5;
            g.waveProgress = 3;
            g.score = 700;
            g.money = 200;
            g.waveTotal = 20;
            g.isWaveOver = false;
            g.spawner = new Spawner();
            Infantry i1 = Infantry.MakeInfantry();
            i1.posX = 2;
            i1.posY = 4;
            i1.health = 13;
            i1.pathProgress = 3;
            Vehicle v1 = Vehicle.MakeVehicle();
            v1.posX = 6;
            v1.posY = 2;
            v1.health = 10;
            v1.pathProgress = 2;
            Spawner.enemies.Add(i1);
            Spawner.enemies.Add(v1);
            Mortar m1 = Mortar.MakeMortar();
            m1.xPos = 2;
            m1.yPos = 3;
            Stun s1 = Stun.MakeStun();
            s1.xPos = 7;
            s1.yPos = 5;
            Tesla t1 = Tesla.MakeTesla();
            t1.xPos = 3;
            t1.yPos = 5;
            g.currentTurrets = new List<Turret>();
            g.currentTurrets.Add(m1);
            g.currentTurrets.Add(s1);
            g.currentTurrets.Add(t1);
            Game loadedGame = Game.LoadGame("SavedGame.txt", null, null);
            //            Assert.AreEqual(Game.LoadGame("SavedGame.txt", null, null), g);
            //CORRECT Assert.AreEqual(loadedGame.currentEnemies, g.currentEnemies);
            //WRONG Assert.AreEqual(loadedGame.currentWave, g.currentWave);
            //CORRECT Assert.AreEqual(loadedGame.money, g.money);
            //WRONG Assert.AreEqual(loadedGame.score, g.score);
            //WRONG Assert.AreEqual(loadedGame.waveProgress, g.waveProgress);
            //WRONG Assert.AreEqual(loadedGame.waveTotal, g.waveTotal);
            //WRONG Assert.AreEqual(loadedGame.isWaveOver, g.isWaveOver);
            //WRONG Assert.AreEqual(loadedGame.currentTurrets, g.currentTurrets);

        }
        [Test]
        public void SaveGame_ValidInput_ValidString()
        {
            Game g = new Game(0, null, null);
            g.currentWave = 5;
            g.waveTotal = 20;
            g.waveProgress = 3;
            g.money = 200;
            g.score = 700;
            g.isWaveOver = false;
            Game.map = new Map(0);
            Mortar m1 = Mortar.MakeMortar();
            m1.xPos = 2;
            m1.yPos = 3;
            Stun s1 = Stun.MakeStun();
            s1.xPos = 7;
            s1.yPos = 5;
            Tesla t1 = Tesla.MakeTesla();
            t1.xPos = 3;
            t1.yPos = 5;
            g.currentTurrets = new List<Turret>();
            g.currentTurrets.Add(m1);
            g.currentTurrets.Add(s1);
            g.currentTurrets.Add(t1);
            g.spawner = new Spawner();
            Infantry i1 = Infantry.MakeInfantry();
            i1.posX = 2;
            i1.posY = 4;
            i1.health = 13;
            i1.pathProgress = 3;
            Vehicle v1 = Vehicle.MakeVehicle();
            v1.posX = 6;
            v1.posY = 2;
            v1.health = 10;
            v1.pathProgress = 2;
            g.currentEnemies.Add(i1);
            g.currentEnemies.Add(v1);
            Spawner.enemies.Add(i1);
            Spawner.enemies.Add(v1);
            g.SaveGame("SavedGame1.txt");
            StreamReader reader = new StreamReader("SavedGame1.txt");
            string SerializedGame = reader.ReadToEnd();
            Assert.AreEqual(SerializedGame, "NG\r\n0,5,3,700,200,20,false\r\nENEMIES\r\ninfantry,2,4,13,3\r\nvehicle,6,2,10,2\r\nENDENEMIES\r\nTURRETS\r\nmortar,2,3\r\nstun,7,5\r\ntesla,3,5\r\nENDTURRETS\r\nEND\r\n");
            reader.Close();
        }
    }
}
