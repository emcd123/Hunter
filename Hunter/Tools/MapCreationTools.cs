using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLNET;
using RogueSharp;

using Hunter.Core;

namespace Hunter.Tools
{
    public class MapCreationTools
    {
        //private readonly IRandom _random;
        private readonly int _width;
        private readonly int _height;
        private readonly int _maxRooms;
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;
        private readonly DungeonMap _map;

        private readonly int _roomSize = 10;
        public List<Rectangle> roomArr;
        private Random rnd = new Random();

        public MapCreationTools(int width, int height, int maxRooms, int roomMaxSize, int roomMinSize, int roomSize = 10)
        {
            _width = width;
            _height = height;
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;
            _map = new DungeonMap();
            _roomSize = roomSize;
  
        }

        public int GenerateRandomInt(int min, int max)
        {
            int num = rnd.Next(min, max);
            return num;
        }

        public DungeonMap CreateMap()
        {            
        //Intialise a map with correct width and height
            _map.Initialize(_width, _height);

            List<Rectangle> Rooms = new List<Rectangle>();

            int roomXCoord = 0;
            int roomYCoord = 0;

            var FullRectangle = new Rectangle(roomXCoord, roomYCoord, _width, _height);
            Rooms.Add(FullRectangle);

            ////Segments = SplitRect(FullRectangle);

            RectSplitIteration(Rooms);
            for (int i = 0; i < 3; i++)
            {
                RectSplitIteration(Rooms);
                
            }
            Console.WriteLine(Rooms.Count);

            foreach (Rectangle o in Rooms)
            {
                Console.WriteLine(o);
                MakeRoom(_map, o);
            }

            //Console.WriteLine(GenerateRandomInt(1, 100));
            //for (int i = 0; i < _maxRooms; i++)
            //{
            //    CreateRectangleRoom(roomXCoord, roomYCoord);
            //    int increment = _roomSize + 10;
            //    roomXCoord += increment;
            //}
            PlacePlayer(_map, Rooms);
            return _map;
        }

        public static void RectangleIteration(Rectangle Slice)
        {
            for (int i = 0; i < 3; i++)
            {
                
            }
        }



        private static void MakeRoom(DungeonMap map, Rectangle room)
        {
            int xi = room.Left;
            int yi = room.Top;

            for (int x = room.Left + 1; x < room.Right; x++)
            {
                map.SetCellProperties(x, yi, false, false, true);
            }
            for (int y = room.Top + 1; y < room.Bottom; y++)
            {
                map.SetCellProperties(xi, y, false, false, true);
            }

            xi = room.Right-1;
            yi = room.Bottom-1;

            for (int x = room.Left + 1; x < room.Right; x++)
            {
                map.SetCellProperties(x, yi, false, false, true);
            }
            for (int y = room.Top + 1; y < room.Bottom; y++)
            {
                map.SetCellProperties(xi, y, false, false, true);
            }

            for (int x = room.Left + 1; x < room.Right-1; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom-1; y++)
                {
                    map.SetCellProperties(x, y, true, true, true);
                }
            }
        }

        public List<Rectangle> SplitRectVertical(Rectangle rect)
        {
            int width = rect.Width;
            int height = rect.Height;

            int xBreak = GenerateRandomInt(1, width);
            //int xBreak = width / 2;
            int yBreak = height / 2;

            var Segment1 = new Rectangle(rect.Left, rect.Top, xBreak, height);
            var Segment2 = new Rectangle(xBreak, rect.Top, width - xBreak, height);

            List<Rectangle> rect_li = new List<Rectangle>() { Segment1, Segment2 };
            return  rect_li; 
        }

        public List<Rectangle> SplitRectHorizontal(Rectangle rect)
        {
            int width = rect.Width;
            int height = rect.Height;

            int yBreak = GenerateRandomInt(1, height);
            int xBreak = width / 2;
            //nt yBreak = height / 2;

            var Segment1 = new Rectangle(rect.Left, rect.Top, width, yBreak);
            var Segment2 = new Rectangle(rect.Left, yBreak, width, height-yBreak);

            List<Rectangle> rect_li = new List<Rectangle>() { Segment1, Segment2 };
            return rect_li;
        }

        public void RectSplitIteration(List<Rectangle> Slice)
        {
            //TO FIX: Slows compilation and will impact later when have multiple floors.
            // Sloppy fix to stop multiple split in some orientation causing too small
            //rooms to be generated
            System.Threading.Thread.Sleep(1000);
            int startSize = Slice.Count;
            int number = GenerateRandomInt(1, 5);
            foreach (Rectangle o in Slice.ToList())
            {
                List<Rectangle> IterLi = new List<Rectangle>();
                if (number > 3)
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
                    Slice.Add(IterLi[i]);
                    //List<Cell> borderCells = _map.GetCellsAlongLine(IterLi[i].Left, IterLi[i].Top, IterLi[i].Right, IterLi[i].Bottom).ToList();
                }                
            }
        }

        public void FillInWalls(List<Rectangle> Slice)
        {

            List<Rectangle> WallCoordLi = new List<Rectangle>();
            for (int i = 0; i < WallCoordLi.Count; i++)
            {
                
                //List<Cell> borderCells = _map.GetCellsAlongLine(WallCoordLi[i].Left, WallCoordLi[i].Top, WallCoordLi[i].Right, WallCoordLi[i].Bottom).ToList();
            }
        }
        //Create a rectangular room.
        public void CreateRectangleRoom(DungeonMap map, Rectangle newRoom)
        {
            int xCoord = newRoom.Left;
            int yCoord = newRoom.Top;

            for (int x = newRoom.Left + 1; x < newRoom.Right; x++)
            {
                for (int y = newRoom.Top + 1; y < newRoom.Bottom; y++)
                {
                    map.SetCellProperties(x, y, true, true, true);
                }
            }
            for (int x = newRoom.Left; x < newRoom.Right; x++)
            {
                map.SetCellProperties(x, yCoord, false, false, true);
            }
            for (int y = newRoom.Top; y < newRoom.Bottom; y++)
            {
                map.SetCellProperties(xCoord, y, false, false, true);
            }
            for (int x = newRoom.Left; x < newRoom.Right; x++)
            {
                map.SetCellProperties(x, yCoord + _roomSize, false, false, true);
            }
            for (int y = newRoom.Top; y < newRoom.Bottom; y++)
            {
                map.SetCellProperties(xCoord + _roomSize, y, false, false, true);
            }
        }

        //public Rectangle[] CreateRect(int xCoord, int yCoord, int width, int height)
        //{
        //    var newRoom = new Rectangle(xCoord, yCoord, width, height);
        //    Rectangle[] roomArray = new Rectangle[] { newRoom };
        //    return roomArray;
        //}

        public void PlacePlayer(DungeonMap map, List<Rectangle> roomArray)
        {
            int size = roomArray.Count;
            Player player = Game.Player;
            int X = roomArray[size-1].Center.X;
            int Y = roomArray[size-1].Center.Y;
            map.SetActorPosition(player, X, Y);
        }


        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                _map.SetCellProperties(x, yPosition, true, true);
            }
        }

        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                _map.SetCellProperties(xPosition, y, true, true);
            }
        }
    }
}
