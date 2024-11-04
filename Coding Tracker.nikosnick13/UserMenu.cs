using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

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
        bool isAppRuning = true;

        while (isAppRuning)
        {

            var menu = AnsiConsole.Prompt(
            new SelectionPrompt<Menu>()
                .Title("\n\nWhat you like to do?")
                .AddChoices(Menu.Exit)
                .AddChoices(Menu.View)
                .AddChoices(Menu.Add)
                .AddChoices(Menu.Edit)
                .AddChoices(Menu.Delete)
                );

            switch (menu)
            {
                case Menu.Exit:
                    isAppRuning = false;
                    Environment.Exit(0);
                    break;
                case Menu.View:
                    CodingSessionController.Get();
                    break;
                case Menu.Add:
                    AddProsses();
                    break;
                case Menu.Edit:
                    ProssesEdit();
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
        CodingSessionController.Get();
        WriteLine("Please add id of the category you want to delete (or 0 to return to Main Menu).");

        string? userDelete = ReadLine();

        if (userDelete == "0") MainMenu();

        while(Validation.IsValidId(userDelete)) 
        {
            WriteLine("\n\nInvalid input.Please enter a valid id number.");
            if (userDelete == "0") MainMenu();
            userDelete = ReadLine();
        }

        //Parse the commantInput
        int id = Int32.Parse(userDelete);
        var coding = CodingSessionController.GetById(id);

    
        CodingSessionController.Delete(id);
        DeleteProsses();
    }


    private void AddProsses() 
    {
        var GetStartTime = GetTimeInput("\nPlease insert the houre to start:(Format: hh:mm).Type 0 to return to main manu.");
        var GetEndTime = GetTimeInput("\nPlease insert the houre to end:(Format: hh:mm).Type 0 to return to main manu.");

        TimeSpan startTimeInput = TimeSpan.ParseExact(GetStartTime, "hh\\:mm", CultureInfo.InvariantCulture);
        TimeSpan endTimeInput = TimeSpan.ParseExact(GetEndTime, "hh\\:mm", CultureInfo.InvariantCulture);

        CodingSession codingSession = new();


        codingSession.StartTime = startTimeInput;
        codingSession.EndTime = endTimeInput;

        CodingSessionController.Post(codingSession);
 

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

    private void ProssesEdit() 
    {
        CodingSessionController.Get();
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

            while (coding.Id == 0)
            {
                WriteLine($"\nRecord with id {id} doesn't exist\n");
                ProssesEdit();
            }

        var  newStartTimeInput = GetTimeInput("\nPlease insert the houre to start:(Format: hh:mm).Type 0 to return to main manu.");

        var newEndtTimeInput = GetTimeInput("\nPlease insert the houre to end:(Format: hh:mm).Type 0 to return to main manu.");

        coding.StartTime = TimeSpan.Parse(newStartTimeInput);
        coding.EndTime = TimeSpan.Parse(newEndtTimeInput);

        CodingSessionController.Update(coding);

    }




}
 

    
