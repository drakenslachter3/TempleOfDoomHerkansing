using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Commands
{
    public class MoveCommand : ICommand
    {
        public readonly Direction Direction;

        public MoveCommand(Direction direction)
        {
            Direction = direction;
        }

        public void Execute(ActionManager actionManager)
        {
            actionManager.MovePlayer(Direction);
        }
    }
}
