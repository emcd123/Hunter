using Hunter.Core;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Monsters
{
    public class Goon : Monster
    {
        public static Goon Create(int level=4)
        {
            int health = 25;

            return new Goon
            {
                Attack = Dice.Roll("1D6") + level,
                Awareness = 10,
                Color = Colors.GoonColor,
                Defense = Dice.Roll("1D6") + level,
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Goon",
                Speed = 17,
                Symbol = 'g',
                IsBoss = false
            };
        }
    }
}
