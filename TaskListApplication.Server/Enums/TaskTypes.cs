using TaskListApplication.Server.Enums;

namespace TaskListApplication.Server.Enums
{
    public enum TaskTypes
    {
        Task,
        SubTask
    }

    public static class TaskTypesExtensions
    {
        public static string ToFriendlyString(this TaskTypes taskType)
        {
            return taskType switch
            {
                TaskTypes.Task => "TK",
                TaskTypes.SubTask => "ST",
                _ => "Unknown"
            };
        }
    }
}

