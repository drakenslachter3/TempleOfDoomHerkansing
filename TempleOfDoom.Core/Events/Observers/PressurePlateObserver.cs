using System.Net.WebSockets;
using TempleOfDoom.Core.Events;
using TempleOfDoom.Core.Game.Models;
using TempleOfDoom.Data.Models;

namespace TempleOfDoom.Core.Events.Observers
{
    public class PressurePlateObserver : IGameEventObserver
    {
        public void OnGameEvent(GameEvent gameEvent)
        {
            if (gameEvent.Type != GameEventType.ItemPickedUp)
                return;

            var eventData = (ItemPickupEventData)gameEvent.Data;
            var item = eventData.Item;
            var room = eventData.Room;
            if (item.Type.ToLower().Contains("pressure plate"))
            {
                foreach (var connection in room.DoorConnections)
                {
                    foreach (var door in connection.Doors)
                    {
                        if (door.Description?.ToLower().Contains("toggle") ?? false)
                        {
                            door.IsOpen = !door.IsOpen;
                        }
                    }
                }
            }
        }
    }
}