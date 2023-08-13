using Section2.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Section2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=DotNetCourseDatabase;Trusted_Connection=false;TrustServerCertificate=True;User Id=sa;Password=SQLConnect1;";

            IDbConnection dbConnection = new SqlConnection(connectionString);

            // string sqlCommand = "SELECT GETDATE()";
            // DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand);
            // Console.WriteLine(rightNow);

            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLte = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060",
            };

            // int result = InsertComputer(myComputer, dbConnection);

            PrintAllComputers(dbConnection);
        }

        public static int InsertComputer(Computer computer, IDbConnection dbConnection)
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

            return dbConnection.Execute(sql);
        }

        public static void PrintAllComputers(IDbConnection dbConnection)
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

            IEnumerable<Computer> computers = dbConnection.Query<Computer>(sqlSelect);
            // List<Computer> computers = dbConnection.Query<Computer>(sqlSelect).ToList();

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