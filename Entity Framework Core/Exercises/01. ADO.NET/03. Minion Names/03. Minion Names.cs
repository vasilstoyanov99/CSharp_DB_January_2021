using System;
using Microsoft.Data.SqlClient;

namespace _03._Minion_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection("Server=.;Integrated Security=true;Database=MinionsDB"))
            {
                connection.Open();
                Console.WriteLine("Enter villain Id: ");
                int villainId = int.Parse(Console.ReadLine());

                string queryToFindVillainName = $"SELECT [Name] FROM Villains WHERE Id = {villainId}";
                SqlCommand command = new SqlCommand(queryToFindVillainName, connection);

                if (object.Equals(command.ExecuteScalar(), null))
                {
                    Console.WriteLine($"No villain with ID <{villainId}> exists in the database.");
                    return;
                }

                string queryToCheckMinionsCount = $"SELECT COUNT(*)" +
                                                  $"FROM MinionsVillains AS mv" +
                                                  $"	JOIN Minions As m ON mv.MinionId = m.Id" +
                                                  $"WHERE mv.VillainId = {villainId}";
                command = new SqlCommand(queryToCheckMinionsCount, connection);
                command = new SqlCommand(queryToFindVillainName, connection);
                string villainName = (string)command.ExecuteScalar();

                if (object.Equals(command.ExecuteScalar(), null))
                {
                    Console.WriteLine($"Villain: {villainName}");
                    Console.WriteLine("(no minions)");
                    return;
                }

                string queryToGetAllMinions =
                    @"SELECT ROW_NUMBER()  OVER (ORDER BY m.Name) as RowNum,
                        m.Name, 
                        m.Age
                      FROM MinionsVillains AS mv
                            JOIN Minions As m ON mv.MinionId = m.Id
                      WHERE mv.VillainId = @Id
                      ORDER BY m.Name";
                command = new SqlCommand(queryToGetAllMinions, connection);
                command.Parameters.AddWithValue("@Id", villainId);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"Villain: {villainName}");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]}. {reader[1]} {reader[2]}");
                }
            }
        }
    }
}
