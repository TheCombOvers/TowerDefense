using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace TowerDefenseGUI.Model_Files
{
    [TestFixture]
    class HighscoreTest
    {
        [Test]
        private void TestHighscoreAdd()
        {
            Highscores hs = new Highscores();
            hs.AddHighscore("John", 34000);
            hs.AddHighscore("Mark", 68000);
            hs.AddHighscore("Lewis", 25000);
            Assert.IsTrue(hs.highscorelst.Contains("John") && hs.highscorelst.Contains("34000") && hs.highscorelst.Contains("Mark") && hs.highscorelst.Contains("68000") && hs.highscorelst.Contains("Lewis") && hs.highscorelst.Contains("25000"));
        }
    }
}
