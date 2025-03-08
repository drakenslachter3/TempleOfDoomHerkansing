namespace TempleOfDoom.Data.Models
{
    public class Connection //DTO
    {
        public int? North { get; set; }
        public int? South { get; set; }
        public int? East { get; set; }
        public int? West { get; set; }
        public List<Door> Doors { get; set; }
        public int? Within { get; set; }
        public bool? Horizontal { get; set; }
        public string convertToString()
        {
            return "N" + North + "E" + East + "S" + South + "W" + West + "[wtin:]" + Within + "[hrntl:]"+Horizontal;

        }
    }
}