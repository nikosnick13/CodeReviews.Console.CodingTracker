using System.Configuration;

namespace Coding_Tracker.nikosnick13
{
    internal class Program
    {
        static public string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

        //  static public string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");  

        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.CreatTable(connectionString);  
            

            UserMenu menu = new UserMenu();
            menu.MainMenu();

        }
    }
}
