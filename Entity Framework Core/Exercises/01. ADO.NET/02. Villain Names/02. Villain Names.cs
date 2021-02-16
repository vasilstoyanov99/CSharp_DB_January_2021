using System;
using Microsoft.Data.SqlClient;

namespace _02._Villain_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection("Server=.;Integrated Security=true;Database=MinionsDB"))
            {
                connection.Open();
                string query = "SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  " +
                               "FROM Villains AS v " +
                               "    JOIN MinionsVillains AS mv ON v.Id = mv.VillainId " +
                               "GROUP BY v.Id, v.Name " +
                               "  HAVING COUNT(mv.VillainId) > 3 " +
                               "ORDER BY COUNT(mv.VillainId)";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]}");
                }
            }
        }
    }
}
