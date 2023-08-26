namespace Section2.Models
{
    public class Computer
    {
        public int ComputerId { get; set; }
        // private string _motherboard; // automatically set when we add get set
        public string Motherboard { get; set; } = "";
        public int? CPUCores { get; set; } = 0;
        public bool HasWifi { get; set; }
        public bool HasLte { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; } = "";
    }
}