using TempleOfDoom.Core.Events;
using TempleOfDoom.Core.Game.Models;
namespace TempleOfDoom.Core.Events.Observers
{
    public class SwitchObserver : IGameEventObserver
    {
        public void OnGameEvent(GameEvent gameEvent)
        {
            if (gameEvent.Type != GameEventType.PlayerMoved)
                return;

            var eventData = (PlayerMovedEventData)gameEvent.Data;
            var room = eventData.Room;
            var player = eventData.Player;

            if (CheckForSwitchedCase(room, player))
            {
                room.SwitchTriggered = true;
            }
        }

        private static bool CheckForSwitchedCase(GameRoom room, GamePlayer player)
        {
            var pressurePlates = room.Items.Where(item => item.Type.ToLower().Contains("pressure plate"));

            foreach (var plate in pressurePlates)
            {
                bool isOccupied = IsPositionOccupied(room, player, plate);
                if (!isOccupied) return false;
            }
            return true;
        }

        private static bool IsPositionOccupied(GameRoom room, GamePlayer player, GameItem position)
        {
            if (player.X == position.X && player.Y == position.Y) return true;

            return room.Enemies.Any(enemy => enemy.X == position.X && enemy.Y == position.Y);
        }
    }
}