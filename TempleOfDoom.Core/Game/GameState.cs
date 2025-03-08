using System.Data;
using TempleOfDoom.Core.Events;
using TempleOfDoom.Core.Game.Models;
using ICommand = TempleOfDoom.Core.Commands.ICommand;

namespace TempleOfDoom.Core.Game
{
    public class GameState
    {
        public GamePlayer Player { get; private set; }
        public GameRoom CurrentRoom { get; set; }
        public List<GameRoom> GameRooms {get; private set;}
        public bool IsGameRunning { get; set; } = true;
        public bool HasWon { get; set; } = false;
        public ICommand? LatestCommand { get; set; } = null;

        public GameState(GamePlayer player, List<GameRoom> gameRooms) //De tussenkoppeling tussen alles. Dit houdt alle informatie over het spel vast
        {
            Player = player;
            GameRooms = gameRooms;
            CurrentRoom = gameRooms.First(i => i.Id == player.StartRoomId);
        }

    }
}