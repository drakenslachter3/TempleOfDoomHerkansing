using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Doors
{
    public class GameDoor : IDoor
    {
        public virtual bool IsOpen { get; set; } = true;
        public virtual string Description => "Basic Door";

        public virtual bool CanPass(GameState gameState)
        {
            return IsOpen;
        }

        public virtual void OnPass(GameState gameState)
        {
        }
    }
}
