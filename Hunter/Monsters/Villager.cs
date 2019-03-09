﻿using Hunter.Core;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Monsters
{
    public class Villager : Npc
    {
        public static Villager Create(int level)
        {
            int health = Dice.Roll("2D5");
            return new Villager
            {
                //Attack = Dice.Roll("1D3") + level / 3,
                //AttackChance = Dice.Roll("25D3"),
                //Awareness = 10,
                Color = Colors.GoonColor,
                //Defense = Dice.Roll("1D3") + level / 3,
                //DefenseChance = Dice.Roll("10D4"),
                //Gold = Dice.Roll("5D5"),
                //Health = health,
                //MaxHealth = health,
                Name = "TownsPerson",
                Speed = 12,
                Symbol = 't'
            };
        }
    }
}
