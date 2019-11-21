using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TowerDefenseGUI
{
    public class Map
    {
        public enum Direction { RIGHT, LEFT, UP, DOWN };
        static List<Image> maps = new List<Image>();
        static List<List<Intersection>> pathways = new List<List<Intersection>>();
        public static Image map;
        public static List<Intersection> coords;
        public int mapID;

        // map 1 coords ( [0,350], [350,350], [350,150], [750,150], [750,600], [200,600], [200,800], [500,800], [500,900] )
        public Map(int id)
        {
            var img = new Image();
            img.Source = new BitmapImage(new Uri("path1.png"));
            maps.Add(img);
            var path = new List<Intersection>();
            path.Add(new Intersection(0, 350, Direction.RIGHT));
            path.Add(new Intersection(350, 350, Direction.UP));
            path.Add(new Intersection(350, 150, Direction.RIGHT));
            path.Add(new Intersection(750, 150, Direction.DOWN));
            path.Add(new Intersection(750, 600, Direction.LEFT));
            path.Add(new Intersection(200, 600, Direction.DOWN));
            path.Add(new Intersection(200, 800, Direction.RIGHT));
            path.Add(new Intersection(500, 800, Direction.DOWN));
            path.Add(new Intersection(500, 900, Direction.DOWN));
            pathways.Add(path);
            mapID = id;
            map = maps[mapID];
            coords = pathways[mapID];
        }
    }

    public struct Intersection
    {
        public int x;
        public int y;
        public Map.Direction direction;

        public Intersection(int v1, int v2, Map.Direction dir)
        {
            x = v1;
            y = v2;
            direction = dir;
        }
    }
}
