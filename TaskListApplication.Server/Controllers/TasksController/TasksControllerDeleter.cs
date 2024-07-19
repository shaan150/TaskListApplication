using TaskListApplication.Server.Data;
using TaskListApplication.Server.Enums;
using TaskListApplication.Server.Controllers.Utilities;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using Task = TaskListApplication.Server.Models.Task;
using static TaskListApplication.Server.Controllers.TasksController.TasksControllerRetriever;

namespace TaskListApplication.Server.Controllers.TasksController
{
    public static class TasksControllerDeleter
    {

        public static async Task<bool> DeleteTask(TaskContext _context, string id)
        {

            // check if task exists
            if (!await UtilityWrapper.TaskExistsFunc(_context, TaskTypes.Task, id))
            {
                throw new NotFoundException(Enums.TaskTypes.Task, id);
            }

            Task task = await GetTask(_context, id);

            // check if there are subtasks
            if (task.SubTasks.Count > 0)
            {
                throw new HasSubTasksException(id);
            }


            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            // check if task was deleted
            if (!await UtilityWrapper.TaskExistsFunc(_context, TaskTypes.Task, id))
            {
                return true;
            }

            return false;
        }
    }
}
