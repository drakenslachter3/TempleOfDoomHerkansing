using TempleOfDoom.Core.Events;
using TempleOfDoom.Core.Game.Models;

namespace TempleOfDoom.Core.Events
{
    public class ItemPickupEventData
    {
        public GameItem Item { get; }
        public GamePlayer Player { get; }
        public GameRoom Room { get; }
        public GameEventManager EventManager { get; }

        public ItemPickupEventData(GameItem item, GamePlayer player, GameRoom room, GameEventManager eventManager)
        {
            Item = item;
            Player = player;
            Room = room;
            EventManager = eventManager;
        }
    }
}
