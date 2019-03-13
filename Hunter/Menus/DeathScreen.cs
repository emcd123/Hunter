using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Menus
{
    public class DeathScreen : Menu
    {
        public DeathScreen(int menuWidth, int menuHeight) : base(menuWidth, menuHeight)
        {
        }

        public void CreateDeathScreen(RLConsole rootConsole)
        {
            CreateMenu(rootConsole, DeathLineArray());
        }

        private List<string> DeathLineArray()
        {
            List<string> linesLocal = new List<string>();
            linesLocal.Add("YOU DIED!");
            linesLocal.Add("");
            linesLocal.Add("Press 'Enter' key to return to Town");
            return linesLocal;
        }    
    }
}
