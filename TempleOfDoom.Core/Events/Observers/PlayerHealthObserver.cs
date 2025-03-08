using System.Diagnostics.Tracing;

namespace TempleOfDoom.Core.Events.Observers
{
    public class PlayerHealthObserver : IGameEventObserver
    {
        public void OnGameEvent(GameEvent gameEvent)
        {
            if (gameEvent.Type != GameEventType.PlayerHealthChanged)
                return;

            var eventData = (PlayerHealthEventData)gameEvent.Data;
            int currentHealth = eventData.CurrentHealth;

            if (currentHealth <= 0)
            {
                eventData.EventManager.NotifyObservers(new GameEvent(GameEventType.GameLost));
            }
        }
    }

}
