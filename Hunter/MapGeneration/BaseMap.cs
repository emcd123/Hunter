using Hunter.Core;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.MapGeneration
{
    public class BaseMap
    {
        public BaseMap()
        {          

        }

        public int GenerateRandomInt(int min, int max)
        {
            int num = Game.rng.Next(min, max);
            return num;
        }

        public void PlacePlayer(DungeonMap map, List<Rectangle> roomArray)
        {
            int size = roomArray.Count;
            Player player = Game.Player;
            int X = roomArray[size - 1].Center.X;
            int Y = roomArray[size - 1].Center.Y;
            map.SetActorPosition(player, X, Y);
            Game.SchedulingSystem.Add(player);
        }

        public void CreateStairs(DungeonMap map, List<Rectangle> Rooms)
        {
            map.StairsUp = new Stairs
            {
                X = Rooms.First().Center.X + 1,
                Y = Rooms.First().Center.Y,
                IsUp = true
            };
            map.StairsDown = new Stairs
            {
                X = Rooms.Last().Center.X,
                Y = Rooms.Last().Center.Y,
                IsUp = false
            };
        }

        public void MakeDoor(DungeonMap map, Rectangle ROOM)
        {
            int DoorCoordX = ROOM.Left;
            int DoorCoordY = ROOM.Center.Y;
            if (DoorCoordX != 0)
            {
                map.SetCellProperties(DoorCoordX, DoorCoordY, false, true, true);
                map.Doors.Add(new Door
                {
                    X = DoorCoordX,
                    Y = DoorCoordY,
                    IsOpen = false
                });
            }

            DoorCoordX = ROOM.Center.X;
            DoorCoordY = ROOM.Top;
            if (DoorCoordY != 0)
            {
                map.SetCellProperties(DoorCoordX, DoorCoordY, false, true, true);
                map.Doors.Add(new Door
                {
                    X = DoorCoordX,
                    Y = DoorCoordY,
                    IsOpen = false
                });
            }
        }

        public void MakeExteriorWall(DungeonMap map, Rectangle rect)
        {
            int height = rect.Height;
            int width = rect.Width;
            foreach (Cell cell in map.GetAllCells())
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }
            foreach (Cell cell in map.GetCellsInRows(0, height - 1))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            // Set the first and last columns in the map to not be transparent or walkable
            foreach (Cell cell in map.GetCellsInColumns(0, width - 1))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }
        }
    }
}
