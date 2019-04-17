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
        private RLConsole menuConsole;
        private readonly int _width;
        private readonly int _height;

        public Menu(int menuWidth, int menuHeight)
        {
            _width = menuWidth;
            _height = menuHeight;
            menuConsole = new RLConsole(_width, _height);
        }

        public void CreateMenu(RLConsole rootConsole)
        {
            menuConsole.Clear();
            //DrawTopConsole(LineArray());
            RLConsole subMenuConsole = CreateSubMenu(menuConsole, 100, 10, 0, 10);
           // RLConsole.Blit(subMenuConsole, subMenuConsole., subMenuConsole.y, subMenuWidth, subMenuHeight, menuConsoleLoc, 0, 0);
            RLConsole.Blit(menuConsole, 0, 0, _width, _height, rootConsole, 0, 0);
        }

        #region alt-contructor
        public void CreateMenu(RLConsole rootConsole, List<string> LineArr)
        {
            menuConsole = new RLConsole(_width, _height);
            menuConsole.Clear();
            DrawTopConsole(LineArr);
            RLConsole.Blit(menuConsole, 0, 0, _width, _height, rootConsole, 0, 0);

        }

        private List<string> LineArray()
        {
            List<string> linesLocal = new List<string>();
            linesLocal.Add("hello");
            return linesLocal;
        }
        #endregion

        public RLConsole CreateSubMenu(RLConsole menuConsoleLoc, int subMenuWidth, int subMenuHeight, int x, int y)
        {
            RLConsole subMenuConsole;
            subMenuConsole = new RLConsole(subMenuWidth, subMenuHeight);
            subMenuConsole.Clear();

            subMenuConsole.SetBackColor(0, 0, RLColor.Cyan);
            return subMenuConsole;
        }

        public List<RLConsole> ListOfSubConsoles(int numSubConsoles)
        {
            int y_increment = 10;
            List<RLConsole> list_consoles = new List<RLConsole>();
            for (int i = 0; i < numSubConsoles; i++)
            {
                RLConsole sub_menu = CreateSubMenu(menuConsole, 100, 10, 0, y_increment);
                list_consoles.Add(sub_menu);
                y_increment += 10;
            }
            return list_consoles;
        }

        public void DrawSubConsoles(int numSubConsoles)
        {
            int y_increment = 10;
            List<RLConsole> list_consoles = new List<RLConsole>();
            for (int i = 0; i < numSubConsoles; i++)
            {
                RLConsole sub_menu = CreateSubMenu(menuConsole, 100, 10, 0, y_increment);
                y_increment += 10;
            }
        }

        public void DrawTopConsole(List<string> LineArray)
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
