using System.Text.Json;
using TempleOfDoom.Data.Models;

namespace TempleOfDoom.Data
{
    public class JsonFileLoader : IFileLoader
    {
        private readonly JsonSerializerOptions _options;

        public JsonFileLoader()
        {
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public bool CanLoadFile(string filePath)
        {
            return Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase);
        }

        public GameData LoadGame(string filePath) //Maakt gebruik van de ingebakken JSON parser on DTOtjes te maken
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Game data file not found at: {filePath}. Current directory is: {Directory.GetCurrentDirectory()}");
            }

            try
            {
                string jsonString = File.ReadAllText(filePath);
                var gameData = JsonSerializer.Deserialize<GameData>(jsonString, _options);

                if (gameData == null)
                {
                    throw new InvalidDataException("Failed to deserialize game data");
                }

                return gameData;
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException($"Error parsing JSON file: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading game data: {ex.Message}");
            }
        }
    }
}
