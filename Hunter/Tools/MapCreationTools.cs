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
        private Rectangle[] roomArray;
        

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


        public DungeonMap CreateMap()
        {            
        //Intialise a map with correct width and height
            _map.Initialize(_width, _height);

            List<Rectangle> Segments = new List<Rectangle>();

            int roomXCoord = 0;
            int roomYCoord = 0;

            var FullRectangle = new Rectangle(roomXCoord, roomYCoord, _width, _height);
            Segments.Add(FullRectangle);

            //Segments = SplitRect(FullRectangle);
            for (int i = 0; i < 2; i++)
            {
                RectSplitIteration(Segments);
            }
            Console.WriteLine(Segments.Count);

            foreach (object o in Segments)
            {
                Console.WriteLine(o);
            }


            //for (int i = 0; i < _maxRooms; i++)
            //{
            //    CreateRectangleRoom(roomXCoord, roomYCoord);
            //    int increment = _roomSize + 10;
            //    roomXCoord += increment;
            //}
            //PlacePlayer(_map);
            return _map;
        }

        public List<Rectangle> SplitRect(Rectangle rect)
        {
            int width = rect.Width;
            int height = rect.Height;
            int xBreak = width / 2;
            int yBreak = height / 2;

            var Segment1 = new Rectangle(rect.Left, rect.Top, xBreak, height);
            var Segment2 = new Rectangle(xBreak, rect.Top, width - xBreak, height);

            List<Rectangle> rect_li = new List<Rectangle>() { Segment1, Segment2 };
            return  rect_li; 
        }

        public void RectSplitIteration(List<Rectangle> Slice)
        {
            foreach (Rectangle o in Slice.ToList())
            {
                List<Rectangle> IterLi = new List<Rectangle>();
                IterLi = SplitRect(o);
                for (int i = 0; i < IterLi.Count; i++)
                {
                    //Console.WriteLine(IterLi[i]);
                    Slice.Add(IterLi[i]);
                    //List<Cell> borderCells = _map.GetCellsAlongLine(IterLi[i].Left, IterLi[i].Top, IterLi[i].Right, IterLi[i].Bottom).ToList();
                }
            }
        }

        ////Create a rectangular room.
        //public void CreateRectangleRoom(int roomXCoord, int roomYCoord)
        //{
        //    int xCoord = roomXCoord;
        //    int yCoord = roomYCoord;

        //    roomArray = CreateRect(xCoord, yCoord, _roomSize, _roomSize);
        //    Rectangle newRoom = roomArray[0];

        //    for (int x = newRoom.Left + 1; x < newRoom.Right; x++)
        //    {
        //        for (int y = newRoom.Top + 1; y < newRoom.Bottom; y++)
        //        {
        //            _map.SetCellProperties(x, y, true, true, true);
        //        }
        //    }
        //    for (int x = newRoom.Left; x < newRoom.Right + 1; x++)
        //    {
        //        _map.SetCellProperties(x, yCoord, false, false, true);
        //    }
        //    for (int y = newRoom.Top; y < newRoom.Bottom + 1; y++)
        //    {
        //        _map.SetCellProperties(xCoord, y, false, false, true);
        //    }
        //    for (int x = newRoom.Left; x < newRoom.Right + 1; x++)
        //    {
        //        _map.SetCellProperties(x, yCoord + _roomSize, false, false, true);
        //    }
        //    for (int y = newRoom.Top; y < newRoom.Bottom + 1; y++)
        //    {
        //        _map.SetCellProperties(xCoord + _roomSize, y, false, false, true);
        //    }
        //}

        //public Rectangle[] CreateRect(int xCoord, int yCoord, int width, int height)
        //{
        //    var newRoom = new Rectangle(xCoord, yCoord, width, height);
        //    Rectangle[] roomArray = new Rectangle[] { newRoom };
        //    return roomArray;
        //}

        public void PlacePlayer(DungeonMap map)
        {
            Player player = Game.Player;
            int X = roomArray[0].Center.X;
            int Y = roomArray[0].Center.Y;
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
