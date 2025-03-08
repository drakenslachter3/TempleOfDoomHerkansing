using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Doors.Decorators
{
    public class SwitchedDoorDecorator : DoorDecorator
    {
        public SwitchedDoorDecorator(IDoor door) : base(door)
        {
        }
        public override string Description => $"{base.Description} (Switched)";

        public override bool CanPass(GameState gameState) //Kijkt naar de currentroom's switchtriggered status. 
        {
            return base.CanPass(gameState) && gameState.CurrentRoom.SwitchTriggered;
        }
    }
}
