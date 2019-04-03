using Hunter.Equipments;
using Hunter.Interfaces;
using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Core
{
    public class Equipment : Item
    {
        public void GiveStartingWeapons(IActor actor)
        {
            actor.Melee = SwitchBlade.Create();
            actor.Inventory.AddItem(actor.Melee);
            if(actor is Player)
                Game.MessageLog.Add($"{actor.Name} started with a {actor.Melee.Name}");
        }

        public void DrawStats(RLConsole EquipConsole, int position)
        {

        }
    }
}
