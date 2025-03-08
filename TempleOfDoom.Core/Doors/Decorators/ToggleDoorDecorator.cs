using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Doors.Decorators
{
    public class ToggleDoorDecorator : DoorDecorator
    {
        public ToggleDoorDecorator(IDoor door) : base(door)
        {
        }
        public override string Description => $"{base.Description} (Toggle)";

        public override void OnPass(GameState gameState) 
        {
            IsOpen = !IsOpen;
            base.OnPass(gameState);
        }
    }
}
