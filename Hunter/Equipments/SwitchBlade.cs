using Hunter.Core;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Equipments
{
    public class SwitchBlade : MeleeEquipment
    {
        public static SwitchBlade Create()
        {            
            return new SwitchBlade
            {
                Attack = Dice.Roll("1D6"),
                Color = Colors.GoonColor,
                Defense = Dice.Roll("1D6"),
                Name = "SwitchBlade",
                Symbol = '|',
            };
        }
    }
}
