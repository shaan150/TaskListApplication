using TaskListApplication.Server.Enums;

namespace TaskListApplication.Server.Exceptions.TaskExceptions
{
    public class NotFoundException : TaskException
    {
        public NotFoundException(TaskTypes type, string id) : base(type, id, "Not Found")
        {

        }
    }
}
