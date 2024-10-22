using Microsoft.Data.Sqlite;
using Spectre.Console;
using System.Configuration;
using System.Data.SQLite;
using static System.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coding_Tracker.nikosnick13;

internal class CodingController
{
    static public string? connectionString = ConfigurationManager.AppSettings["ConnectionString"];

    
    public void Post(CodingSession coding)
    {
        using (var conn  = new SQLiteConnection(connectionString)) 
        {
            conn.Open();
            string query = @"INSERT INTO coding (StartTime ,EndTime) VALUES (@startTime, @endTime)";

            using (var insertCmd = new SQLiteCommand(query,conn)) 
            {

                insertCmd.Parameters.AddWithValue("startTime", coding.StartTime );
                insertCmd.Parameters.AddWithValue("endTime", coding.EndTime);

                insertCmd.ExecuteNonQuery();
            }
        }
    }

    public void Get() 
    {
        List<CodingSession> recordsList = new List<CodingSession>();

        try
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT * FROM coding ";

                using (var viewCmd = new SQLiteCommand(query, conn))
                {
                    using (var reader = viewCmd.ExecuteReader()) 
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                recordsList.Add(new CodingSession
                                {
                                    Id = reader.GetInt32(0),
                                    StartTime = reader.GetDateTime(1),
                                    EndTime = reader.GetDateTime(2),
                                });
                            }
                        }
                        else 
                        {
                            WriteLine("\n\nNo rows fount\n\n");
                        }
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

    public void Update(CodingSession coding)
    {
        using (var conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string query = @"UPDATE coding SET Date = @date, Duration = @duration   WHERE Id = @id ";

            using(var editCmd = new SQLiteCommand(query,conn))
            {
                editCmd.Parameters.AddWithValue("id", coding.Id);
                editCmd.Parameters.AddWithValue("date", coding.Date);
                editCmd.Parameters.AddWithValue("duration", coding.Duration);

                editCmd.ExecuteNonQuery();

            }
        }
        WriteLine($"\n\nRecord with Id {coding.Id} was updated. \n\n");
    }

    public void Delete(int id) 
    {

        using(var conn = new SQLiteConnection(connectionString)) 
        {
            conn.Open();
            string query = @"DELETE FROM coding WHERE Id = @id ";

            using (var cmdDelete = new SQLiteCommand(query,conn)) 
            {
                cmdDelete.Parameters.AddWithValue("id", id);
                cmdDelete.ExecuteNonQuery();

                WriteLine($"\n\nRecord with Id {id} was deleted. \n\n");
            }

        }
        
    }

    internal CodingSession GetById(int id)
    {
        using (var conn = new  SQLiteConnection(connectionString) )
        {
            conn.Open();
            string query = @"SELECT * FROM coding WHERE Id = @inputId";
            using (var cmdGetById = new SQLiteCommand(query,conn)) 
            {
                cmdGetById.Parameters.AddWithValue("inputId", id);

                //Check if has rows
                using (var reader = cmdGetById.ExecuteReader()) 
                {
                    CodingSession coding = new CodingSession();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        coding.Id = reader.GetInt32(0);
                        coding.Duration = reader.GetString(1);
                        coding.Date=  reader.GetString(2);

                    }
                    Console.WriteLine("\n\n");
                    return coding;
                }
            }

        }
        
    }
}