using TempleOfDoom.Core.Game.Models;
namespace TempleOfDoom.Core.Events
{
    public class PlayerMovedEventData
    {
        public GameRoom Room { get; }
        public GamePlayer Player { get; }

        public PlayerMovedEventData(GameRoom room, GamePlayer player)
        {
            Room = room;
            Player = player;
        }
    }
}