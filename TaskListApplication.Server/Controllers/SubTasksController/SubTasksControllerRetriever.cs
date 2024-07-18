using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Controllers.Utilities;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using TaskListApplication.Server.Models;
using static TaskListApplication.Server.Controllers.Utilities.Utility;

namespace TaskListApplication.Server.Controllers.SubTasksController
{
    public class SubTasksControllerRetriever
    {
        public static async Task<ActionResult<IEnumerable<SubTaskDto>>> GetSubTasksDto(TaskContext context, System.Func<SubTaskDto, bool> filter)
        {
            List<SubTask> tasks = await context.SubTasks.ToListAsync();

            if (tasks.Count == 0)
            {
                return new List<SubTaskDto>();
            }

            return tasks.Select(ConvertSubTaskToDto)
                .Where(filter)
                .ToList();
        }

        public static async Task<ActionResult<SubTaskDto>> GetSubTaskDto(TaskContext context, string id)
        {
            SubTask task = await context.SubTasks.FirstOrDefaultAsync(t => t.Id == id) ?? throw new NotFoundException(Enums.TaskTypes.SubTask, id);

            return ConvertSubTaskToDto(task);
        }
    }
}
