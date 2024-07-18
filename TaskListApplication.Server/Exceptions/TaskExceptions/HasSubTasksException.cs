namespace TaskListApplication.Server.Exceptions.TaskExceptions
{
    public class HasSubTasksException : TaskException
    {
        public HasSubTasksException(string id) : base(Enums.TaskTypes.Task, id, "Cannot delete a task that has sub-tasks.")
        {
        }
    }
}
