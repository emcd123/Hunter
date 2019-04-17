using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSandbox
{
    public static class TestSandbox
    {
        // The screen height and width are in number of tiles
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;
        private static RLRootConsole _rootConsole;

        // The map console takes up most of the screen and is where the map will be drawn
        private static readonly int _menuBackgroundWidth = 100;
        private static readonly int _menuBackgroundHeight = 65;
        private static RLConsole _menuBackgroundConsole;

        // Above the map is the inventory console which shows the players equipment, abilities, and items
        private static readonly int _titleWidth = 100;
        private static readonly int _titleHeight = 5;
        private static RLConsole _titleConsole;

        private static readonly int _subMenuWidth = 40;
        private static readonly int _subMenuHeight = 5;
        private static RLConsole _subMenuConsole;

        public static void Main()
        {
            // This must be the exact name of the bitmap font file we are using or it will error.
            string fontFileName = "terminal8x8.png";
            // The title will appear at the top of the console window
            string consoleTitle = "RougeSharp V3 Tutorial - Level 1";
            // Tell RLNet to use the bitmap font that we specified and that each tile is 8 x 8 pixels
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight, 8, 8, 1f, consoleTitle);

            // Initialize the sub consoles that we will Blit to the root console
            _menuBackgroundConsole = new RLConsole(_menuBackgroundWidth, _menuBackgroundHeight);
            _titleConsole = new RLConsole(_titleWidth, _titleHeight);
            _subMenuConsole = new RLConsole(_subMenuWidth, _subMenuHeight);

            // Set up a handler for RLNET's Update event
            _rootConsole.Update += OnRootConsoleUpdate;
            // Set up a handler for RLNET's Render event
            _rootConsole.Render += OnRootConsoleRender;
            // Begin RLNET's game loop
            _rootConsole.Run();
        }

        // Event handler for RLNET's Update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            // Set background color and text for each console 
            // so that we can verify they are in the correct positions
            _menuBackgroundConsole.SetBackColor(0, 0, _menuBackgroundWidth, _menuBackgroundHeight, RLColor.Black);
            _menuBackgroundConsole.Print(1, 1, "Map", RLColor.White);

            _titleConsole.SetBackColor(0, 0, _titleWidth, _titleHeight, RLColor.Cyan);
            _titleConsole.Print(1, 1, "Inventory", RLColor.White);

            _subMenuConsole.SetBackColor(0, 0, _subMenuWidth, _subMenuHeight, RLColor.Green);
            _subMenuConsole.Print(1, 1, "Sub Menu", RLColor.White);
        }

        // Event handler for RLNET's Render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            // Blit the sub consoles to the root console in the correct locations
            RLConsole.Blit(_menuBackgroundConsole, 0, 0, _menuBackgroundWidth, _menuBackgroundHeight,  _rootConsole, 0, _titleHeight);
            RLConsole.Blit(_titleConsole, 0, 0, _titleWidth, _titleHeight, _rootConsole, 0, 0);

            RLConsole.Blit(_subMenuConsole, 0, 0, _subMenuWidth, _subMenuHeight, _rootConsole, 0, _titleHeight);

            // Tell RLNET to draw the console that we set
            _rootConsole.Draw();
        }
    }
}
