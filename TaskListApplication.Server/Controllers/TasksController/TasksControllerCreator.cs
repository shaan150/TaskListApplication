using TaskListApplication.Server.Controllers.Utilities;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using Task = TaskListApplication.Server.Models.Task;
using TaskListApplication.Server.Exceptions;
using TaskListApplication.Server.Enums;

namespace TaskListApplication.Server.Controllers.TasksController
{
    public static class TasksControllerCreator
    {
        // task prefix
        
        private static readonly TaskTypes TaskType = Enums.TaskTypes.Task;
        private static readonly string TaskPrefix = TaskTypesExtensions.ToFriendlyString(TaskType);

        public static async Task<string> AddTask(TaskContext _context, TaskCreateDto taskCreateDto)
        {
            string id = await UtilityWrapper.FindAvailableIdFunc(_context, TaskPrefix, TaskType);

            Task task = new()
            {
                Id = id,
                Title = taskCreateDto.Title,
                IsComplete = taskCreateDto.IsComplete
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // validate new task
            if (!await UtilityWrapper.TaskExistsFunc(_context, TaskType, id))
            {
                throw new CreationException(Enums.TaskTypes.Task, id);
            }

            return task.Id;
        }


        
    }
}
