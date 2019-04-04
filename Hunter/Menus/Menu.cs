using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Menus
{
    public class Menu
    {
        private static RLConsole menuConsole;
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

        public void CreateMenu(RLConsole rootConsole, List<string> LineArr)
        {
            menuConsole = new RLConsole(_width, _height);

            menuConsole.Clear();

            Draw(LineArr);

            RLConsole.Blit(menuConsole, 0, 0, _width, _height, rootConsole, 0, 0);
        }


        private List<string> LineArray()
        {
            List<string> linesLocal = new List<string>();
            linesLocal.Add("");
            return linesLocal;
        }

        public void Draw(List<string> LineArray)
        {
            List<string> lines = LineArray;
            for (int i = 0; i < lines.Count; i++)
            {
                //Console.WriteLine(lines[i]);
                menuConsole.Print(1, i + 1, lines[i], RLColor.White);
            }
        }
    }
}
