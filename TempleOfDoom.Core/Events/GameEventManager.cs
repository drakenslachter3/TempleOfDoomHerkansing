namespace TempleOfDoom.Core.Events
{
    public class GameEventManager : IGameEventSubject
    {
        private readonly List<IGameEventObserver> _observers = new();

        public void RegisterObserver(IGameEventObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void RemoveObserver(IGameEventObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(GameEvent gameEvent)
        {
            foreach (var observer in _observers)
            {
                observer.OnGameEvent(gameEvent);
            }
        }
    }
}
