// This file contains the Map class and Intersection Struct class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TowerDefenseGUI
{
    // The map class holds the list of map paths, a reference to the
    // chosen path, a enumerated type, and a value to specify the selected map
    public class Map
    {
        public enum Direction { RIGHT, LEFT, UP, DOWN }; // stores the direction for the path intersection
        static List<List<Intersection>> pathways = new List<List<Intersection>>(); // list of map paths
        public static List<Intersection> coords; // the selected path
        public int mapID; // the selection of map

        // Hard codes the 3 map paths by identifying the intersections in the path.
        public Map(int id)
        {
            // map 1 coords ([200,0], [200,200], [350,200], [350,550], [150,550],[150,750],[600,750],[600,550],[900,550],[900,150],[700,150],[700,350],[550,350],[550,0])
            var path = new List<Intersection>();
            path.Add(new Intersection(175, 0, Direction.DOWN));
            path.Add(new Intersection(175, 175, Direction.DOWN));
            path.Add(new Intersection(325, 175, Direction.RIGHT));
            path.Add(new Intersection(325, 525, Direction.DOWN));
            path.Add(new Intersection(125, 525, Direction.LEFT));
            path.Add(new Intersection(125, 725, Direction.DOWN));
            path.Add(new Intersection(575, 725, Direction.RIGHT));
            path.Add(new Intersection(575, 525, Direction.UP));
            path.Add(new Intersection(875, 525, Direction.RIGHT));
            path.Add(new Intersection(875, 125, Direction.UP));
            path.Add(new Intersection(675, 125, Direction.LEFT));
            path.Add(new Intersection(675, 325, Direction.DOWN));
            path.Add(new Intersection(525, 325, Direction.LEFT));
            path.Add(new Intersection(525, -25, Direction.UP));
            pathways.Add(path);
            // map 2 coords ( [0,350], [350,350], [350,150], [750,150], [750,600], [200,600], [200,800], [500,800], [500,900] )
            path = new List<Intersection>();
            path.Add(new Intersection(0, 325, Direction.RIGHT));
            path.Add(new Intersection(325, 325, Direction.RIGHT));
            path.Add(new Intersection(325, 125, Direction.UP));
            path.Add(new Intersection(725, 125, Direction.RIGHT));
            path.Add(new Intersection(725, 575, Direction.DOWN));
            path.Add(new Intersection(175, 575, Direction.LEFT));
            path.Add(new Intersection(175, 775, Direction.DOWN));
            path.Add(new Intersection(475, 775, Direction.RIGHT));
            path.Add(new Intersection(475, 900, Direction.DOWN));
            pathways.Add(path);
            // map 3 coords ( [0,350], [1000,325] )
            path = new List<Intersection>();
            path.Add(new Intersection(0, 325, Direction.RIGHT));
            path.Add(new Intersection(1000, 325, Direction.RIGHT));
            pathways.Add(path);
            mapID = id;
            coords = pathways[mapID];
        }
    }

    // A Struct that holds the x, y, and direction values of the map intersection
    public struct Intersection
    {
        public int x;
        public int y;
        public Map.Direction direction;

        // assigns the x, y, and direction values
        public Intersection(int v1, int v2, Map.Direction dir)
        {
            x = v1;
            y = v2;
            direction = dir;
        }
    }
}
