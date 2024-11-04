 
using ConsoleTableExt;

using static System.Console;

namespace Coding_Tracker.nikosnick13
{
    internal class TableVisualisation
    {
        internal static void ShowTable<T>(List<T> recordsList) where T : class
        {
            Clear();
            ConsoleTableBuilder
                .From(recordsList)
                .WithTitle("Coding")
                .ExportAndWriteLine();

            WriteLine("\n\n");
        }
    }
}