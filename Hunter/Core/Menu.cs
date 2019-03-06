using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Core
{
    public class Menu
    {
        private RLConsole menuConsole;
        private int _width;
        private int _height;

        public Menu(int menuWidth, int menuHeight)
        {
            _width = menuWidth;
            _height = menuHeight;
        }

        public void CreateMenu(RLConsole rootConsole)
        {
            menuConsole = new RLConsole(_width, _height);

            menuConsole.Clear();

            RLConsole.Blit(menuConsole, 0, 0, _width, _height, rootConsole, 0, 0);
        }
    }
}
