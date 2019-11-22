using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace TowerDefenseGUI
{
    class Infantry : Enemy
    {
        public override string Serialize()
        {
            string infantry = string.Format("{0},{1},{2},{3},{4}", "infantry", posX, posY, health, pathProgress);
            return infantry;
        }
        public override object Deserialize(string info)
        {
            string[] aInfo = info.Split(',');
            Infantry i = MakeInfantry();
            i.posX = Convert.ToDouble(aInfo[1]);
            i.posY = Convert.ToDouble(aInfo[2]);
            i.health = Convert.ToDouble(aInfo[3]);
            i.pathProgress = Convert.ToInt32(aInfo[4]);
            return i;
        }
        public static Infantry MakeInfantry()
        {
            Infantry i = new Infantry();
            i.image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Basic Unit.png"));
            i.health = 20;
            i.rewardMoney = 5;
            i.speed = 50 / 60;
            i.posX = Map.coords[0].x;
            i.posY = Map.coords[0].y;
            i.pathProgress = 0;
            i.rewardScore = 2;
            i.type = "infantry";
            return i;
        }
    }
}
