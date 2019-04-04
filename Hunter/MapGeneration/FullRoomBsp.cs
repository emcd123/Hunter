using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLNET;
using RogueSharp;

using Hunter.Core;
using Hunter.Monsters;

namespace Hunter.MapGeneration
{
    public class FullRoomBsp : BaseMap
    {
        //private readonly IRandom _random;
        private readonly int _width;
        private readonly int _height;
        private readonly DungeonMap _map;
        public List<Rectangle> roomArr;
        public List<Cell> DoorCoords;        

        public FullRoomBsp(int width, int height)
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
                MakeRoom(Rooms[i]);
                if (i != 0 || i != Rooms.Count)
                    MakeDoor(_map, Rooms[i]);

                PlaceMonsters(_map, Rooms[i]);
            }
            PlaceBoss(_map, Rooms);
            PlaceItemsInRooms(_map, Rooms);
            //HACK HERE: Placeplayer returns an integer as a side effect of it purpose
            // In reality PlacePlayer is a void function that directly affects the map.
            // Needs Refactoring
            int RoomIndex = PlacePlayer(_map, Rooms);
            CreateUpStairs(_map, Rooms, RoomIndex);
            return _map;
        }

        private List<Rectangle> SplitAction(List<Rectangle> RoomsList)
        {
            List<Rectangle> IterRectangles = new List<Rectangle>();
            foreach (Rectangle o in RoomsList.ToList())
            {
                int number = GenerateRandomInt(1, 5);
                List<Rectangle> IterLi = new List<Rectangle>();
                if (number > 3 && o.Width >= 4)
                {
                    IterLi = SplitRectVertical(o);
                }
                else if (o.Height >= 4)
                {
                    IterLi = SplitRectHorizontal(o);
                }
                else
                    IterLi = new List<Rectangle> { o };

                for (int i = 0; i < IterLi.Count; i++)
                {
                    //Console.WriteLine(IterLi[i]);
                    IterRectangles.Add(IterLi[i]);
                }
            }
            return IterRectangles;
        }

        private List<Rectangle> SplitRectVertical(Rectangle rect)
        {
            int width = rect.Width;
            int height = rect.Height;

            Rectangle rect1;
            Rectangle rect2;
            
            int xBreak = GenerateRandomInt(2, width-2);
                
            rect1 = new Rectangle(rect.Left, rect.Top, xBreak, rect.Height);
            rect2 = new Rectangle(rect.Left + xBreak, rect.Top, rect.Width - xBreak, rect.Height);

            return new List<Rectangle>() { rect1, rect2 };
        }

        private List<Rectangle> SplitRectHorizontal(Rectangle rect)
        {
            int width = rect.Width;
            int height = rect.Height;

            Rectangle rect1;
            Rectangle rect2;

            int yBreak = GenerateRandomInt(2, height-2);
            rect1 = new Rectangle(rect.Left, rect.Top, width, yBreak);
            rect2 = new Rectangle(rect.Left, rect.Top + yBreak, width, rect.Height - yBreak);    

            return new List<Rectangle>() { rect1, rect2 };
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
    }
}
