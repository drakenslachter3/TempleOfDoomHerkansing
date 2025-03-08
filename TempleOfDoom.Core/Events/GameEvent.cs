namespace TempleOfDoom.Core.Events
{
    public class GameEvent
    {
        public GameEventType Type { get; }
        public object Data { get; }

        public GameEvent(GameEventType type, object data = null)
        {
            Type = type;
            Data = data;
        }
    }
}