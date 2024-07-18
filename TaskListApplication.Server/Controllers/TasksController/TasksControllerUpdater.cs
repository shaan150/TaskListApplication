using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Models;
using static Retriever = TaskListApplication.Server.Controllers.TasksController.TasksControllerRetriever;

namespace TaskListApplication.Server.Controllers.TasksController
{
    public static class TasksControllerUpdater
    {
        public static async Task<bool> UpdateTask(TaskContext _context, string id, TaskDto taskDto)
        {

            // check if task exists
            if (!await Utilities.Utility.TaskExists(_context, id))
            {
                throw new Exception("Task not found");
            }

            var task = await Retriever.GetTask(_context, id);

            task.Title = taskDto.Title;
            task.IsComplete = taskDto.IsComplete;;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
