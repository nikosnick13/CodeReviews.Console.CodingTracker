using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Tracker.nikosnick13
{
    internal class DatabaseManager
    {

        public void CreatTable(string connection)
        {
            try
            {
                using (var conn = new SqliteConnection(connection))
                {
                    conn.Open();
                    string query = @"CREATE TABLE IF NOT EXISTS coding(
                       Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime DATETIME,
                        EndTime DATETIME,
                        Duration TEXT
                    )";

                    using (var cmdTable = new SqliteCommand(query, conn))
                    {
                        cmdTable.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


    }
}