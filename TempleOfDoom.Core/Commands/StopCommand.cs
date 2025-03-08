using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Commands
{
    public class StopCommand : ICommand
    {

        public void Execute(ActionManager actionManager)
        {
            actionManager.Stop();
        }
    }
}
