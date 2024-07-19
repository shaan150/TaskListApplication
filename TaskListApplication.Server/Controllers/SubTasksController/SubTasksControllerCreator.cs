using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskListApplication.Server.Controllers.Utilities;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Enums;
using TaskListApplication.Server.Exceptions;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using TaskListApplication.Server.Models;

namespace TaskListApplication.Server.Controllers.SubTasksController
{
    public static class SubTasksControllerCreator
    {
        private static readonly string SubTasksPrefix = TaskTypesExtensions.ToFriendlyString(Enums.TaskTypes.SubTask);
        private static readonly TaskTypes TaskType = Enums.TaskTypes.SubTask;

        public static async Task<string> AddSubTask(TaskContext _context, SubTaskCreateDto subTaskDto)
        {
            string taskId = subTaskDto.TaskId;

            // validate task
            if (!await Utility.TaskExists(_context, Enums.TaskTypes.Task, taskId))
            {
                throw new NotFoundException(Enums.TaskTypes.Task, taskId);
            }

            string id = await UtilityWrapper.FindAvailableIdFunc(_context, SubTasksPrefix, TaskType);

            SubTask subTask = new()
            {
                Id = id,
                TaskId = taskId,
                Title = subTaskDto.Title,
                IsComplete = subTaskDto.IsComplete
            };

            _context.SubTasks.Add(subTask);
            await _context.SaveChangesAsync();

            // validate new subtask
            if (!await UtilityWrapper.TaskExistsFunc(_context, TaskType, id))
            {
                throw new CreationException(TaskType, id);
            }

            return subTask.Id;
        }

        
    }
}
