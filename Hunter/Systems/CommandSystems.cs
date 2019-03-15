using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLNET;
using RogueSharp;

using Hunter.Core;
using RogueSharp.DiceNotation;
using Hunter.Interfaces;

namespace Hunter.Systems
{
    public class CommandSystem
    {
        // Return value is true if the player was able to move
        // false when the player couldn't move, such as trying to move into a wall
        Combat CombatSys = new Combat();
        public static bool IsPlayerTurn { get; set; }

        public void EndPlayerTurn()
        {
            CommandSystem.IsPlayerTurn = false;
        }

        public bool MovePlayer(Direction direction)
        {
            int x = Game.Player.X;
            int y = Game.Player.Y;

            switch (direction)
            {
                case Direction.Up:
                {
                    y = Game.Player.Y - 1;
                    break;
                }
                case Direction.Down:
                {
                    y = Game.Player.Y + 1;
                    break;
                }
                case Direction.Left:
                {
                    x = Game.Player.X - 1;
                    break;
                }
                case Direction.Right:
                {
                    x = Game.Player.X + 1;
                    break;
                }
                default:
                {
                    return false;
                }
            }

            if (Game.DungeonMap.SetActorPosition(Game.Player, x, y))
            {
                return true;
            }

            Monster monster = Game.DungeonMap.GetMonsterAt(x, y);
            if (monster != null)
            {
                CombatSys.Attack(Game.Player, monster);
                return true;
            }

            return false;
        }

        public void CloseMenu()
        {
            Globals.BuildingEntranceIsTriggered = false;
        }

        

        public void ActivateMonsters()
        {
          IScheduleable scheduleable = Game.SchedulingSystem.Get();
          if ( scheduleable is Player )
          {
            IsPlayerTurn = true;
            Game.SchedulingSystem.Add( Game.Player );
          }
          else if (scheduleable is Npc)
          {
                Npc Npc = scheduleable as Npc;

                if (Npc != null)
                {
                    Npc.PerformAction(this);
                    Game.SchedulingSystem.Add(Npc);
                }
                ActivateMonsters();
            }
          else
          {
            Monster monster = scheduleable as Monster;

            if ( monster != null )
            {
              monster.PerformAction( this );
              Game.SchedulingSystem.Add( monster );
            }

            ActivateMonsters();
          }
        }

        public void MoveMonster( Monster monster, ICell cell )
        {
          if ( !Game.DungeonMap.SetActorPosition( monster, cell.X, cell.Y ) )
          {
            if ( Game.Player.X == cell.X && Game.Player.Y == cell.Y )
            {
              CombatSys.Attack( monster, Game.Player );
            }
          }
        }

        public void MoveVillager(ICell npcCell, Npc villager, int randomDir)
        {
            if (!Game.DungeonMap.SetActorPosition(villager, npcCell.X, npcCell.Y))
            {
                int npcX = npcCell.X;
                int npcY = npcCell.Y;
                
                //Console.WriteLine(randomDir);
                if (randomDir == 1)
                    Game.DungeonMap.SetActorPosition(villager, npcX, npcY - 1);
                else if (randomDir == 2)
                    Game.DungeonMap.SetActorPosition(villager, npcX, npcY + 1);
                else if (randomDir == 3)
                    Game.DungeonMap.SetActorPosition(villager, npcX, npcY + 1);
                else if (randomDir == 4)
                    Game.DungeonMap.SetActorPosition(villager, npcX - 1, npcY);
            }
        }
    }
}
