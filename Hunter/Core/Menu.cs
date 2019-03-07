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

            Draw();

            RLConsole.Blit(menuConsole, 0, 0, _width, _height, rootConsole, 0, 0);
        }
        
        private List<string> LineArray()
        {
            List<string> linesLocal = new List<string>();
            linesLocal.Add("Location: Country Sheriff's Office");
            linesLocal.Add("");
            linesLocal.Add("Your mission should you choose to accept it");
            linesLocal.Add("");
            linesLocal.Add("For you first mission you are assigned to track Outlaw Billy,");
            linesLocal.Add("He's been shooting up saloons along the valley.");
            linesLocal.Add("Dead or Alive, preferably dead.");
            return linesLocal;
        }

        public void Draw()
        {
            List<string> lines = LineArray();
            for (int i = 0; i < lines.Count; i++)
            {
                //Console.WriteLine(lines[i]);
                menuConsole.Print(1, i + 1, lines[i], RLColor.White);
            }
        }
    }
}
