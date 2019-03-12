using Hunter.Core;
using Hunter.Monsters;
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
        private bool test = true;

        public BaseMap()
        {          

        }

        public int GenerateRandomInt(int min, int max)
        {
            int num = Game.rng.Next(min, max);
            return num;
        }

        public int PlacePlayer(DungeonMap map, List<Rectangle> roomArray)
        {
            int size = roomArray.Count;
            int RoomIndex = GenerateRandomInt(0, roomArray.Count);
            Player player = Game.Player;
            int X = roomArray[RoomIndex].Center.X;
            int Y = roomArray[RoomIndex].Center.Y;
            map.SetActorPosition(player, X, Y);
            Game.SchedulingSystem.Add(player);

            return RoomIndex;
        }

        public void CreateUpStairs(DungeonMap map, List<Rectangle> Rooms, int RoomNumber)
        {
            map.StairsUpList.Add(new Stairs
            {
                X = Rooms[RoomNumber].Center.X + 1,
                Y = Rooms[RoomNumber].Center.Y,
                IsUp = true
            });
        }

        public void CreateDownStairs(DungeonMap map, List<Rectangle> Rooms)
        {
            map.StairsDownList.Add(new Stairs
            {
                X = Rooms.Last().Center.X,
                Y = Rooms.Last().Center.Y,
                IsUp = false
            });
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


        public void PlaceMonsters(DungeonMap map, Rectangle room)
        {
            // Each room has a 60% chance of having monsters
            //if (Dice.Roll("1D10") < 7)
            //{
            //    // Generate between 1 and 4 monsters
            //    var numberOfMonsters = Dice.Roll("1D4");
            //    for (int i = 0; i < numberOfMonsters; i++)
            //    {
            //        // Find a random walkable location in the room to place the monster
            //        Point randomRoomLocation = _map.GetRandomWalkableLocationInRoom(room);
            //        // It's possible that the room doesn't have space to place a monster
            //        // In that case skip creating the monster
            //        if (randomRoomLocation != null)
            //        {
            //            // Temporarily hard code this monster to be created at level 1
            //            var monster = Kobold.Create(1);
            //            monster.X = randomRoomLocation.X;
            //            monster.Y = randomRoomLocation.Y;
            //            _map.AddMonster(_map, monster);
            //        }
            //    }
            //}
            if (test == true)
            {
                // Generate between 1 and 4 monsters
                var numberOfMonsters = 1;
                for (int i = 0; i < numberOfMonsters; i++)
                {
                    // Find a random walkable location in the room to place the monster
                    Point randomRoomLocation = map.GetRandomWalkableLocationInRoom(room);
                    // It's possible that the room doesn't have space to place a monster
                    // In that case skip creating the monster
                    if (randomRoomLocation != null)
                    {
                        // Temporarily hard code this monster to be created at level 1
                        var monster = Goon.Create(1);
                        monster.X = randomRoomLocation.X;
                        monster.Y = randomRoomLocation.Y;
                        map.AddMonster(map, monster);
                    }
                }
            }
        }

        public void PlaceBoss(DungeonMap map, List<Rectangle> rooms)
        {            
            int roomIndex = GenerateRandomInt(0, rooms.Count);
            Console.WriteLine(roomIndex);
            Rectangle room = rooms[roomIndex];
            Point randomRoomLocation = map.GetRandomWalkableLocationInRoom(room);
            if (randomRoomLocation != null)
            {
                // Temporarily hard code this monster to be created at level 1
                var boss = Outlaw.Create(1);
                boss.X = randomRoomLocation.X;
                boss.Y = randomRoomLocation.Y;
                map.AddMonster(map, boss);
            }

        }
    }
}
