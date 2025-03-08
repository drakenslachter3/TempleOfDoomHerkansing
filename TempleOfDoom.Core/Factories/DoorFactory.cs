using System.Diagnostics;
using TempleOfDoom.Core.Doors;
using TempleOfDoom.Core.Doors.Decorators;
using DoorDto = TempleOfDoom.Data.Models.Door;

namespace TempleOfDoom.Core.Factories
{
    public class DoorFactory : IGameObjectFactory<DoorDto, IDoor>
    {
        public string Type => "Door";

        public IDoor Create(DoorDto door)
        {
            IDoor baseDoor = new GameDoor();

            if (door.Type?.ToLower().Contains("switched") ?? false)
            {
                baseDoor = new SwitchedDoorDecorator(baseDoor);
            }

            if (door.Type?.ToLower().Contains("toggle") ?? false)
            {
                baseDoor = new ToggleDoorDecorator(baseDoor);
            }

            if (door.Type?.ToLower().Contains("closing gate") ?? false)
            {
                baseDoor = new ClosingGateDecorator(baseDoor);
            }

            if (door.Type?.ToLower().Contains("open on odd") ?? false)
            {
                baseDoor = new OddLifeDoorDecorator(baseDoor);
            }

            if (door.Type?.ToLower().Contains("colored") ?? false)
            {
                baseDoor = new ColoredDoorDecorator(baseDoor, door.Color);
            }

            if (door.Type?.ToLower().Contains("open on stones in room") ?? false)
            {
                baseDoor = new StoneCountDoorDecorator(baseDoor, door.NoOfStones ?? 0);
            }

            return baseDoor;
        }
    }
}
