using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Doors.Decorators
{
    public class OddLifeDoorDecorator : DoorDecorator
    {
        public OddLifeDoorDecorator(IDoor door) : base(door)
        {
        }
        public override string Description => $"{base.Description} (Odd Life)";

        public override bool CanPass(GameState gameState) //Kijkt of player oneven levens heeft
        {
            return base.CanPass(gameState) && gameState.Player.Lives % 2 != 0;
        }
    }
}
