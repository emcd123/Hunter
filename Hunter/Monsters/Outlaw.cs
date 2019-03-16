using Hunter.Core;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Monsters
{
    public class Outlaw : Monster
    {

        public static Outlaw Create(int level=4)
        {
            int health = 50;
            return new Outlaw
            {
                Attack = Dice.Roll("1D10") + level,
                Awareness = 10,
                Color = Colors.OutlawColor,
                Defense = Dice.Roll("1D10") + level,
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Outlaw",
                Speed = 14,
                Symbol = 'o',
                IsBoss = true
            };
        }
    }
}
