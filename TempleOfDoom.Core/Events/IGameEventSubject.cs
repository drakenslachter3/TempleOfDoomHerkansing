namespace TempleOfDoom.Core.Events
{
    public interface IGameEventSubject
    {
        void RegisterObserver(IGameEventObserver observer);
        void RemoveObserver(IGameEventObserver observer);
        void NotifyObservers(GameEvent gameEvent);
    }
}
