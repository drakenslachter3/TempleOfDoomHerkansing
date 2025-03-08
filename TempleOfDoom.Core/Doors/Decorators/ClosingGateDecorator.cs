using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Doors.Decorators
{
    public class ClosingGateDecorator : DoorDecorator
    {
        private bool _hasBeenUsed = false;

        public ClosingGateDecorator(IDoor door) : base(door)
        {
        }
        public override string Description => $"{base.Description} (Closing Gate)";

        public override bool CanPass(GameState gameState)
        {
            return base.CanPass(gameState) && !_hasBeenUsed;
        }

        public override void OnPass(GameState gameState)
        {
            base.OnPass(gameState);
            _hasBeenUsed = true;
            IsOpen = false;
        }
    }
}
