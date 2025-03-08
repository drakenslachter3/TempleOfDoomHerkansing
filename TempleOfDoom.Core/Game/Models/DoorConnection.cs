using TempleOfDoom.Core.Doors;
using TempleOfDoom.Data.Models;

namespace TempleOfDoom.Core.Game.Models
{
    public class DoorConnection
    {
        public Connection Connection { get; }
        public List<IDoor> Doors { get; }

        public DoorConnection(Connection connection, List<IDoor> doors)
        {
            Connection = connection;
            Doors = doors;
        }
    }
}
