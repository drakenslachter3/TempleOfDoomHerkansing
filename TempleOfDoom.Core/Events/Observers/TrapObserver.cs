using TempleOfDoom.Core.Events;
namespace TempleOfDoom.Core.Events.Observers
{
    public class TrapObserver : IGameEventObserver
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
            if (item.Type.ToLower().Contains("boobytrap"))
            {
                player.Lives--;

                if (item.Type.ToLower().Contains("disappearing"))
                {
                    room.Items.Remove(item);
                }

                eventData.EventManager.NotifyObservers(
                    new GameEvent(GameEventType.PlayerHealthChanged, new PlayerHealthEventData(player.Lives, eventManager)));
            }
        }

    }
}