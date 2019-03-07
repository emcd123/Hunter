using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLNET;
using RogueSharp;

namespace Hunter.Core
{
    public class Colors
    {
        public static RLColor FloorBackground = RLColor.Black;
        public static RLColor Floor = Palette.AlternateDarkest;
        public static RLColor FloorBackgroundFov = Palette.DbDark;
        public static RLColor FloorFov = Palette.Alternate;

        public static RLColor WallBackground = Palette.SecondaryDarkest;
        public static RLColor Wall = Palette.Secondary;
        public static RLColor WallBackgroundFov = Palette.SecondaryDarker;
        public static RLColor WallFov = Palette.SecondaryLighter;

        public static RLColor DoorBackground = RLColor.Black;
        public static RLColor Door = Palette.DbSun;
        public static RLColor DoorBackgroundFov = Palette.DbDark;
        public static RLColor DoorFov = Palette.DbSun;

        public static RLColor BuildingEntranceBackground = RLColor.Black;
        public static RLColor BuildingEntrance = RLColor.Magenta;
        public static RLColor BuildingEntranceBackgroundFov = Palette.DbDark;
        public static RLColor BuildingEntranceFov = RLColor.LightMagenta;

        public static RLColor TextHeading = Palette.DbLight;

        public static RLColor Player = RLColor.White;
        public static RLColor KoboldColor = Palette.DbBrightWood;
        public static RLColor OutlawColor = Palette.DbBrightWood;
        public static RLColor GoonColor = Palette.DbStone;

        public static RLColor Text = Palette.DbLight;
        public static RLColor Gold = Palette.DbSun;
    }
}
