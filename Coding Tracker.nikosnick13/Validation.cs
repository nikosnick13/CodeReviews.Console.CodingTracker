using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Coding_Tracker.nikosnick13;

internal class Validation
{
    #region ValidationsForInputs
    public static bool IsValidDate(string? dateInput)
    {
         return !DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _);
        
    }

    public static bool IsValidDuration(string? durationInput)
    {
        return !TimeSpan.TryParseExact(durationInput, "h\\:mm", CultureInfo.InvariantCulture, out _);
    }
    #endregion

    #region ValidationsForIds
    public static void CheckForZero(string? inputUser)
    {
        if (inputUser == "0")
        {
            UserMenu userMenu = new();
            userMenu.MainMenu();
        }
    }

    public static bool IsValidId(string? idInput) 
    {
        return !Int32.TryParse(idInput, out _) || string.IsNullOrEmpty(idInput) || Int32.Parse(idInput) < 0 ;
    }

    public static bool IsExistingRecord(int id, CodingSession coding) 
    {   
        return coding.Id != 0;
    }
    #endregion

}
