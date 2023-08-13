namespace Section2
{
    public class Computer
    {
        // private string _motherboard; // automatically set when we add get set
        public string Motherboard { get; set; } = "";
        public int CPUCores { get; set; }
        public bool HasWifi { get; set; }
        public bool HasLte { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; } = "";
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLte = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060",
            };
            myComputer.HasWifi = false;
            Console.WriteLine(myComputer.Motherboard);
            Console.WriteLine(myComputer.HasWifi);
            Console.WriteLine(myComputer.HasLte);
            Console.WriteLine(myComputer.ReleaseDate);
            Console.WriteLine(myComputer.Price);
            Console.WriteLine(myComputer.VideoCard);
        }
    }
}