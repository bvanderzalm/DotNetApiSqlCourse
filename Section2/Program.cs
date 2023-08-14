using Section2.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Section2.Data;
using Microsoft.Extensions.Configuration;

namespace Section2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DataContextDapper dapper = new DataContextDapper(config);
            DataContextEF entityFramework = new DataContextEF(config);

            string sqlCommand = "SELECT GETDATE()";
            DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);
            Console.WriteLine(rightNow);

            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLte = false,
                ReleaseDate = DateTime.Now,
                Price = 843.87m,
                VideoCard = "RTX 2060",
            };

            // EntityFrameworkAddComputer(myComputer, entityFramework);
            // bool result = SqlInsertComputer(myComputer, dapper);

            Console.WriteLine("entity framework");
            IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();
            if (computersEf != null)
                PrintComputerList(computersEf.ToList());

            Console.WriteLine("dapper");
            PrintAllComputersDapper(dapper);
        }

        public static void EntityFrameworkAddComputer(Computer computer, DataContextEF entityFramework)
        {
            entityFramework.Add(computer);
            entityFramework.SaveChanges();
        }

        public static bool SqlInsertComputer(Computer computer, DataContextDapper dapper)
        {
            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLte,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" + computer.Motherboard
                    + "','" + computer.HasWifi
                    + "','" + computer.HasLte
                    + "','" + computer.ReleaseDate
                    + "','" + computer.Price
                    + "','" + computer.VideoCard
            + "')";

            return dapper.ExecuteSql(sql);
        }

        public static void PrintAllComputersDapper(DataContextDapper dapper)
        {
            string sqlSelect = @"
                SELECT
                    Computer.ComputerId,
                    Computer.Motherboard,
                    Computer.HasWifi,
                    Computer.HasLte,
                    Computer.ReleaseDate,
                    Computer.Price,
                    Computer.VideoCard
                FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
            // List<Computer> computers = dapper.LoadData<Computer>(sqlSelect).ToList();
            PrintComputerList(computers.ToList());
        }

        public static void PrintComputerList(List<Computer> computers)
        {
            foreach(Computer computer in computers)
            {
                Console.WriteLine("'" + computer.ComputerId
                    + "','" + computer.Motherboard
                    + "','" + computer.HasWifi
                    + "','" + computer.HasLte
                    + "','" + computer.ReleaseDate
                    + "','" + computer.Price
                    + "','" + computer.VideoCard + "'");

                Console.WriteLine("'ComputerId','Motherboard','HasWifi','HasLTE','ReleaseDate','Price','Videocard'");
                Console.WriteLine();
            }
        }
    }
}