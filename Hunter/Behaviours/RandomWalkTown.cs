using Hunter.Core;
using Hunter.Interfaces;
using Hunter.Systems;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Behaviours
{
    public class RandomWalkTown
    {
        public bool Act(Npc villager, CommandSystem commandSystem)
        {
            DungeonMap dungeonMap = Game.DungeonMap;

            ICell NpcCell = dungeonMap.GetCell(villager.X, villager.Y);

            Random rng = new Random();
            int randomDir = rng.Next(0, 5);

            commandSystem.MoveVillager(NpcCell, villager, randomDir);
            return true;
        }
    }
}
