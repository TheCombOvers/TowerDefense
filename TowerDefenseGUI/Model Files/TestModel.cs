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
            Game g = new Game(0, false, null, null, 1);
            Game.map = new Map(0);
            g.currentWave = 5;
            g.waveProgress = 3;
            g.score = 700;
            Game.money = 200;
            g.isWaveOver = false;
            g.spawner = new Spawner(null, null);
            Infantry i1 = Infantry.MakeInfantry("b");
            i1.posX = 2;
            i1.posY = 4;
            i1.health = 13;
            i1.pathProgress = 3;
            Vehicle v1 = Vehicle.MakeVehicle("b");
            v1.posX = 6;
            v1.posY = 2;
            v1.health = 10;
            v1.pathProgress = 2;
            Spawner.enemies.Add(i1);
            Spawner.enemies.Add(v1);
            g.currentEnemies.Add(i1);
            g.currentEnemies.Add(v1);
            Mortar m1 = Mortar.MakeMortar(2,3,0);
            Stun s1 = Stun.MakeStun(7,5,0);
            Tesla t1 = Tesla.MakeTesla(3,5,0);
            g.currentTurrets = new List<Turret>();
            g.currentTurrets.Add(m1);
            g.currentTurrets.Add(s1);
            g.currentTurrets.Add(t1);
            Game loadedGame = Game.LoadGame("SavedGame.txt", null, null);
            // all should be working
            Assert.AreEqual(g.currentEnemies.Count, loadedGame.currentEnemies.Count);
            Assert.AreEqual(8 , Game.lives);
            Assert.AreEqual(g.currentWave, loadedGame.currentWave);
            Assert.AreEqual(200, Game.money);
            Assert.AreEqual(g.score, loadedGame.score);
            Assert.AreEqual(g.waveProgress, loadedGame.waveProgress);
            Assert.AreEqual(g.difficulty, loadedGame.difficulty);
            Assert.AreEqual(g.isWaveOver, loadedGame.isWaveOver);
            Assert.AreEqual(g.currentTurrets.Count, loadedGame.currentTurrets.Count);
        }
        [Test]
        public void SaveGame_ValidInput_ValidString()
        {
            Game g = new Game(0, false, null, null, 1);
            g.currentWave = 5;
            g.waveProgress = 3;
            Game.money = 200;
            g.score = 700;
            g.isWaveOver = false;
            Game.lives = 8;
            Game.map = new Map(0);
            Mortar m1 = Mortar.MakeMortar(2,3,0);
            Stun s1 = Stun.MakeStun(7,5,0);
            Tesla t1 = Tesla.MakeTesla(3,5,0);
            g.currentTurrets = new List<Turret>();
            g.currentTurrets.Add(m1);
            g.currentTurrets.Add(s1);
            g.currentTurrets.Add(t1);
            g.spawner = new Spawner(null, null);
            Infantry i1 = Infantry.MakeInfantry("b");
            i1.posX = 2;
            i1.posY = 4;
            i1.health = 13;
            i1.pathProgress = 3;
            Vehicle v1 = Vehicle.MakeVehicle("b");
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
            Assert.AreEqual(SerializedGame, "NG\r\n0,5,3,700,200,20,false,1\r\nENEMIES\r\nbinfantry,2,4,13,3\r\nbvehicle,6,2,10,2\r\nENDENEMIES\r\nTURRETS\r\nmortar,2,3\r\nstun,7,5\r\ntesla,3,5\r\nENDTURRETS\r\nEND\r\n");
            reader.Close();
        }
    }
}
