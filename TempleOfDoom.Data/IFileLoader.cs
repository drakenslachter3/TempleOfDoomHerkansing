
using TempleOfDoom.Data.Models;

namespace TempleOfDoom.Data
{
    public interface IFileLoader
    {
        GameData LoadGame(string filePath);
        bool CanLoadFile(string filePath);
    }
}
