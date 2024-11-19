using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using static System.Console;

namespace Coding_Tracker.nikosnick13;

internal class CodingSessionController
{
    static public string? connectionString = ConfigurationManager.AppSettings["ConnectionString"];

    public static void UpdateCodingRecord(CodingSession session)
    {
        using (var conn = new SqliteConnection(connectionString))
        {
            conn.Open();
            string query = @"UPDATE Coding SET StartTime = @startTime, EndTime = @endTime WHERE Id = @id";

            var editCmd = conn.Execute(query, new
            {
                id = session.Id,
                startTime = session.StartTime,
                endTime = session.EndTime
            });
        }
    }

    public static CodingSession? GetById(int id)
    {
        using (var conn = new SqliteConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT StartTime, EndTime FROM Coding WHERE Id = @id";

            var result = conn.QueryFirstOrDefault(query, new { id });

            if (result != null)
            {
                return new CodingSession
                {
                    Id = id,
                    StartTime = TimeSpan.Parse(result.StartTime),
                    EndTime = TimeSpan.Parse(result.EndTime)
                };
            }
            return null;
        }
    }

    public static void InsertCodingRecord(CodingSession codingSession)
    {
        using (var conn = new SqliteConnection(connectionString))
        {
            conn.Open();

            string query = @"INSERT INTO Coding (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)";

            var parameters = new
            {
                StartTime = codingSession.StartTime.ToString(@"hh\:mm"),
                EndTime = codingSession.EndTime.ToString(@"hh\:mm"),
                Duration = (codingSession.EndTime - codingSession.StartTime).ToString(@"hh\:mm")
            };

            conn.Execute(query, parameters);
        }
    }

    public static void GetAllCodingRecords()
    {
        List<CodingSession> recordsList = new List<CodingSession>();

        try
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT * FROM Coding";
                 
                using (var GetAllCmd = new SqliteCommand(query, conn))
                {
                    using (var reader = GetAllCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                recordsList.Add(new CodingSession
                                {
                                    Id = reader.GetInt32(0),
                                    StartTime = TimeSpan.Parse(reader.GetString(1)),
                                    EndTime = TimeSpan.Parse(reader.GetString(2)),
                                });
                            }

                        }
                        else WriteLine("\n\nNo rows fount\n\n");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            WriteLine("Error: " + ex.Message);
        }

          TableVisualisation.ShowTable(recordsList);
    }

    public static void DeleteCodingRecordById(int id)
    {
        try
        {
            using (var conn = new SqliteConnection(connectionString)) 
            {
                conn.Open();
                string query = @"DELETE FROM Coding WHERE Id = @id ";

                int rowsAffected = conn.Execute(query, new { id });

                if (rowsAffected > 0)
                {
                    WriteLine($"\n\nRecord with Id {id} was deleted successfully.Preess any key to return.\n\n");
                    ReadKey();
                }
                else 
                {
                    WriteLine($"\n\nNo record found with Id {id}. Nothing was deleted preess any key to return..\n\n");
                    ReadKey();
                }
            }
        }
        catch (Exception ex)
        {
            WriteLine("Error: " + ex.Message);
        }

    }


}
