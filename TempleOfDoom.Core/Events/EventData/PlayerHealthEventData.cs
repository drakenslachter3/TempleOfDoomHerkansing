using TempleOfDoom.Core.Events;

namespace TempleOfDoom.Core.Events
{
    public class PlayerHealthEventData
    {
        public int CurrentHealth { get; }
        public GameEventManager EventManager { get; }

        public PlayerHealthEventData(int currentHealth, GameEventManager eventManager)
        {
            CurrentHealth = currentHealth;
            EventManager = eventManager;
        }
    }
}
