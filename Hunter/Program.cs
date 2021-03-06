﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLNET;
using RogueSharp;

using Hunter.Core;
using Hunter.Systems;
using Hunter.MapGeneration;

namespace Hunter
{
    public class Game
    {
        // The screen height and width are in number of tiles
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 90;
        private static RLRootConsole _rootConsole;

        // The map console takes up most of the screen and is where the map will be drawn
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 80;
        private static RLConsole _mapConsole;

        private static readonly int _menuWidth = 80;
        private static readonly int _menuHeight = 80;
        //private static RLConsole _menuConsole;

        // Below the map console is the message console which displays attack rolls and other information
        private static readonly int _messageWidth = 80;
        private static readonly int _messageHeight = 10;
        private static RLConsole _messageConsole;

        // The stat console is to the right of the map and display player and monster stats
        private static readonly int _statWidth = 20;
        private static readonly int _statHeight = 70;
        private static RLConsole _statConsole;

        private static bool _renderRequired = true;

        public static CommandSystem CommandSystem { get; private set; }
        public static Menu Menu { get; private set; }
        public static QuestMenu QuestMenu { get; private set; }
        public static DeathScreen DeathScreen { get; private set; }
        public static WinMenu WinMenu { get; private set; }

        public static Player Player { get; private set; }
        public static DungeonMap DungeonMap { get; private set; }
        public static MessageLog MessageLog { get; private set; }
        public static SchedulingSystem SchedulingSystem { get; private set; }
        public static Random rng = new Random();
        //private static int _mapLevel = 1;

        public static int _maxrooms = 4;
        public static int _roomMinSize = 10;
        public static int _roomMaxSize = 15;        

        public static void Main()
        {
            // This must be the exact name of the bitmap font file we are using or it will error.
            string fontFileName = "terminal8x8.png";
            // The title will appear at the top of the console window
            string consoleTitle = "Hunter v0.0";


            // Tell RLNet to use the bitmap font that we specified and that each tile is 8 x 8 pixels
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight,
              8, 8, 1f, consoleTitle);

            // Initialize the sub consoles that we will Blit to the root console
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _messageConsole = new RLConsole(_messageWidth, _messageHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);

            Player = new Player();
            CommandSystem = new CommandSystem();
            QuestMenu = new QuestMenu(_menuWidth, _menuHeight);
            DeathScreen = new DeathScreen(_menuWidth, _menuHeight);
            WinMenu = new WinMenu(_menuWidth, _menuHeight);
            Menu = new Menu(_menuWidth, _menuHeight);
            SchedulingSystem = new SchedulingSystem();

            // Create a new MessageLog and print the random seed used to generate the level
            MessageLog = new MessageLog();
            MessageLog.Add("The rogue arrives on level 1");
            MessageLog.Add("Prepare to fight for your life");

            //Generate the map   
            TownMap mapCreation = new TownMap(_mapWidth, _mapHeight);
            //SimpleBsp mapCreation = new SimpleBsp(_mapWidth, _mapHeight);
            //FullRoomBsp mapCreation = new FullRoomBsp(_mapWidth, _mapHeight);

            DungeonMap = mapCreation.CreateMap();
            DungeonMap.UpdatePlayerFieldOfView();


            // Set up a handler for RLNET's Update event
            _rootConsole.Update += OnRootConsoleUpdate;
            // Set up a handler for RLNET's Render event
            _rootConsole.Render += OnRootConsoleRender;
            // Begin RLNET's game loop
            _rootConsole.Run();
        }

        // Event handler for RLNET's Update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            bool didPlayerAct = false;
            RLKeyPress keyPress = _rootConsole.Keyboard.GetKeyPress();

            if (CommandSystem.IsPlayerTurn)
            {
                if (Globals.BuildingEntranceIsTriggered)
                {
                    if (keyPress != null)
                    {
                        if (keyPress.Key == RLKey.E)
                        {                            
                            CommandSystem.CloseMenu();
                            Globals.SheriffTriggered = false;
                            Globals.GenericMenuTriggered = false;
                            didPlayerAct = true;
                        }
                        else if (keyPress.Key == RLKey.Enter)
                        {
                            //SimpleBsp mapGenerator = new SimpleBsp(_mapWidth, _mapHeight);
                            FullRoomBsp mapGenerator = new FullRoomBsp(_mapWidth, _mapHeight);
                            DungeonMap = mapGenerator.CreateMap();
                            MessageLog = new MessageLog();
                            CommandSystem = new CommandSystem();
                            CommandSystem.CloseMenu();
                            Globals.SheriffTriggered = false;
                            didPlayerAct = true;
                        }
                        else if (keyPress.Key == RLKey.Escape)
                        {
                            _rootConsole.Close();
                        }
                    }
                }
                else if (Globals.IsPlayerDead)
                {
                    if (keyPress != null)
                    {
                        if (keyPress.Key == RLKey.Enter)
                        {                            
                            TownMap mapGenerator = new TownMap(_mapWidth, _mapHeight);
                            DungeonMap = mapGenerator.CreateMap();
                            MessageLog = new MessageLog();
                            CommandSystem = new CommandSystem();
                            CommandSystem.CloseMenu();
                            Player.Health = Player.MaxHealth;
                            Globals.IsPlayerDead = false;
                            didPlayerAct = true;
                        }
                        else if (keyPress.Key == RLKey.Escape)
                        {
                            _rootConsole.Close();
                        }
                    }
                }
                else if (Globals.IsBossDead)
                {
                    if (keyPress != null)
                    {
                        if (keyPress.Key == RLKey.Enter)
                        {
                            TownMap mapGenerator = new TownMap(_mapWidth, _mapHeight);
                            DungeonMap = mapGenerator.CreateMap();
                            MessageLog = new MessageLog();
                            CommandSystem = new CommandSystem();
                            CommandSystem.CloseMenu();
                            Player.Health = Player.MaxHealth;
                            Globals.IsBossDead = false;
                            didPlayerAct = true;
                        }
                        else if (keyPress.Key == RLKey.E)
                        {
                            CommandSystem.CloseMenu();
                            Globals.IsBossDead = false;
                            didPlayerAct = true;
                        }
                        else if (keyPress.Key == RLKey.Escape)
                        {
                            _rootConsole.Close();
                        }
                    }
                }
                else
                {
                    if (keyPress != null)
                    {
                        if (keyPress.Key == RLKey.Up)
                        {
                            didPlayerAct = CommandSystem.MovePlayer(Direction.Up);
                        }
                        else if (keyPress.Key == RLKey.Down)
                        {
                            didPlayerAct = CommandSystem.MovePlayer(Direction.Down);
                        }
                        else if (keyPress.Key == RLKey.Left)
                        {
                            didPlayerAct = CommandSystem.MovePlayer(Direction.Left);
                        }
                        else if (keyPress.Key == RLKey.Right)
                        {
                            didPlayerAct = CommandSystem.MovePlayer(Direction.Right);
                        }
                        else if (keyPress.Key == RLKey.Comma)
                        {
                            //if (DungeonMap.CanMoveDownToNextLevel())
                            //{
                            TownMap mapGenerator = new TownMap(_mapWidth, _mapHeight);
                            DungeonMap = mapGenerator.CreateMap();
                            MessageLog = new MessageLog();
                            CommandSystem = new CommandSystem();
                            Player.Health = Player.MaxHealth;
                            didPlayerAct = true;
                            //}
                        }
                        else if (keyPress.Key == RLKey.Escape)
                        {
                            _rootConsole.Close();
                        }
                    }
                }
                if (didPlayerAct)
                {
                    _renderRequired = true;
                    CommandSystem.EndPlayerTurn();
                }
            }
            else
            {
                CommandSystem.ActivateMonsters();
                _renderRequired = true;
            }
        }

        // Event handler for RLNET's Render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            if (_renderRequired)
            {
                _mapConsole.Clear();
                _statConsole.Clear();
                _messageConsole.Clear();

                DungeonMap.Draw(_mapConsole, _statConsole);
                Player.Draw(_mapConsole);
                Player.DrawStats(_statConsole);
                MessageLog.Draw(_messageConsole);
                    

                if (!Globals.BuildingEntranceIsTriggered && !Globals.IsPlayerDead && !Globals.IsBossDead)
                {
                    // Blit the sub consoles to the root console in the correct locations
                    RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight,
                      _rootConsole, 0, 0);
                    RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight,
                      _rootConsole, _mapWidth, 0);
                    RLConsole.Blit(_messageConsole, 0, 0, _messageWidth, _messageHeight,
                      _rootConsole, 0, _screenHeight - _messageHeight);
                }
                else if(Globals.BuildingEntranceIsTriggered)
                {
                    if (Globals.SheriffTriggered)
                        QuestMenu.CreateQuestMenu(_rootConsole);
                    else if (Globals.GenericMenuTriggered)
                        Menu.CreateMenu(_rootConsole);
                    else                    
                        Globals.BuildingEntranceIsTriggered = false;
                }
                else if (Globals.IsBossDead)
                {                    
                    WinMenu.CreateWinScreen(_rootConsole);
                }
                else if (Globals.IsPlayerDead)
                {
                    if (Player.Health <= 0)
                        DeathScreen.CreateDeathScreen(_rootConsole);
                    else
                        Globals.IsPlayerDead = false;
                }
                // Tell RLNET to draw the console that we set
                _rootConsole.Draw();
                _renderRequired = false;
            }           
        }
    }
}
