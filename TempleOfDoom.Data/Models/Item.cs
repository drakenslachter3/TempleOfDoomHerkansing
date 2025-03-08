namespace TempleOfDoom.Data.Models
{
    public class Item //DTO
    {
        public string Type { get; set; }
        public int? Damage { get; set; }
        public string Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}