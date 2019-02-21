using Hunter.Core;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Monsters
{
    public class Kobold : Monster
    {
        //One thing to note is that we didn’t create a Draw() method for the Kobold,
        //yet he will still be able to be drawn to our map just fine.
        //The reason for this is because of the inheritance chain we set up.Kobold : Monster : Actor.
        //Because an Actor already has a Draw() method the Kobold will get it automatically through inheritance

        public static Kobold Create(int level)
        {
            int health = Dice.Roll("2D5");
            return new Kobold
            {
                //Attack = Dice.Roll("1D3") + level / 3,
                //AttackChance = Dice.Roll("25D3"),
                Awareness = 10,
                Color = Colors.KoboldColor,
                //Defense = Dice.Roll("1D3") + level / 3,
                //DefenseChance = Dice.Roll("10D4"),
                //Gold = Dice.Roll("5D5"),
                //Health = health,
                //MaxHealth = health,
                Name = "Kobold",
                //Speed = 14,
                Symbol = 'k'
            };
        }
    }
}
