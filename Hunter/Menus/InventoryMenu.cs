using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Menus
{
    public class InventoryMenu : Menu
    {
        public InventoryMenu(int menuWidth, int menuHeight) : base(menuWidth, menuHeight)
        {
        }

        public void CreateInventoryMenu(RLConsole rootConsole)
        {
            CreateMenu(rootConsole);
        }
    }
}
