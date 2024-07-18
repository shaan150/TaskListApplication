using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.Enums;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using TaskListApplication.Server.Models;
using static TaskListApplication.Server.Controllers.Utilities.Utility;

namespace TaskListApplication.Server.Controllers.SubTasksController
{
    public static class SubTasksControllerDeleter
    {
        public static async Task<bool> DeleteSubTask(TaskContext _context, string id)
        {
            SubTask subTask = await _context.SubTasks.FirstOrDefaultAsync(t => t.Id == id) ?? throw new NotFoundException(TaskTypes.SubTask, id);

            _context.SubTasks.Remove(subTask);
            await _context.SaveChangesAsync();

            bool exists = await TaskExists(_context, TaskTypes.SubTask, id);

            return !exists;

        }
    }
}
