# HabitLogger
 
My first C# console application built with Visual Studio. A CRUD app to track habits and the hours spent on them, developed using C# and SQLite.
 
## Given Requirements
 
- When the application starts, it should create a SQLite database, if one isn't present.
- It should also create a table in the database, where habits will be logged.
- You need to be able to insert, delete, update, and view your logged habits.
- You should handle all possible errors so the application never crashes.
- You can only interact with the database using raw SQL — no mappers such as Entity Framework.
- The application should validate all user input (habit name, hours, date) before touching the database.
## Features
 
### SQLite database connection
The program uses a SQLite connection to store and read habit data. If no database or table exists, both are created automatically on program start.
 
### A console-based UI with Spectre.Console
Users navigate a menu using arrow keys, powered by the [Spectre.Console](https://spectreconsole.net/) library, with colored feedback for errors and results.
 
### CRUD DB functions
From the main menu, users can log, view, update, or delete habit entries. Habit names, hours, and dates are all validated before being sent to the database, and delete/update operations correctly report back whether a matching entry was actually found.
 
### Layered project structure
The solution is split into three projects instead of one big Program.cs:
- **HabitLogger.Data** — raw SQL and SQLite connections
- **HabitLogger.Core** — business logic, delegates persistence to the Data layer
- **HabitLogger** — console UI, menus, and input handling
### Reports
Logged habits are displayed in a formatted table (ID, Name, Hours, Date) using Spectre.Console's `Table` component instead of plain `Console.WriteLine` output.
 
## Challenges
 
- **Namespace confusion.** Early on, my `using` statements didn't match the actual namespaces declared in my other projects (`HabitLogger.Core` vs `HabitLoggerCore`), which caused build errors that weren't obvious at first — the fix was making sure the `using` directive and the `namespace` declaration matched exactly.
- **Understanding dependency injection.** I didn't initially understand why `HabitLoggerCore` needed a `HabitLoggerData` object passed into its constructor instead of just creating one internally, or why instance methods couldn't be called without an object to call them on.
- **SQLite provider initialization.** Ran into a `SQLitePCLRaw` error (`You need to call SQLitePCL.raw.SetProvider()`) the first time I tried to open a connection — fixed by installing the `SQLitePCLRaw.bundle_e_sqlite3` NuGet package and calling `Batteries.Init()` before any connection is opened.
- **Silent failures on Update/Delete.** `ExecuteNonQuery()` doesn't throw an error when a `WHERE` clause matches zero rows — it just does nothing. I had to check the "rows affected" count myself to correctly tell the user whether a habit was actually found and deleted/updated, rather than assuming success just because no exception was thrown.
- **DateTime formatting.** Dates read back from SQLite came out with a `00:00:00` time component attached, even though only a date was ever stored — had to explicitly format with `:yyyy-MM-dd` when displaying.
- **Markup vs plain text output.** Spent time confused about why `[red]...[/]` wasn't showing color — `AnsiConsole.WriteLine` prints markup tags literally; `AnsiConsole.MarkupLine` is what actually parses and renders them.
## Lessons Learned
 
- **Separate input-gathering from logic.** Instead of validating and using input in the same method, I split things into small `GetHabitName()`, `GetHours()`, `GetDate()` methods that only gather and validate input, then pass clean data into the logic layer. This avoided duplicating validation code across every menu case.
- **Check return values, don't assume success.** `ExecuteNonQuery()` returning without an exception doesn't mean the operation did what I expected — checking `rowsAffected` was necessary to catch "no matching row" cases.
- **One object, reused, not recreated.** Understanding *object scope* — creating `HabitLoggerCore` once, outside the main loop, and reusing it — rather than recreating it inside every switch case.
## Areas to Improve
 
- Add a confirmation step before deleting a habit, so a mistyped name doesn't silently do nothing (or worse, get confirmed by accident later).
- Look into exceptions instead of string-based `"Error: ..."` return values for cleaner error handling in the Core layer.
- Learn more about unit testing so `HabitLogger.Core` and `HabitLogger.Data` can be tested without needing to run the whole console app.
- Explore adding reporting features (e.g., total hours per habit, weekly summaries) using more advanced SQL aggregate queries.
## Getting Started
 
1. Clone the repository
```bash
   git clone https://github.com/Sangeerths/HabitLogger
   cd HabitLogger
```
2. Restore dependencies
```bash
   dotnet restore
```
3. Build and run
```bash
   dotnet build
   dotnet run --project HabitLogger
```
 
On first run, the app creates a local SQLite database file (`mydatabase.db`) and the `Habits` table automatically.
