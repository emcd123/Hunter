using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Core
{
    public class WinMenu : Menu
    {
        public WinMenu(int menuWidth, int menuHeight) : base(menuWidth, menuHeight)
        {
        }

        public void CreateWinScreen(RLConsole rootConsole)
        {
            CreateMenu(rootConsole, WinLineArray());
        }

        private List<string> WinLineArray()
        {
            List<string> linesLocal = new List<string>();
            linesLocal.Add("You killed the boss!");
            linesLocal.Add("");
            linesLocal.Add("Press 'Enter' key to return to Town");
            linesLocal.Add("");
            linesLocal.Add("Or Press 'E' to return to Dungoen");
            return linesLocal;
        }
    }
}
