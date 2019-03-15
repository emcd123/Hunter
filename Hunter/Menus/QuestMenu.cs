using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Menus
{
    public class QuestMenu : Menu
    {
        public QuestMenu(int menuWidth, int menuHeight) : base(menuWidth, menuHeight)
        {
        }

        public void CreateQuestMenu(RLConsole rootConsole)
        {
            CreateMenu(rootConsole, QuestLineArray());
        }

        private List<string> QuestLineArray()
        {
            List<string> linesLocal = new List<string>();
            linesLocal.Add("Location: Country Sheriff's Office");
            linesLocal.Add("");
            linesLocal.Add("Your mission should you choose to accept it");
            linesLocal.Add("");
            linesLocal.Add("For you first mission you are assigned to track Outlaw Billy,");
            linesLocal.Add("He's been shooting up saloons along the valley.");
            linesLocal.Add("Dead or Alive, preferably dead.");
            linesLocal.Add("");
            linesLocal.Add("");
            linesLocal.Add("[Press 'Enter' to begin mission, 'E' to close this menu]");
            return linesLocal;
        }
    }
}
