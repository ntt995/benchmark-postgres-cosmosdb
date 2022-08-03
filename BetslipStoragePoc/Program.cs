
using Npgsql;

namespace Cb.BetslipStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            string option = "";
            while (option != "4")
            {
                var cs = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";
                string sql = "";
                Console.WriteLine("Select option to run: ");
                Console.WriteLine("1 - Check Local Postgres Version");
                Console.WriteLine("2 - Write the data to car table");
                Console.WriteLine("3 - Read the data from car table");
                Console.WriteLine("4 - Stop the program");
                Console.WriteLine("-----------------------");
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        {
                            using var con = new NpgsqlConnection(cs);
                            con.Open();

                            sql = "SELECT version()";

                            using var verCmd = new NpgsqlCommand(sql, con);

                            var version = verCmd.ExecuteScalar().ToString();
                            Console.WriteLine($"PostgreSQL version: {version}");
                        }
                        break;
                        Console.WriteLine("");
                    case "2":
                        {

                            using var con = new NpgsqlConnection(cs);
                            con.Open();

                            using var cmd = new NpgsqlCommand();
                            cmd.Connection = con;

                            cmd.CommandText = "DROP TABLE IF EXISTS cars";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = @"CREATE TABLE cars(id SERIAL PRIMARY KEY, 
                        name VARCHAR(255), price INT)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi2',52642)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes2',57127)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda2',9000)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volvo2',29000)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Bentley2',350000)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Citroen2',21000)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Hummer2',41400)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volkswagen2',21600)";
                            cmd.ExecuteNonQuery();

                            Console.WriteLine("Table cars created");
                        }
                        Console.WriteLine("");
                        break;
                    case "3":
                        {
                            using var con = new NpgsqlConnection(cs);
                            con.Open();
                            sql = "SELECT * FROM cars";
                            using var readCmd = new NpgsqlCommand(sql, con);

                            using NpgsqlDataReader rdr = readCmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                Console.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1),
                                        rdr.GetInt32(2));
                            }
                            Console.WriteLine("");
                        }

                        break;
                    default: break;
                }
            }
        }
    }
}