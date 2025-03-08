using TempleOfDoom.Core.Game.Models;

namespace TempleOfDoom.Core.Events.Observers
{
    public class InventoryObserver : IGameEventObserver
    {
        public void OnGameEvent(GameEvent gameEvent)
        {
            if (gameEvent.Type != GameEventType.ItemPickedUp)
                return;

            var eventData = (ItemPickupEventData)gameEvent.Data;
            var item = eventData.Item;
            var player = eventData.Player;
            var room = eventData.Room;
            var eventManager = eventData.EventManager;

            if (!item.Type.ToLower().Contains("pressure plate") &&
                !item.Type.ToLower().Contains("boobytrap"))
            {
                player.Inventory.Add(item);
                room.Items.Remove(item);

                int sankaraStones = player.Inventory.Count(i => i.Type == "sankara stone");
                if (sankaraStones == 5)
                {
                    eventManager.NotifyObservers(new GameEvent(GameEventType.GameWon));
                }
            }
        }
    }

}

