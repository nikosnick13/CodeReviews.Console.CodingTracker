using Spectre.Console;
using System;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using static System.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coding_Tracker.nikosnick13;

internal class UserMenu
{
    enum Menu
    {
        Exit,
        View,
        Add,
        Edit,
        Delete
    }

    CodingSessionController codingSessionController = new();

    public void MainMenu()
    {
        bool isAppRunning = true;

        while (isAppRunning)
        {

            var menu = AnsiConsole.Prompt(
            new SelectionPrompt<Menu>()
                .Title("\n\nWhat would you like to do?")
                .AddChoices(Menu.Exit)
                .AddChoices(Menu.View)
                .AddChoices(Menu.Add)
                .AddChoices(Menu.Edit)
                .AddChoices(Menu.Delete)
                );

            switch (menu)
            {
                case Menu.Exit:
                    isAppRunning = false;
                    Environment.Exit(0);
                    break;
                case Menu.View:
                    CodingSessionController.GetAllCodingRecords();
                    break;
                case Menu.Add:
                    AddProcess();
                    break;
                case Menu.Edit:
                    ProcessEdit();
                    break;
                case Menu.Delete:
                    DeleteProsses();
                    break;
                default:
                    break;
            }
        }

    }

    private void DeleteProsses()
    {
        CodingSessionController.GetAllCodingRecords();
        WriteLine("Please enter the ID of the category you want to delete (or 0 to return to the main menu).");

        string? userDelete = ReadLine();

        if (userDelete == "0") MainMenu();

        while (Validation.IsValidId(userDelete))
        {
            WriteLine("\n\nInvalid input.Please enter a valid ID number.");
            if (userDelete == "0") MainMenu();
            userDelete = ReadLine();
        }

        //Parse the commantInput
        int id = Int32.Parse(userDelete);

        bool shouldDelete = Validation.ConfirmDeletion(id);

        if (shouldDelete) CodingSessionController.DeleteCodingRecordById(id);

        DeleteProsses();
    }


    private void AddProcess()
    {
        var GetStartTime = GetTimeInput("\nPlease insert the houre to start:(Format: hh:mm).Type 0 to return to main manu.");
        var GetEndTime = GetTimeInput("\nPlease insert the houre to end:(Format: hh:mm).Type 0 to return to main manu.");

        TimeSpan startTimeInput = TimeSpan.ParseExact(GetStartTime, "hh\\:mm", CultureInfo.InvariantCulture);
        TimeSpan endTimeInput = TimeSpan.ParseExact(GetEndTime, "hh\\:mm", CultureInfo.InvariantCulture);

        CodingSession codingSession = new();


        codingSession.StartTime = startTimeInput;
        codingSession.EndTime = endTimeInput;

        CodingSessionController.InsertCodingRecord(codingSession);
    }

    private string GetTimeInput(string? msg)
    {
        WriteLine(msg);

        string? timeInput = ReadLine();

        if (timeInput == "0") MainMenu();


        while (Validation.IsValidDuration(timeInput))
        {
            WriteLine("\n\nInvalid input.Please insert the date with the (Format: hh:mm)");
            timeInput = ReadLine();
        }
        return timeInput!;
    }

    private void ProcessEdit()
    {
        CodingSessionController.GetAllCodingRecords();
        WriteLine("Please add id of the category you want to update (or 0 to return to Main Menu).");

        string? userEdit = ReadLine();

        while (Validation.IsValidId(userEdit))
        {
            WriteLine("\n\nInvalid input. Please enter a valid id number.");
            userEdit = ReadLine();
        }

        var id = Int32.Parse(userEdit);

        if (id == 0) MainMenu();

        var coding = CodingSessionController.GetById(id);

        if (coding == null)
        {
            WriteLine($"Record with id {id} doesn't exist. Press any key to return to editing.");
            ReadLine();
            ProcessEdit();
            return;
        }

        bool shouldEdit = Validation.ConfirmEdit("Do you want edit the start houre? ");

        if (shouldEdit) 
        {
            var newStartTimeInput = GetTimeInput("\nPlease insert the new houre to start:(Format: hh:mm).Type 0 to return to main manu.");
            coding.StartTime = TimeSpan.Parse(newStartTimeInput);
        }

        shouldEdit = Validation.ConfirmEdit("Do you want edit the end houre?");

        if (shouldEdit)
        {
            var newEndtTimeInput = GetTimeInput("\nPlease insert the new houre to end:(Format: hh:mm).Type 0 to return to main manu.");
            coding.EndTime = TimeSpan.Parse(newEndtTimeInput);
        }

        CodingSessionController.UpdateCodingRecord(coding);

    }




}



