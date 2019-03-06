using Hunter.Interfaces;
using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Core
{
    public class BuildingEntrance : IDrawable
    {
        public BuildingEntrance()
        {
            Color = Colors.BuildingEntrance;
            BackgroundColor = Colors.BuildingEntranceBackground;
        }
        //public bool IsOpen { get; set; }

        public RLColor Color { get; set; }
        public RLColor BackgroundColor { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }

            //Symbol = IsOpen ? '1' : '1';
            if (map.IsInFov(X, Y))
            {
                Color = Colors.BuildingEntranceFov;
                BackgroundColor = Colors.BuildingEntranceBackgroundFov;
            }
            else
            {
                Color = Colors.BuildingEntrance;
                BackgroundColor = Colors.BuildingEntranceBackground;
            }

            console.Set(X, Y, Color, BackgroundColor, Symbol);
        }
    }
}

