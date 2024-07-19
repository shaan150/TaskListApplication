using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using TaskListApplication.Server.Enums;
using Task = TaskListApplication.Server.Models.Task;
using static TaskListApplication.Server.Controllers.Utilities.Utility;
using static TaskListApplication.Server.Controllers.TasksController.TasksControllerRetriever;

namespace TaskListApplication.Server.Controllers.TasksController
{
    public static class TasksControllerUpdater
    {
        public static async Task<bool> UpdateTask(TaskContext _context, string id, TaskDto taskDto)
        {
            
            // check if task exists
            if (!await TaskExists(_context, TaskTypes.Task, id))
            {
                throw new NotFoundException(TaskTypes.Task, id);
            }

            Task task = await GetTask(_context, id);


            task.Title = taskDto.Title;
            task.IsComplete = taskDto.IsComplete;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // check if task was updated

            // get updated task
            Task updatedTask = await GetTask(_context, id);
            
            if (task.Title != updatedTask.Title || task.IsComplete != updatedTask.IsComplete)
            {
                return false;
            }

            return true;
        }
    }
}
