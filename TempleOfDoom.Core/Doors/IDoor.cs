using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Doors
{
    public interface IDoor
    {
        bool IsOpen { get; set; }
        string Description { get; }
        bool CanPass(GameState gameState);
        void OnPass(GameState gameState);
    }
}
