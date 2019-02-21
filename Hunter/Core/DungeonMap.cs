using System;
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

        public DungeonMap()
        { 
            Doors = new List<Door>();
        }

        // Return the door at the x,y position or null if one is not found.
        public Door GetDoor(int x, int y)
        {
            return Doors.SingleOrDefault(d => d.X == x && d.Y == y);
        }

        // The Draw method will be called each time the map is updated
        // It will render all of the symbols/colors for each cell to the map sub console
        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
            foreach (Door door in Doors)
            {
                door.Draw(mapConsole, this);
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
                return true;
            }
            return false;            
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
    }
}
