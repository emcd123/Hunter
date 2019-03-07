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

namespace Hunter.MapGeneration
{
    public class SimpleBsp : BaseMap
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
            MakeExteriorWall(_map, FullRectangle);
            Rooms.Add(FullRectangle);

            for (int i = 0; i < 4; i++)
            {
                Rooms = SplitAction(Rooms);
            }
            for (int i = 0; i < Rooms.Count; i++)
            {
                Console.WriteLine(Rooms[i]);
                MakeRoom(_map, Rooms[i]);
                if(i != 0 || i != Rooms.Count)
                    MakeDoor(_map, Rooms[i]);

                PlaceMonsters(_map, Rooms[i]);
            }
            //CreateStairs(Rooms);
            PlacePlayer(_map, Rooms);            
            return _map;
        }

        private void CreateStairs(List<Rectangle> Rooms)
        {
            _map.StairsUp = new Stairs
            {
                X = Rooms.First().Center.X + 1,
                Y = Rooms.First().Center.Y,
                IsUp = true
            };
            _map.StairsDown = new Stairs
            {
                X = Rooms.Last().Center.X,
                Y = Rooms.Last().Center.Y,
                IsUp = false
            };
        }

        private List<Rectangle> SplitAction(List<Rectangle> RoomsList)
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

        private List<Rectangle> SplitRectVertical(Rectangle rect)
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

        private List<Rectangle> SplitRectHorizontal(Rectangle rect)
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


        private void MakeRoom(DungeonMap map, Rectangle rm)
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

        private void PlaceMonsters(DungeonMap map, Rectangle room)
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
                        var monster = Kobold.Create(1);
                        monster.X = randomRoomLocation.X;
                        monster.Y = randomRoomLocation.Y;
                        map.AddMonster(map, monster);
                    }
                }
            }
        }
    }
}

