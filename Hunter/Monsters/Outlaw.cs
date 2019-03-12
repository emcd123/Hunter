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

        public static Outlaw Create(int level)
        {
            int health = Dice.Roll("5D5");
            IsBoss = true;
            return new Outlaw
            {
                Attack = Dice.Roll("1D5") + level / 3,
                AttackChance = Dice.Roll("25D3"),
                Awareness = 10,
                Color = Colors.OutlawColor,
                Defense = Dice.Roll("1D3") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Outlaw",
                Speed = 14,
                Symbol = 'o'
            };
        }
    }
}
