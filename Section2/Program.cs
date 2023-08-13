using Section2.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Section2.Data;

namespace Section2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataContextDapper dapper = new DataContextDapper();

            string sqlCommand = "SELECT GETDATE()";
            DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);
            Console.WriteLine(rightNow);

            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLte = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060",
            };

            // bool result = InsertComputer(myComputer, dapper);

            PrintAllComputers(dapper);
        }

        public static bool InsertComputer(Computer computer, DataContextDapper dapper)
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

        public static void PrintAllComputers(DataContextDapper dapper)
        {
            string sqlSelect = @"
                SELECT
                    Computer.Motherboard,
                    Computer.HasWifi,
                    Computer.HasLte,
                    Computer.ReleaseDate,
                    Computer.Price,
                    Computer.VideoCard
                FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
            // List<Computer> computers = dapper.LoadData<Computer>(sqlSelect).ToList();

            foreach(Computer computer in computers)
            {
                Console.WriteLine("'" + computer.Motherboard
                    + "','" + computer.HasWifi
                    + "','" + computer.HasLte
                    + "','" + computer.ReleaseDate
                    + "','" + computer.Price
                    + "','" + computer.VideoCard + "'");

                Console.WriteLine("'Motherboard','HasWifi','HasLTE','ReleaseDate','Price','Videocard'");
                Console.WriteLine();
            }
        }
    }
}