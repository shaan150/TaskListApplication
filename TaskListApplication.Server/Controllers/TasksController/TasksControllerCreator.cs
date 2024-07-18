using TaskListApplication.Server.Controllers.Utilities;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = TaskListApplication.Server.Models.Task;
using static Retriever = TaskListApplication.Server.Controllers.TasksController.TasksControllerRetriever;

namespace TaskListApplication.Server.Controllers.TasksController
{
    public static class TasksControllerCreator
    {
        // task prefix
        private const string TaskPrefix = "tk";

        private static string FindAvailableId(TaskContext _context)
        {
            string id = Utility.CreateId(TaskPrefix);
            while (Utility.TaskExists(_context, id).Result)
            {
                id = Utility.CreateId(TaskPrefix);
            }
            return id;
        }


        public static async Task<string> AddTask(TaskContext _context, TaskDto taskDto)
        {
            string id = FindAvailableId(_context);

            var task = new Task
            {
                Id = id,
                Title = taskDto.Title,
                IsComplete = taskDto.IsComplete,
                SubTasks = []
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // validate new task
            if (!Utility.TaskExists(_context, id).Result)
            {
                throw new Exception("Task not created");
            }

            return task.Id;
        }


        
    }
}
