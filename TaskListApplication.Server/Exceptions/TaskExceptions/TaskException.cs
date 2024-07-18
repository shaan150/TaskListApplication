using TaskListApplication.Server.Enums;

namespace TaskListApplication.Server.Exceptions.TaskExceptions
{
    public class TaskException : Exception
    {
        public TaskException(TaskTypes type, string id, string additionalMessage = "", Exception innerException = null)
            : base(GenerateMessage(type, id, additionalMessage), innerException)
        {
        }

        private static string GenerateMessage(TaskTypes type, string id, string additionalMessage)
        {
            var baseMessage = $"{type} Exception: ID: {id}";
            if (!string.IsNullOrEmpty(additionalMessage))
            {
                baseMessage += $" {additionalMessage}";
            }
            return baseMessage;
        }
    }
}
