using HabitLogger.Core;
using Spectre.Console;

namespace HabitLogger
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            var core = new HabitLoggerCore();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("\nWelcome to the Habit Logger.");
            bool exit = true;
            while (exit)
            {
                AnsiConsole.Clear();
                string choice  = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("\nWhat would you like to do?").AddChoices(new[] { "Log a habit", "View habits", "Update hours", "Delete habit", "Exit" }));
                switch(choice)
                {
                    case "Log a habit":
                        // Logic to log a habit
                        string habitName = GetHabitName();
                        int hourSpend = GetHours();
                        DateTime dateInput = GetDate();
                        AnsiConsole.WriteLine(core.LogHabit(habitName, hourSpend, dateInput));
                        break; 
                    case "View habits":
                        // Logic to view habits
                        AnsiConsole.WriteLine(core.ViewHabits());
                        break;
                    case "Update hours":
                        // Logic to update hours
                        string habitToUpdate = GetHabitName();
                        int newHours = GetHours();
                        DateTime updateDate = GetDate();
                        AnsiConsole.WriteLine(core.UpdateHours(habitToUpdate, newHours, updateDate));
                        break;
                    case "Delete habit":
                        // Logic to delete a habit
                        string habitToDelete = GetHabitName();
                        AnsiConsole.WriteLine(core.DeleteHabit(habitToDelete));
                        break;
                    case "Exit":
                        exit = false;
                        break;
                }
                if(choice != "Exit")
                {
                    AnsiConsole.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
               
            }
        }

        static string GetHabitName()
        {
            string habitName = AnsiConsole.Ask<string>("Enter the habit name:");
            while (string.IsNullOrEmpty(habitName) || !habitName.All(char.IsLetter))
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[red]Invalid habit name. Please enter a valid habit name.[/]");
                habitName = AnsiConsole.Ask<string>("Enter the habit name:");
            }
            return habitName;
        }

        static int GetHours()
        {
            string hourSpend = AnsiConsole.Ask<string>("Enter the number of hours spent on this habit:");
            while (!int.TryParse(hourSpend, out int hours))
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[red]Invalid input. Please enter a valid number of hours.[/]");
                hourSpend = AnsiConsole.Ask<string>("Enter the number of hours spent on this habit:");
            }
            return int.Parse(hourSpend);
        }

        static DateTime GetDate()
        {
            string dateInput = AnsiConsole.Ask<string>("Enter the date (yyyy-MM-dd):");
            while (!DateTime.TryParse(dateInput, out DateTime date))
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[red]Invalid date format. Please enter a valid date in the format yyyy-MM-dd.[/]\n");
                dateInput = AnsiConsole.Ask<string>("Enter the date (yyyy-MM-dd):");
            }
            return DateTime.Parse(dateInput);

        }
    }
}
