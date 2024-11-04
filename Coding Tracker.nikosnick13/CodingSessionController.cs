using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Coding_Tracker.nikosnick13;

internal class CodingSessionController
{
    static public string? connectionString = ConfigurationManager.AppSettings["ConnectionString"];

    public static void Update(CodingSession session)
    {
        using (var conn = new SqliteConnection(connectionString))
        {
            conn.Open();
            string query = @"UPDATE Coding SET StartTime = @startTime, EndTime = @endTime WHERE Id = @id";

            using (var editCmd = new SqliteCommand(query, conn))
            {

                editCmd.Parameters.AddWithValue("id", session.Id);
                editCmd.Parameters.AddWithValue("startTime",session.StartTime);
                editCmd.Parameters.AddWithValue("endTime" , session.EndTime);

                editCmd.ExecuteNonQuery();

            }
            WriteLine($"\n\nRecord with Id {session.Id} was updated. \n\n");
        }
    }

    public static CodingSession GetById(int id)
    {
        using (var conn = new SqliteConnection(connectionString))
        {
            conn.Open();
            string query = @$"SELECT * FROM Coding WHERE Id = @inputId";

            using (var GetIdCmd = new SqliteCommand(query, conn)) 
            {
                GetIdCmd.Parameters.AddWithValue("inputId", id);

                using(var reader = GetIdCmd.ExecuteReader()) 
                {
                    CodingSession coding = new();
                    if (reader.HasRows) 
                    {
                        reader.Read();
                        coding.Id = reader.GetInt32(0);
                        coding.StartTime  = TimeSpan.Parse(reader.GetString(1));
                        coding.EndTime =  TimeSpan.Parse(reader.GetString(2));
                    }
                    Console.WriteLine("\n\n");
                    return coding;
                }

            }
        }

    }


    public static void Post(CodingSession codingSession)
    {
        using (var conn = new SqliteConnection(connectionString)) 
        {
            conn.Open();
            string query = @"INSERT INTO coding (StartTime, EndTime, Duration) VALUES (@startTime, @endTime, @duration)";

            using (var insertCmd = new SqliteCommand(query, conn))
            {
                insertCmd.Parameters.AddWithValue("@startTime", codingSession.StartTime.ToString(@"hh\:mm"));
                insertCmd.Parameters.AddWithValue("@endTime", codingSession.EndTime.ToString(@"hh\:mm"));
                insertCmd.Parameters.AddWithValue("@duration", (codingSession.EndTime - codingSession.StartTime).ToString(@"hh\:mm"));
                insertCmd.ExecuteNonQuery();
            }

        }
    }

    public static void Get()
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
        catch(Exception ex)
        {
            WriteLine("Error: " + ex.Message);
        }

        TableVisualisation.ShowTable(recordsList);
    }
    public static void  Delete(int id) 
    {
        try
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                string query = @"DELETE FROM Coding WHERE Id = @id ";
                using (var cmdDelete = new SqliteCommand(query, conn))
                {
                    cmdDelete.Parameters.AddWithValue("id", id);

                    int rowsAffected = cmdDelete.ExecuteNonQuery();

                    //Chek if rows affected
                    if (rowsAffected > 0)
                    {
                        WriteLine($"\n\nRecord with Id {id} was deleted successfully.\n\n");
                    }
                    else
                    {
                        WriteLine($"\n\nNo record found with Id {id}. Nothing was deleted.\n\n");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            WriteLine("Error: " + ex.Message);
        }

    }


}
