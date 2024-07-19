using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using static TaskListApplication.Server.Controllers.Utilities.Utility;
using Task = TaskListApplication.Server.Models.Task;

namespace TaskListApplication.Server.Controllers.TasksController
{
    public static class TasksControllerRetriever
    {

        // This class is used to retrieve tasks from the database
        public static async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks(TaskContext context, System.Func<TaskDto, bool> filter)
        {
            List<Task> tasks = await context.Tasks
                .Include(t => t.SubTasks)
                .ToListAsync();

            if (tasks.Count == 0)
            {
                return new List<TaskDto>();
            }


            return tasks.Select(t => ConvertTaskToDto(t))
                .Where(filter)
                .ToList();
        }

        private static TaskDto ConvertTaskToDto(Task t)
        {
            return new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                IsComplete = t.IsComplete,
                SubTasks = t.SubTasks
                            .Select(ConvertSubTaskToDto)
                            .ToList(),
                SubTasksCount = t.SubTasks.Count
            };
        }


        // get tasks by id
        public static async Task<Models.Task> GetTask(TaskContext context, string id)
        {
            return await context.Tasks
                .Include(t => t.SubTasks)
                .FirstOrDefaultAsync(t => t.Id == id) 
                ?? 
                throw new NotFoundException(Enums.TaskTypes.Task, id);
        }

        // get tasksdto 

        public static async Task<ActionResult<TaskDto>> GetTaskDto(TaskContext context, string id)
        {
            Task task = await GetTask(context, id);

            int subTasksCount = task.SubTasks.Count;

            return ConvertTaskToDto(task);
        }
    }
}
