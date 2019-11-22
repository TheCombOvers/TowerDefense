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
            Game g = new Game();
            g.currentWave = 5;
            g.waveTotal = 20;
            g.waveProgress = 3;
            g.money = 200;
            g.score = 700;
            g.isWaveOver = false;
            Game.map = new Map(0);
            Mortar m1 = new Mortar();
            m1.xPos = 2;
            m1.yPos = 3;
            Stun s1 = new Stun();
            s1.xPos = 7;
            s1.yPos = 5;
            Tesla t1 = new Tesla();
            t1.xPos = 3;
            t1.yPos = 5;
            g.currentTurrets = new List<Turret>();
            g.currentTurrets.Add(m1);
            g.currentTurrets.Add(s1);
            g.currentTurrets.Add(t1);
            g.spawner = new Spawner();
            Infantry i1 = new Infantry();
            i1.posX = 2;
            i1.posY = 4;
            i1.health = 13;
            i1.pathProgress = 3;
            Vehicle v1 = new Vehicle();
            v1.posX = 6;
            v1.posY = 2;
            v1.health = 10;
            v1.pathProgress = 2;
            g.spawner.enemies.Add(i1);
            g.spawner.enemies.Add(v1);
            Assert.AreEqual(Game.LoadGame("SavedGame.txt"), g);
        }
        [Test]
        public void SaveGame_ValidInput_ValidString()
        {
            Game g = new Game();
            g.currentWave = 5;
            g.waveTotal = 20;
            g.waveProgress = 3;
            g.money = 200;
            g.score = 700;
            g.isWaveOver = false;
            Game.map = new Map(0);
            Game.map.mapID = 1;
            Mortar m1 = new Mortar();
            m1.xPos = 2;
            m1.yPos = 3;
            Stun s1 = new Stun();
            s1.xPos = 7;
            s1.yPos = 5;
            Tesla t1 = new Tesla();
            t1.xPos = 3;
            t1.yPos = 5;
            g.currentTurrets = new List<Turret>();
            g.currentTurrets.Add(m1);
            g.currentTurrets.Add(s1);
            g.currentTurrets.Add(t1);
            g.spawner = new Spawner();
            Infantry i1 = new Infantry();
            i1.posX = 2;
            i1.posY = 4;
            i1.health = 13;
            i1.pathProgress = 3;
            Vehicle v1 = new Vehicle();
            v1.posX = 6;
            v1.posY = 2;
            v1.health = 10;
            v1.pathProgress = 2;
            g.spawner.enemies.Add(i1);
            g.spawner.enemies.Add(v1);
            g.SaveGame("SavedGame.txt");
            StreamReader reader = new StreamReader("SavedGame.txt");
            string SerializedGame = reader.ReadToEnd();
            Assert.AreEqual(SerializedGame, "NG\n1,5,3,700,200,20,false\nENEMIES\ninfantry,2,4,13\nvehicle,6,2,13\nTURRETS\nmortar,2,3\nstun,7,5\ntesla,3,5\nEND");
            reader.Close();
        }
    }
}
