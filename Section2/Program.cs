﻿using Section2.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Section2.Data;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

            string computersJson = File.ReadAllText("Computers.json");

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            // Using ienumerable type rather than list since we don't need to access any one data, don't need to add it to after we initialize it
            // so it'll loop through much quicker since that object won't be expected to add anything
            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            IEnumerable<Computer>? computersNewtonsoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            if (computersNewtonsoft != null)
            {
                foreach( Computer computer in computersNewtonsoft)
                {
                    string sql = @"INSERT INTO TutorialAppSchema.Computer (
                        Motherboard,
                        HasWifi,
                        HasLte,
                        ReleaseDate,
                        Price,
                        VideoCard
                    ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
                            + "','" + computer.HasWifi
                            + "','" + computer.HasLte
                            + "','" + computer.ReleaseDate
                            + "','" + computer.Price
                            + "','" + EscapeSingleQuote(computer.VideoCard)
                    + "')";

                    dapper.ExecuteSql(sql);
                }   
            }

            string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);
            File.WriteAllText("computersCopySystem.txt", computersCopySystem);

            string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonsoft, settings);
            File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);
        }

        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");
            
            return output;
        }

        public static void ReadingAndWritingToAFile(string sql)
        {
            // File.WriteAllText("log.txt", "\n" + sql + "\n"); // will override file everytime

            using StreamWriter openFile = new("log.txt", append: true);
            openFile.WriteLine("\n" + sql + "\n"); // will append all sql inserts
            openFile.Close();

            string fileText = File.ReadAllText("log.txt");

            Console.WriteLine(fileText);
        }

        public static void Section2()
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