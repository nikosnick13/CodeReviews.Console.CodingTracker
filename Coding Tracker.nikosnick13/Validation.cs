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

    public static bool IsStringIsNull(string? str)
    {

        if (string.IsNullOrEmpty(str))
        {
            WriteLine("Connection string is missing or null.");
            return false;

        }
        return true;
    }

    public static bool IsValidDuration(string? timeInput)
    {
        return !TimeSpan.TryParseExact(timeInput, "hh\\:mm", CultureInfo.InvariantCulture, out _);
    }

    public static bool IsValidId(string? idInput)
    {
        return !Int32.TryParse(idInput, out _) || string.IsNullOrEmpty(idInput) || Int32.Parse(idInput) < 0;
    }
}
