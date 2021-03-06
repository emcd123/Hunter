﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLNET;
using RogueSharp;

namespace Hunter.Core
{
    // Our custom DungeonMap class extends the base RogueSharp Map class
    public class DungeonMap : Map
    {
        private bool firstRun;
        public List<Door> Doors { get; set; }
        public List<BuildingEntrance> BuildingEntrances { get; set; }
        public List<Stairs> StairsUpList { get; set; }
        public List<Stairs> StairsDownList { get; set; }

        private readonly List<Monster> _monsters;
        private readonly List<Npc> _villagers;

        public DungeonMap()
        {
            Game.SchedulingSystem.Clear();
            Doors = new List<Door>();
            BuildingEntrances = new List<BuildingEntrance>();
            StairsUpList = new List<Stairs>();
            StairsDownList = new List<Stairs>();
            _monsters = new List<Monster>();
            _villagers = new List<Npc>();
        }

        public bool CanMoveDownToNextLevel()
        {
            //Player player = Game.Player;
            //return StairsDown.X == player.X && StairsDown.Y == player.Y;
            return true;
        }

        // The Draw method will be called each time the map is updated
        // It will render all of the symbols/colors for each cell to the map sub console
        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
            foreach (Door door in Doors)
            {
                door.Draw(mapConsole, this);
            }
            foreach (BuildingEntrance buildingEntrance in BuildingEntrances)
            {
                buildingEntrance.Draw(mapConsole, this);
            }
            foreach (Stairs upstair in StairsUpList)
            {
                upstair.Draw(mapConsole, this);
            }
            foreach (Stairs downstair in StairsDownList)
            {
                downstair.Draw(mapConsole, this);
            }

            // Keep an index so we know which position to draw monster stats at
            int i = 0;
            // Iterate through each monster on the map and draw it after drawing the Cells
            foreach (Monster monster in _monsters)
            {
                monster.Draw(mapConsole, this);
                // When the monster is in the field-of-view also draw their stats
                if (IsInFov(monster.X, monster.Y))
                {
                    // Pass in the index to DrawStats and increment it afterwards
                    monster.DrawStats(statConsole, i);
                    i++;
                }
            }

            foreach (Npc villager in _villagers)
            {
                villager.Draw(mapConsole, this);
            }
        }

        private void SetConsoleSymbolForCell(RLConsole console, Cell cell)
        {
            // When we haven't explored a cell yet, we don't want to draw anything
            if (!cell.IsExplored)
            {
                return;
            }

            // When a cell is currently in the field-of-view it should be drawn with ligher colors
            if (IsInFov(cell.X, cell.Y))
            {
                // Choose the symbol to draw based on if the cell is walkable or not
                // '.' for floor and '#' for walls
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }                
            }
            else
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                }

                else
                {
                    console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                }
            }            
        }

        // This method will be called any time we move the player to update field-of-view
        public void UpdatePlayerFieldOfView()
        {
            Player player = Game.Player;
            // Compute the field-of-view based on the player's location and awareness
            ComputeFov(player.X, player.Y, player.Awareness, true);
            // Mark all cells in field-of-view as having been explored
            foreach (Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }

        // Returns true when able to place the Actor on the cell or false otherwise
        public bool SetActorPosition(Actor actor, int x, int y)
        {
            // Only allow actor placement if the cell is walkable
            if (GetCell(x, y).IsWalkable)
            {
                // The cell the actor was previously on is now walkable
                if (firstRun)
                {
                    SetCellProperties(actor.X, actor.Y, GetCell(actor.X, actor.Y).IsTransparent, true, GetCell(actor.X, actor.Y).IsExplored);
                    firstRun = false;
                }
                firstRun = true;
                // Update the actor's position
                actor.X = x;
                actor.Y = y;
                // The new cell the actor is on is now not walkable
                SetCellProperties(actor.X, actor.Y, GetCell(actor.X, actor.Y).IsTransparent, false, GetCell(actor.X, actor.Y).IsExplored);
                // Don't forget to update the field of view if we just repositioned the player
                if (actor is Player)
                {
                    UpdatePlayerFieldOfView();
                }
                OpenDoor(actor, x, y);
                OpenBuildingEntrance(actor, x, y);
                return true;
            }
            return false;            
        }

        // Return the door at the x,y position or null if one is not found.
        public Door GetDoor(int x, int y)
        {
            return Doors.SingleOrDefault(d => d.X == x && d.Y == y);
        }

        // The actor opens the door located at the x,y position
        private void OpenDoor(Actor actor, int x, int y)
        {
            Door door = GetDoor(x, y);
            if (door != null && !door.IsOpen)
            {
                door.IsOpen = true;
                var cell = GetCell(x, y);
                // Once the door is opened it should be marked as transparent and no longer block field-of-view
                SetCellProperties(x, y, true, cell.IsWalkable, cell.IsExplored);
            }
        }

        // Return the door at the x,y position or null if one is not found.
        public BuildingEntrance GetBuildingEntrance(int x, int y)
        {
            return BuildingEntrances.SingleOrDefault(b => b.X == x && b.Y == y);
        }

        // The actor opens the door located at the x,y position
        private void OpenBuildingEntrance(Actor actor, int x, int y)
        {
            BuildingEntrance buildingEntrance = GetBuildingEntrance(x, y);
            if (buildingEntrance != null && !buildingEntrance.IsTriggered)
            {
                if (buildingEntrance.Symbol == '1')
                    Globals.SheriffTriggered = true;
                else
                    Globals.GenericMenuTriggered = true;

                Globals.BuildingEntranceIsTriggered = true;
                var cell = GetCell(x, y);
            }
        }

        public void AddVillager(DungeonMap map, Npc villager)
        {
            _villagers.Add(villager);
            // After adding the monster to the map make sure to make the cell not walkable
            map.SetCellProperties(villager.X, villager.Y, true, false, true);
            Game.SchedulingSystem.Add(villager);
        }

        public void AddMonster(DungeonMap map, Monster monster)
        {
            _monsters.Add(monster);
            // After adding the monster to the map make sure to make the cell not walkable
            map.SetCellProperties(monster.X, monster.Y, true, false, true);
            Game.SchedulingSystem.Add(monster);
        }

        public void RemoveMonster(Monster monster)
        {
            _monsters.Remove(monster);
            // After removing the monster from the map, make sure the cell is walkable again
            SetCellProperties(monster.X, monster.Y, true, true, true);
            Game.SchedulingSystem.Remove(monster);
        }

        public void AddBoss(DungeonMap map, Monster monster)
        {
            _monsters.Add(monster);
            // After adding the monster to the map make sure to make the cell not walkable
            map.SetCellProperties(monster.X, monster.Y, true, false, true);
            Game.SchedulingSystem.Add(monster);
        }

        public void RemoveBoss(Monster monster)
        {
            _monsters.Remove(monster);
            // After removing the monster from the map, make sure the cell is walkable again
            SetCellProperties(monster.X, monster.Y, true, true, true);
            Game.SchedulingSystem.Remove(monster);
        }

        public Monster GetMonsterAt(int x, int y)
        {
            return _monsters.FirstOrDefault(m => m.X == x && m.Y == y);
        }

        // Look for a random location in the room that is walkable.
        public Point GetRandomWalkableLocationInRoom(Rectangle room)
        {
            //if (DoesRoomHaveWalkableSpace(room))
            //{
            for (int i = 0; i < 100; i++)
            {
                int x = GenerateRandomInt(1, room.Width - 1) + room.X;
                int y = GenerateRandomInt(1, room.Height - 1) + room.Y;
                if (IsWalkable(x, y))
                {
                    return new Point(x, y);
                }                
            }
                
            return new Point(room.Center.X, room.Center.Y);                
            //}
            // If we didn't find a walkable location in the room return null
            //return null;
        }

        public int GenerateRandomInt(int min, int max)
        {
            int num = Game.rng.Next(min, max);
            return num;
        }
    }
}
