using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Core;
using RLNET;

namespace Hunter.Equipments
{
    public class MeleeEquipment : Equipment
    {
        public static MeleeEquipment None()
        {
            return new MeleeEquipment { Name = "None" };
        }

        public void DrawStats(RLConsole EquipConsole, int position)
        {

        }
    }
}
