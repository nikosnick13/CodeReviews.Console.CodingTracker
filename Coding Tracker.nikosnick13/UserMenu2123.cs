using Spectre.Console;
using System;
using System.Collections.Generic;
using static System.Console;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker.nikosnick13
{

    enum Menu
    {
        Exit,
        View,
        Add,
        Edit,
        Delete
    }

    internal class UserMenu2123
    {

        CodingController codingController = new();

        public void MainMenu123()
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
                        codingController.Get();
                        break;
                    case Menu.Add:
                        ProssesAdd();
                        break;
                    case Menu.Edit:
                        ProssesEdit();
                        break;
                    case Menu.Delete:
                        ProcessDelete();
                        break;
                    default:
                        break;
                }
            }


        }

        private void ProssesAdd()
        {
            Clear();

            var  startTime = GetTimeInput("Please insert the houre to start:(Format: hh:mm).Type 0 to return to main manu.");

            var endTime = GetTimeInput("Please insert the houre to start:(Format: hh:mm).Type 0 to return to main manu.");

            TimeSpan startTimeInput = TimeSpan.ParseExact(startTime, "hh\\:mm", CultureInfo.InvariantCulture);
            TimeSpan endTimeInput = TimeSpan.ParseExact(endTime, "hh\\:mm", CultureInfo.InvariantCulture);

            CodingSession coding = new();
            coding.StartTime = startTimeInput;
            coding.EndTime = endTimeInput; 

            codingController.Post(coding);
        }

        public string GetTimeInput(string msg)
        {
            WriteLine("\n\n ");
            string timeInput = ReadLine();

            if (timeInput == "0") MainMenu();


            while (Validation.IsValidDuration(timeInput))
            {
                WriteLine("\n\nInvalid input.Please insert the date with the (Format: hh:mm) ");
                timeInput = ReadLine();
            }

            return timeInput;
        }

       
























        private void ProcessDelete()
        {
            codingController.Get();
            WriteLine("Please add id of the category you want to delete (or 0 to return to Main Menu).");
            string commantInput = ReadLine();

            while (!Int32.TryParse(commantInput, out _) || string.IsNullOrEmpty(commantInput) || Int32.Parse(commantInput) < 0)
            {
                WriteLine("\nYou have to type a valid id (or 0 to return to Main Menu))");
                commantInput = ReadLine();
            }
            //Parse the commantInput
            var id = Int32.Parse(commantInput);
            if (id == 0) MainMenu();

            var coding = codingController.GetById(id);

            while (coding.Id == 0)
            {
                WriteLine($"\nRecord with id {id} doesn't exist\n");
                ProcessDelete();
            }
            codingController.Delete(id);

        }
        private void ProssesEdit()
        {

            codingController.Get();
            WriteLine("Please add id of the category you want to update (or 0 to return to Main Menu).");
            string userEdit = ReadLine();

            while (!Int32.TryParse(userEdit, out _) || Int32.Parse(userEdit) < 0 || string.IsNullOrEmpty(userEdit))
            {
                WriteLine();
                userEdit = ReadLine();
            }

            var id = Int32.Parse(userEdit);

            if (id == 0) MainMenu();

            var coding = codingController.GetById(id);

            while (coding.Id == 0)
            {
                WriteLine($"\nRecord with id {id} doesn't exist\n");
                ProssesEdit();
            }

            var newDuration = GetDurationInput();
            var newDate = GetDateInput();
 

            codingController.Update(coding);
        }



    }
}