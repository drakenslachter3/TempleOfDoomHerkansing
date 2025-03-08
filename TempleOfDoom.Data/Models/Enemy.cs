namespace TempleOfDoom.Data.Models
{
    public class Enemy //DTO
    {
        public string? Type { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public int? MinX { get; set; }
        public int? MinY { get; set; }
        public int? MaxX { get; set; }
        public int? MaxY { get; set; }
    }
}