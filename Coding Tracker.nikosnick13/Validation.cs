﻿using Spectre.Console;
using System;
using System.Globalization;
using System.Linq;
using static System.Console;

namespace Coding_Tracker.nikosnick13;

internal class Validation
{
    public static bool ConfirmEdit(string msg)
    {

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>(msg)
                .AddChoice(true)
                .AddChoice(false)
                .DefaultValue(true)
                .WithConverter(choice => choice ? "y" : "n"));


        return confirmation;
    }

    public static bool ConfirmDeletion(int id)
    {

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>($"Do you want to delete it the ID:{id}?")
                .AddChoice(true)
                .AddChoice(false)
                .DefaultValue(true)
                .WithConverter(choice => choice ? "y" : "n"));


        return confirmation;
    }

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
