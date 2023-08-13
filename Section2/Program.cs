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

            string sqlCommand = "SELECT GETDATE()";
            DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand);
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
            myComputer.HasWifi = false;
            // Console.WriteLine(myComputer.Motherboard);
            // Console.WriteLine(myComputer.HasWifi);
            // Console.WriteLine(myComputer.HasLte);
            // Console.WriteLine(myComputer.ReleaseDate);
            // Console.WriteLine(myComputer.Price);
            // Console.WriteLine(myComputer.VideoCard);
        }
    }
}