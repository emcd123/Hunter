using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLNET;

namespace Hunter.Core
{
    public class Player : Actor
    {
        public Player()
        {
            Awareness = 15;
            Name = "Rogue";
            Color = Colors.Player;
            Symbol = '@';
        }

        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Set(X, Y, Color, null, Symbol);
        }
    }
}
