namespace TempleOfDoom.Data.Models
{
    public class Room //DTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Item> Items { get; set; }
        public List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public List<SpecialFloorTile> SpecialFloorTiles { get; set; } = new List<SpecialFloorTile>();
    }
}