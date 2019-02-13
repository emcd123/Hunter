﻿using System;
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

        public DungeonMap CreateMap()
        {            
            //Intialise a map with correct width and height
            _map.Initialize(_width, _height);
            
            List<Rectangle> Rooms = new List<Rectangle>();

            int roomXCoord = 0;
            int roomYCoord = 0;

            var FullRectangle = new Rectangle(roomXCoord, roomYCoord, _width, _height);
            MakeExteriorWall(FullRectangle);
            Rooms.Add(FullRectangle);            
            for (int i = 0; i < 2; i++)
            {
                //Idea: This function needs to return a list of rectangles
                // which overwrite the original list passed as an argument
                Rooms = RectSplitIteration(Rooms);
                Console.WriteLine(Rooms.Count);
            }
            Console.WriteLine(Rooms.Count);

            foreach (Rectangle o in Rooms)
            {
                Console.WriteLine(o);
                MakeRoom(_map, o);
            }

            PlacePlayer(_map, Rooms);
            return _map;
        }

        public void MakeExteriorWall(Rectangle FullRectangle)
        {
            int height = FullRectangle.Height;
            int width = FullRectangle.Width;
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
        private void MakeRoom(DungeonMap map, Rectangle room)
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

            xi = room.Right - 1;
            yi = room.Bottom - 1;

            for (int x = room.Left + 1; x < room.Right; x++)
            {
                map.SetCellProperties(x, yi, false, false, true);
            }
            for (int y = room.Top + 1; y < room.Bottom; y++)
            {
                map.SetCellProperties(xi, y, false, false, true);
            }

            //for (int x = room.Left + 1; x < room.Right-1; x++)
            //{
            //    for (int y = room.Top + 1; y < room.Bottom-1; y++)
            //    {
            //        map.SetCellProperties(x, y, true, true, true);
            //    }
            //}
        }

        public List<Rectangle> RectSplitIteration(List<Rectangle> Slice)
        {
            int startSize = Slice.Count;
            List<Rectangle> IterRectangles = new List<Rectangle>(); 
            int number = GenerateRandomInt(1, 5);
            foreach (Rectangle o in Slice.ToList())
            {
                number = GenerateRandomInt(1, 5);
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
                    IterRectangles.Add(IterLi[i]);
                }
            }
            return IterRectangles;
        }

        public List<Rectangle> SplitRectVertical(Rectangle rect)
        {
            int width = rect.Width;
            int height = rect.Height;

            int xBreak = 0;
            if(width >= 4)
            {
                xBreak = GenerateRandomInt(2, width-2);
                var Segment1 = new Rectangle(rect.Left, rect.Top, xBreak, height);
                var Segment2 = new Rectangle(xBreak, rect.Top, width - xBreak, height);

                List<Rectangle> rect_li = new List<Rectangle>() { Segment1, Segment2 };
                return rect_li;
            }
            else
            {
                var Segment1 = rect;
                var Segment2 = rect;

                List<Rectangle> rect_li = new List<Rectangle>() { Segment1, Segment2 };
                return rect_li;
            }
        }

        public List<Rectangle> SplitRectHorizontal(Rectangle rect)
        {
            int width = rect.Width;
            int height = rect.Height;

            int yBreak = 0;
            if (height >= 4)
            {
                yBreak = GenerateRandomInt(2, height-2);
                var Segment1 = new Rectangle(rect.Left, rect.Top, width, yBreak);
                var Segment2 = new Rectangle(rect.Left, yBreak, width, height - yBreak);

                List<Rectangle> rect_li = new List<Rectangle>() { Segment1, Segment2 };
                return rect_li;
            }
            else
            {
                var Segment1 = rect;
                var Segment2 = rect;

                List<Rectangle> rect_li = new List<Rectangle>() { Segment1, Segment2 };
                return rect_li;
            }

        }

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

        public int GenerateRandomInt(int min, int max)
        {
            int num = rnd.Next(min, max);
            return num;
        }
    }
}
