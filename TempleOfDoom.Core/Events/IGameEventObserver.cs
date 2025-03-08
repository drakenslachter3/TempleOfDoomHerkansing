namespace TempleOfDoom.Core.Events
{
    public interface IGameEventObserver
    {
        void OnGameEvent(GameEvent gameEvent);
    }
}
