using TaskListApplication.Server.Enums;
using TaskListApplication.Server.Exceptions.TaskExceptions;

namespace TaskListApplication.Server.Exceptions
{
    public class CreationException : TaskException
    {
        public CreationException(TaskTypes type, string id) : base(type, id, "Unable To Create")
        {
        }
    }
}
