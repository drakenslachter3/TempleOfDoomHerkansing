using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Commands
{
    public interface ICommand
    {
        void Execute(ActionManager actionManager);
    }
}
