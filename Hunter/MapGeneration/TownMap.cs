using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLNET;
using RogueSharp;

using Hunter.Core;
using Hunter.MapGeneration;

namespace Hunter.Systems
{
    public class TownMap : BaseMap
    {
        private readonly int _width;
        private readonly int _height;
        public List<Cell> DoorCoords;

        private readonly DungeonMap _map;

        // Constructing a new MapGenerator requires the dimensions of the maps it will create
        public TownMap(int width, int height)
        {
            _width = width;
            _height = height;
            _map = new DungeonMap();
        }

        // Generate a new map that is a simple open floor with walls around the outside
        public DungeonMap CreateMap()
        {
            // Initialize every cell in the map by
            // setting walkable, transparency, and explored to true
            _map.Initialize(_width, _height);

            //Make a list meant for the rooms
            List<Rectangle> Rooms = new List<Rectangle>();

            var FullRectangle = new Rectangle(0, 0, _width, _height);
            MakeExteriorWall(_map, FullRectangle);
            Rooms.Add(FullRectangle);            

            for (int i = 0; i < Rooms.Count; i++)
            {
                Console.WriteLine(Rooms[i]);
                MakeRoom(_map, Rooms[i]);
                if (i != 0 || i != Rooms.Count)
                    MakeDoor(_map, Rooms[i]);

                PlaceSheriffsOffice(_map, Rooms[i]);//N
                PlaceGunShop(_map, Rooms[i]);//S
                PlaceGeneralStore(_map, Rooms[i]);//W
                PlaceSaloon(_map, Rooms[i]);//E
            }

            PlacePlayer(_map, Rooms);
            return _map;
        }

        private void MakeRoom(DungeonMap map, Rectangle Room)
        {
            foreach (Cell cell in map.GetAllCells())
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }

            // Set the first and last rows in the map to not be transparent or walkable
            foreach (Cell cell in map.GetCellsInRows(0, _height - 1))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            // Set the first and last columns in the map to not be transparent or walkable
            foreach (Cell cell in map.GetCellsInColumns(0, _width - 1))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }
        }

        private void PlaceSheriffsOffice(DungeonMap map, Rectangle Room)
        {
            //10x10 box of wall tiles with a door, north of the player
            int centerX = Room.Center.X;
            int centerY = Room.Center.Y;

            int buildingCenter = centerY-25;
            int doorCoord = buildingCenter + 5;

            foreach (Cell cell in map.GetCellsInSquare(centerX, buildingCenter, 5))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }
            MakeTownDoor(map, centerX, doorCoord);
            //Rectangle building = new Rectangle(centerX - 5, doorCoord - 10, 10, 10);
            //MakeBuilding(_map, building);
        }

        private void PlaceSaloon(DungeonMap map, Rectangle Room)
        {
            //10x10 box of wall tiles with a door, east of the player
            int centerX = Room.Center.X;
            int centerY = Room.Center.Y;

            int buildingCenter = centerX + 25;
            int doorCoord = buildingCenter - 5;

            foreach (Cell cell in map.GetCellsInSquare(buildingCenter, centerY, 5))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }
            MakeTownDoor(map, doorCoord, centerY);
            //Rectangle building = new Rectangle(centerX - 5, doorCoord - 10, 10, 10);
            //MakeBuilding(_map, building);
        }

        private void PlaceGeneralStore(DungeonMap map, Rectangle Room)
        {
            //10x10 box of wall tiles with a door, west of the player
            int centerX = Room.Center.X;
            int centerY = Room.Center.Y;

            int buildingCenter = centerX - 25;
            int doorCoord = buildingCenter + 5;

            foreach (Cell cell in map.GetCellsInSquare(buildingCenter, centerY, 5))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }
            MakeTownDoor(map, doorCoord, centerY);
            //Rectangle building = new Rectangle(centerX - 5, doorCoord - 10, 10, 10);
            //MakeBuilding(_map, building);
        }

        private void PlaceGunShop(DungeonMap map, Rectangle Room)
        {
            //10x10 box of wall tiles with a door, south of the player
            int centerX = Room.Center.X;
            int centerY = Room.Center.Y;

            int buildingCenter = centerY + 25;
            int doorCoord = buildingCenter - 5;

            foreach (Cell cell in map.GetCellsInSquare(centerX, buildingCenter, 5))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }
            MakeTownDoor(map, centerX, doorCoord);
            //Rectangle building = new Rectangle(centerX - 5, doorCoord - 10, 10, 10);
            //MakeBuilding(_map, building);
        }

        private void MakeTownDoor(DungeonMap map, int DoorCoordX, int DoorCoordY)
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
}
