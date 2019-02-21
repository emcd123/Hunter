using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLNET;
using RogueSharp;

using Hunter.Core;
using RogueSharp.DiceNotation;
using Hunter.Monsters;

namespace Hunter.Tools
{
    public class SimpleBsp
    {
        private readonly int _width;
        private readonly int _height;
        private readonly DungeonMap _map;
        public List<Rectangle> roomArr;
        public List<Cell> DoorCoords;
        private Random rnd = new Random();
        private bool flag = true;
        private bool test = true;

        public SimpleBsp(int width, int height)
        {
            _width = width;
            _height = height;
            _map = new DungeonMap();

        }
        public DungeonMap CreateMap()
        {
            //Intialise a map with correct width and height
            _map.Initialize(_width, _height);

            //Make a list meant for the rooms
            List<Rectangle> Rooms = new List<Rectangle>();

            var FullRectangle = new Rectangle(0, 0, _width, _height);
            MakeExteriorWall(FullRectangle);
            Rooms.Add(FullRectangle);

            for (int i = 0; i < 4; i++)
            {
                Rooms = SplitAction(Rooms);
            }
            for (int i = 0; i < Rooms.Count; i++)
            {
                Console.WriteLine(Rooms[i]);
                MakeRoom(Rooms[i]);
                if(i != 0 || i != Rooms.Count)
                    MakeDoor(Rooms[i]);

                PlaceMonsters(Rooms[i]);
            }

            PlacePlayer(_map, Rooms);            
            return _map;
        }

        private void MakeDoor(Rectangle ROOM)
        {

            int DoorCoordX = ROOM.Left;
            int DoorCoordY = ROOM.Center.Y;
            if (DoorCoordX != 0)
            {
                _map.SetCellProperties(DoorCoordX, DoorCoordY, false, true, true);
                _map.Doors.Add(new Door
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
                _map.SetCellProperties(DoorCoordX, DoorCoordY, false, true, true);
                _map.Doors.Add(new Door
                {
                    X = DoorCoordX,
                    Y = DoorCoordY,
                    IsOpen = false
                });
            }
        }

        private void MakeRoom(Rectangle rm)
        {
            int xi = rm.Left;
            int yi = rm.Top;

            for (int x = rm.Left; x < rm.Right; x++)
            {
                _map.SetCellProperties(x, yi, false, false, true);
            }
            for (int y = rm.Top; y < rm.Bottom; y++)
            {
                _map.SetCellProperties(xi, y, false, false, true);
            }
        }

        public List<Rectangle> SplitAction(List<Rectangle> RoomsList)
        {
            List<Rectangle> IterRectangles = new List<Rectangle>();
            foreach (Rectangle o in RoomsList.ToList())
            {                
                //int number = GenerateRandomInt(1, 5);
                List<Rectangle> IterLi = new List<Rectangle>();
                if (flag == true)
                {
                    IterLi = SplitRectVertical(o);
                }
                else
                {
                    IterLi = SplitRectHorizontal(o);
                }
                for (int i = 0; i < IterLi.Count; i++)
                {
                    //Console.WriteLine(IterLi[i]);
                    IterRectangles.Add(IterLi[i]);
                }
                
            }
            if (flag == true)
                flag = false;
            else
                flag = true;
            return IterRectangles;
        }

        public List<Rectangle> SplitRectVertical(Rectangle rect)
        {
            int width = rect.Width;
            int height = rect.Height;

            Rectangle rect1;
            Rectangle rect2;
            
            int xBreak = width / 2;

            rect1 = new Rectangle(rect.Left, rect.Top, xBreak, rect.Height);
            rect2 = new Rectangle(rect.Left+xBreak, rect.Top, rect.Width - xBreak, rect.Height);

            return new List<Rectangle>() { rect1, rect2 };
        }

        public List<Rectangle> SplitRectHorizontal(Rectangle rect)
        {
            int width = rect.Width;
            int height = rect.Height;

            Rectangle rect1;
            Rectangle rect2;
            
            int yBreak = height/2;

            rect1 = new Rectangle(rect.Left, rect.Top, width, yBreak);
            rect2 = new Rectangle(rect.Left, rect.Top + yBreak, width, rect.Height - yBreak);

            return new List<Rectangle>() { rect1, rect2 };
        }

        public void MakeExteriorWall(Rectangle rect)
        {
            int height = rect.Height;
            int width = rect.Width;
            foreach (Cell cell in _map.GetAllCells())
            {
                _map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }
            foreach (Cell cell in _map.GetCellsInRows(0, height - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            // Set the first and last columns in the map to not be transparent or walkable
            foreach (Cell cell in _map.GetCellsInColumns(0, width - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }
        }

        public void PlacePlayer(DungeonMap map, List<Rectangle> roomArray)
        {
            int size = roomArray.Count;
            Player player = Game.Player;
            int X = roomArray[size - 1].Center.X;
            int Y = roomArray[size - 1].Center.Y;
            map.SetActorPosition(player, X, Y);
        }

        private void PlaceMonsters(Rectangle room)
        {
            // Each room has a 60% chance of having monsters
            if (Dice.Roll("1D10") < 7)
            {
                // Generate between 1 and 4 monsters
                var numberOfMonsters = Dice.Roll("1D4");
                for (int i = 0; i < numberOfMonsters; i++)
                {
                    // Find a random walkable location in the room to place the monster
                    Point randomRoomLocation = _map.GetRandomWalkableLocationInRoom(room);
                    // It's possible that the room doesn't have space to place a monster
                    // In that case skip creating the monster
                    if (randomRoomLocation != null)
                    {
                        // Temporarily hard code this monster to be created at level 1
                        var monster = Kobold.Create(1);
                        monster.X = randomRoomLocation.X;
                        monster.Y = randomRoomLocation.Y;
                        _map.AddMonster(_map, monster);
                    }
                }
            }
            if (test == true)
            {
                // Generate between 1 and 4 monsters
                var numberOfMonsters = 1;
                for (int i = 0; i < numberOfMonsters; i++)
                {
                    // Find a random walkable location in the room to place the monster
                    Point randomRoomLocation = _map.GetRandomWalkableLocationInRoom(room);
                    // It's possible that the room doesn't have space to place a monster
                    // In that case skip creating the monster
                    if (randomRoomLocation != null)
                    {
                        // Temporarily hard code this monster to be created at level 1
                        var monster = Kobold.Create(1);
                        monster.X = randomRoomLocation.X;
                        monster.Y = randomRoomLocation.Y;
                        _map.AddMonster(_map, monster);
                    }
                }
            }
        }

        public int GenerateRandomInt(int min, int max)
        {
            int num = rnd.Next(min, max);
            return num;
        }
    }
}

