using Hunter.Core;
using Hunter.MapGeneration;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Systems
{
    public class InputHandler
    {
        private int _mapWidth;
        private int _mapHeight;
        private RLRootConsole _rootConsole;

        public InputHandler(int mapWidth, int mapHeight, RLRootConsole rootConsole)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            _rootConsole = rootConsole;
        }

        public void HandleGameInputs(DungeonMap DungeonMap, MessageLog MessageLog, CommandSystem CommandSystem, RLKeyPress keyPress)
        {
            //    if (Globals.BuildingEntranceIsTriggered)
            //    {
            //        if (keyPress != null)
            //        {
            //            if (keyPress.Key == RLKey.E)
            //            {
            //                CommandSystem.CloseMenu();
            //                Globals.SheriffTriggered = false;
            //                Globals.GenericMenuTriggered = false;
            //                Game.didPlayerAct = true;
            //            }
            //            else if (keyPress.Key == RLKey.Enter)
            //            {
            //                //SimpleBsp mapGenerator = new SimpleBsp(_mapWidth, _mapHeight);
            //                FullRoomBsp mapGenerator = new FullRoomBsp(_mapWidth, _mapHeight);
            //                DungeonMap = mapGenerator.CreateMap();
            //                MessageLog = new MessageLog();
            //                CommandSystem = new CommandSystem();
            //                CommandSystem.CloseMenu();
            //                Globals.SheriffTriggered = false;
            //                Game.didPlayerAct = true;
            //            }
            //            else if (keyPress.Key == RLKey.Escape)
            //            {
            //                _rootConsole.Close();
            //            }
            //        }
            //    }
            //    else if (Globals.IsPlayerDead)
            //    {
            //        if (keyPress != null)
            //        {
            //            if (keyPress.Key == RLKey.Enter)
            //            {
            //                TownMap mapGenerator = new TownMap(_mapWidth, _mapHeight);
            //                DungeonMap = mapGenerator.CreateMap();
            //                MessageLog = new MessageLog();
            //                CommandSystem = new CommandSystem();
            //                CommandSystem.CloseMenu();
            //                Game.Player.Health = Game.Player.MaxHealth;
            //                Globals.IsPlayerDead = false;
            //                Game.didPlayerAct = true;
            //            }
            //            else if (keyPress.Key == RLKey.Escape)
            //            {
            //                _rootConsole.Close();
            //            }
            //        }
            //    }
            //    else if (Globals.IsBossDead)
            //    {
            //        if (keyPress != null)
            //        {
            //            if (keyPress.Key == RLKey.Enter)
            //            {
            //                TownMap mapGenerator = new TownMap(_mapWidth, _mapHeight);
            //                DungeonMap = mapGenerator.CreateMap();
            //                MessageLog = new MessageLog();
            //                CommandSystem = new CommandSystem();
            //                CommandSystem.CloseMenu();
            //                Game.Player.Health = Game.Player.MaxHealth;
            //                Globals.IsBossDead = false;
            //                Game.didPlayerAct = true;
            //            }
            //            else if (keyPress.Key == RLKey.E)
            //            {
            //                CommandSystem.CloseMenu();
            //                Globals.IsBossDead = false;
            //                Game.didPlayerAct = true;
            //            }
            //            else if (keyPress.Key == RLKey.Escape)
            //            {
            //                _rootConsole.Close();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (keyPress != null)
            //        {
            //            if (keyPress.Key == RLKey.Up)
            //            {
            //                Game.didPlayerAct = CommandSystem.MovePlayer(Direction.Up);
            //            }
            //            else if (keyPress.Key == RLKey.Down)
            //            {
            //                Game.didPlayerAct = CommandSystem.MovePlayer(Direction.Down);
            //            }
            //            else if (keyPress.Key == RLKey.Left)
            //            {
            //                Game.didPlayerAct = CommandSystem.MovePlayer(Direction.Left);
            //            }
            //            else if (keyPress.Key == RLKey.Right)
            //            {
            //                Game.didPlayerAct = CommandSystem.MovePlayer(Direction.Right);
            //            }
            //            else if (keyPress.Key == RLKey.Comma)
            //            {
            //                //if (DungeonMap.CanMoveDownToNextLevel())
            //                //{
            //                TownMap mapGenerator = new TownMap(_mapWidth, _mapHeight);
            //                DungeonMap = mapGenerator.CreateMap();
            //                MessageLog = new MessageLog();
            //                CommandSystem = new CommandSystem();
            //                Game.Player.Health = Game.Player.MaxHealth;
            //                Game.didPlayerAct = true;
            //                //}
            //            }
            //            else if (keyPress.Key == RLKey.Escape)
            //            {
            //                _rootConsole.Close();
            //            }
            //        }
            //    }
        }
    }
}
