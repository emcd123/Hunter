using Hunter.Core;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Equipments
{
    public class BaseBallBat : MeleeEquipment
    {
        public static BaseBallBat Create()
        {
            return new BaseBallBat
            {
                Attack = Dice.Roll("2D6"),
                Color = Colors.GoonColor,
                Defense = Dice.Roll("1D6"),
                Name = "BaseballBat",
                Symbol = '!',
            };
        }
    }
}

