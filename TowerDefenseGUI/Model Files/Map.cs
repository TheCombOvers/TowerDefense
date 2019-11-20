using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TowerDefenseGUI
{
    public class Map
    {
        public enum Direction { RIGHT, LEFT, UP, DOWN };
        static List<Image> maps;
        public static Image map;
        static List<List<Intersection>> pathways;
        public static List<Intersection> coords;
        public int mapID;

        // map 1 coords ( [0,350], [350,350], [350,150], [750,150], [750,600], [200,600], [200,800], [500,800], [500,900] )
        
    }

    public struct Intersection
    {
        public int x;
        public int y;
        public Map.Direction direction;
    }
}
