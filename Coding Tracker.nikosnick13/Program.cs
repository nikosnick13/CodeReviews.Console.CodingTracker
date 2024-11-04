using System.Configuration;
using System.Threading.Tasks;
using static System.Console;

namespace Coding_Tracker.nikosnick13;

internal class Program
{


    public static string? connectionString = ConfigurationManager.AppSettings["ConnectionString"];

    static void Main(string[] args)
    {

        DatabaseManager databaseManager = new DatabaseManager();
        databaseManager.CreateTable(connectionString!);

        UserMenu userMenu = new();
        userMenu.MainMenu();
    }
}
