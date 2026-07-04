using HabitLogger.Data;

namespace HabitLogger.Core
{
    public class HabitLoggerCore
    {
        private readonly HabitLoggerData _habitLoggerData;

        public HabitLoggerCore()
        {
            _habitLoggerData = new HabitLoggerData();
        }
        public string LogHabit(string habitName, int hours, DateTime date)
        {
            // Logic to log a habit
            try
            {
                _habitLoggerData.InsertHabit(habitName, hours, date);
                return $"Habit '{habitName}' logged with {hours} hours. \n";
            }
            catch (Exception ex)
            {
                return $"Error logging habit: {ex.Message}\n";
            }
        }

        public string ViewHabits()
        {
            // Logic to view habits
            try
            {
                _habitLoggerData.ViewHabits();
                return "\nViewing all logged habits.\n";
            }
            catch (Exception ex)
            {
                return $"Error viewing habits: {ex.Message}\n";
            }
        }


        public string UpdateHours(string habitName, int newHours, DateTime date)
        {
            // Logic to update hours for a habit
            try
            {
                if(_habitLoggerData.UpdateHabit( newHours, date))
                    return $"Updated '{habitName}' to {newHours} hours.\n";
                else
                    return $"No habit found for '{habitName}' on {date.ToShortDateString()}.\n";
            }
            catch (Exception ex)
            {
                return $"Error updating habit: {ex.Message}\n";
            }
        }


        public string DeleteHabit(string habitName)
        {
            // Logic to delete a habit
            try
            {
               if(_habitLoggerData.DeleteHabit(habitName))
                {
                    return $"Habit '{habitName}' deleted.\n";
                }
               else
                {
                    return $"Habit '{habitName}' not deleted.\n";
                }
                    
            }
            catch (Exception ex)
            {
                return $"Error deleting habit: {ex.Message}\n";
            }
        }


    }
}
