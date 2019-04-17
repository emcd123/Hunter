using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSandbox
{
    public class Menu
    {
        public Menu() { }

        public static RLConsole CreateSubMenus( RLRootConsole _root, int _subMenuWidth, int _subMenuHeight)
        {
            return new RLConsole(_subMenuWidth, _subMenuHeight);         
        }

        public static void BlitSubMenus(RLRootConsole _root, RLConsole _localsubMenuConsole, int _subMenuWidth, int _subMenuHeight, int increment)
        {
            RLConsole.Blit(_localsubMenuConsole, 0, 0, _subMenuWidth, _subMenuHeight, _root, 0, 5+increment);
        }

        public static void DrawSubMenus(RLConsole _localsubMenuConsole, int _subMenuWidth, int _subMenuHeight)
        {
            _localsubMenuConsole.SetBackColor(0, 0, _subMenuWidth, _subMenuHeight, RLColor.Green);
            _localsubMenuConsole.Print(1, 1, "Sub Menu", RLColor.White);
        }
    }
}
