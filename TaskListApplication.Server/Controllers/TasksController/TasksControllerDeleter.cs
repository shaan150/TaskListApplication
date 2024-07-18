using TaskListApplication.Server.Data;
using static Retriever = TaskListApplication.Server.Controllers.TasksController.TasksControllerRetriever;

namespace TaskListApplication.Server.Controllers.TasksController
{
    public static class TasksControllerDeleter
    {

        public static async Task<bool> DeleteTask(TaskContext _context, string id)
        {

            // check if task exists
            if (!await Utilities.Utility.TaskExists(_context, id))
            {
                throw new Exception("Task not found");
            }

            var task = await Retriever.GetTask(_context, id);

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            // check if task was deleted
            if (!await Utilities.Utility.TaskExists(_context, id))
            {
                return false;
            }

            return true;
        }
    }
}
