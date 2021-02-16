using System;
using Microsoft.Data.SqlClient;

namespace _01._Initial_Setup
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection("Server=.;Integrated Security=true"))
            {
                connection.Open();

                string query = "CREATE DATABASE MinionsDB";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }

            string createTableCountries = "CREATE TABLE Countries " +
                                          "(Id INT PRIMARY KEY IDENTITY, [Name] NVARCHAR(50) NOT NULL)";
            ExecuteNonQuery(createTableCountries);

            string createTableTowns = "CREATE TABLE Towns " +
                                      "( Id INT PRIMARY KEY IDENTITY, [Name] NVARCHAR(50) NOT NULL, " +
                                      "CountryCode INT REFERENCES Countries(Id) )";
            ExecuteNonQuery(createTableTowns);

            string createTableMinions = "CREATE TABLE Minions " +
                                        "( Id INT PRIMARY KEY IDENTITY, [Name] NVARCHAR(50) NOT NULL, " +
                                        "Age INT NOT NULL, TownId INT REFERENCES Towns(Id) )";
            ExecuteNonQuery(createTableMinions);

            string createTableEvilnessFactors = "CREATE TABLE EvilnessFactors " +
                                                "( Id INT PRIMARY KEY IDENTITY, " +
                                                "[Name] NVARCHAR(50) NOT NULL )";
            ExecuteNonQuery(createTableEvilnessFactors);

            string createTableVillains = "CREATE TABLE Villains " +
                                         "( Id INT PRIMARY KEY IDENTITY, [Name] NVARCHAR(50) NOT NULL, " +
                                         "EvilnessFactorId INT REFERENCES EvilnessFactors(Id))";
            ExecuteNonQuery(createTableVillains);

            string createTableMinionsVillains = "CREATE TABLE MinionsVillains " +
                                                "( MinionId INT REFERENCES Minions(Id)," +
                                                "VillainId INT REFERENCES Villains(Id)," +
                                                "PRIMARY KEY (MinionId, VillainId))";
            ExecuteNonQuery(createTableMinionsVillains);
        }

        static void ExecuteNonQuery(string query)
        {
            using (var connection = new SqlConnection("Server=.;Integrated Security=true;Database=MinionsDB"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }

        }
    }
}
