using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Doors.Decorators
{
    public class StoneCountDoorDecorator : DoorDecorator
    {
        private readonly int _requiredStones;

        public StoneCountDoorDecorator(IDoor door, int requiredStones) : base(door)
        {
            _requiredStones = requiredStones;
        }
        public override string Description => $"{base.Description} (Requires {_requiredStones} Stones)";

        public override bool CanPass(GameState gameState) //Kijkt of de player genoeg stones heeft
        {
            int stoneCount = gameState.Player.Inventory
                .Count(item => item.Type.Equals("SankaraStone", StringComparison.OrdinalIgnoreCase));

            return base.CanPass(gameState) && stoneCount >= _requiredStones;
        }
    }
}
