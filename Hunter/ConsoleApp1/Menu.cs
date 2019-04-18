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
        public static int _numConsoles = 7;

        private static readonly int _menuBackgroundWidth = 100;
        private static readonly int _menuBackgroundHeight = 65;
        private static RLConsole _menuBackgroundConsole;

        private static readonly int _titleWidth = 100;
        private static readonly int _titleHeight = 5;
        private static RLConsole _titleConsole;

        private static readonly int _subMenuWidth = 40;
        private static readonly int _subMenuHeight = 5;

        public Menu() { }

        public static void InstantiateBaseMenu()
        {
            _menuBackgroundConsole = new RLConsole(_menuBackgroundWidth, _menuBackgroundHeight);
            _titleConsole = new RLConsole(_titleWidth, _titleHeight);
        }

        public static void DrawBaseMenu()
        {
            _menuBackgroundConsole.SetBackColor(0, 0, _menuBackgroundWidth, _menuBackgroundHeight, RLColor.Black);
            _menuBackgroundConsole.Print(1, 1, "Background", RLColor.White);

            _titleConsole.SetBackColor(0, 0, _titleWidth, _titleHeight, RLColor.Cyan);
            _titleConsole.Print(1, 1, "Inventory", RLColor.White);
        }

        public static void BlitBaseMenu(RLRootConsole _root)
        {
            RLConsole.Blit(_menuBackgroundConsole, 0, 0, _menuBackgroundWidth, _menuBackgroundHeight, _root, 0, _titleHeight);
            RLConsole.Blit(_titleConsole, 0, 0, _titleWidth, _titleHeight, _root, 0, 0);
        }

        public static List<RLConsole> CreateSubMenusIter(RLRootConsole _root)
        {
            List<RLConsole> SubMenuConsoleList = new List<RLConsole>() { };
            for (int i = 0; i < _numConsoles; i++)
            {
                SubMenuConsoleList.Add(Menu.CreateSubMenus(_root));
            }
            return SubMenuConsoleList;
        }

        public static void BlitSubMenusIter(RLRootConsole _root, List<RLConsole> SubMenus)
        {
            int inc = 5;
            for (int i = 0; i < _numConsoles; i++)
            {
                if (i < 5)
                    Menu.BlitSubMenus(_root, SubMenus[i], inc, 0);
                else
                {
                    inc = 5;
                    Menu.BlitSubMenus(_root, SubMenus[i], inc, 50);
                }
                inc += 7;
            }
        }

        public static void DrawSubMenusIter(List<RLConsole> SubMenus)
        {
            for (int i = 0; i < Menu._numConsoles; i++)
            {
                Menu.DrawSubMenus(SubMenus[i]);
            }
        }

        public static RLConsole CreateSubMenus( RLRootConsole _root)
        {
            return new RLConsole(_subMenuWidth, _subMenuHeight);         
        }

        public static void BlitSubMenus(RLRootConsole _root, RLConsole _localsubMenuConsole, int increment, int x_val)
        {
            RLConsole.Blit(_localsubMenuConsole, 0, 0, _subMenuWidth, _subMenuHeight, _root, x_val, 5+increment);
        }

        public static void DrawSubMenus(RLConsole _localsubMenuConsole)
        {
            _localsubMenuConsole.SetBackColor(0, 0, _subMenuWidth, _subMenuHeight, RLColor.Green);
            _localsubMenuConsole.Print(1, 1, "Sub Menu", RLColor.White);
        }
    }
}
