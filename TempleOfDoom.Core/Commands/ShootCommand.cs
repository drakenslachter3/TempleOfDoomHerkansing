using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Commands
{
    public class ShootCommand : ICommand
    {
        public ShootCommand()
        {
        }

        public void Execute(ActionManager actionManager)
        {
            actionManager.Shoot();
        }
    }
}
