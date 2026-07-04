using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace HabitLogger.Data
{
    public class HabitLoggerData
    {
        static string connectionString = "Data Source=mydatabase.db";
         
        public HabitLoggerData()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Habits (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Hours INTEGER, Date DATE)";
                command.ExecuteNonQuery();
            }
        }
        public void InsertHabit(string habitName, int hours, DateTime date)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Habits (Name, Hours, Date) VALUES (@name, @hours, @date)";
                command.Parameters.AddWithValue("@name", habitName);
                command.Parameters.AddWithValue("@hours", hours);
                command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                command.ExecuteNonQuery();
            }
        }

        public bool UpdateHabit( int newHours, DateTime date)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Habits SET Hours = @hours WHERE Date = @date";
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@hours", newHours);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public void ViewHabits()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Habits";
                using (var reader = command.ExecuteReader())
                {
                    var table = new Table();
                    table.Title("[yellow]Habits [/]");
                    table.Border(TableBorder.Rounded);
                    table.AddColumn(new TableColumn("[bold]ID[/]").Centered());
                    table.AddColumn(new TableColumn("[bold]Name[/]").Centered());
                    table.AddColumn(new TableColumn("[bold]Hours[/]").Centered());
                    table.AddColumn(new TableColumn("[bold]Date[/]").Centered());
                    while (reader.Read())
                    {
                        table.AddRow(
    reader.GetInt32(0).ToString(),
    reader.GetString(1),
    reader.GetInt32(2).ToString(),
    reader.GetDateTime(3).ToString("yyyy-MM-dd"));
                    }
                    AnsiConsole.Write(table);
                }
            }
        }

        public bool DeleteHabit(string habitName)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Habits WHERE Name = @name " ;
                command.Parameters.AddWithValue("@name", habitName);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        }
}
