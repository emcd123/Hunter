using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Core;
using RLNET;
using RogueSharp;

namespace Hunter.Unused
{
    class MapMethods : Map
    {

        public List<Door> PopulateDoors(Map map)
        {
            List<Door> DOORS = new List<Door>();
            foreach (Cell cell in map.GetAllCells())
            {
                if (IsDoor(cell.X, cell.Y))
                {
                    Door d = new Door();
                    map.SetCellProperties(cell.X, cell.Y, false, true);
                    DOORS.Add(new Door
                    {
                        X = cell.X,
                        Y = cell.Y,
                        IsOpen = false
                    });
                    DOORS.Add(d);
                }
            }
            return DOORS;
        }

        public bool IsDoor(int x, int y)
        {
            if (GetCell(x, y).IsWalkable)
            {
                if (!GetCell(x, y + 1).IsWalkable && !GetCell(x, y - 1).IsWalkable && GetCell(x + 1, y).IsWalkable && GetCell(x - 1, y).IsWalkable)
                {
                    return true;
                }
                else if (GetCell(x, y + 1).IsWalkable && GetCell(x, y - 1).IsWalkable && !GetCell(x + 1, y).IsWalkable && !GetCell(x - 1, y).IsWalkable)
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
    }
}
