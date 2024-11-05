using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Coding_Tracker.nikosnick13;

internal class DatabaseManager
{


    public void CreateTable(string connection) 
    {
        try
        {
            using (var conn = new SqliteConnection(connection)) 
            {
                conn.Open();
                string query = @"CREATE TABLE IF NOT EXISTS Coding(
                       Id INTEGER PRIMARY KEY AUTOINCREMENT,
                       StartTime DATETIME,
                       EndTime DATETIME,
                       Duration TEXT
                    )";

                conn.Execute(query);
            }
        }
        catch(Exception ex)
        {
            WriteLine( "Error "+ ex.Message);
        }   



    }

}
