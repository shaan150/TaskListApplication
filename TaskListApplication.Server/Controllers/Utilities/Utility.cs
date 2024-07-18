using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Enums;
using TaskListApplication.Server.Models;

namespace TaskListApplication.Server.Controllers.Utilities
{
    public static class Utility
    {
        public static string CreateId(string prefix)
        {
            return $"{prefix}-{Guid.NewGuid().ToString()}";
        }

        // check if task exists
        public static async Task<bool> TaskExists(TaskContext context, TaskTypes types, string id)
        {
            switch(types)
            {
                case TaskTypes.Task:
                    return await context.Tasks.AnyAsync(t => t.Id == id);
                case TaskTypes.SubTask:
                    return await context.SubTasks.AnyAsync(st => st.Id == id);
                default:
                    throw new NotImplementedException();
            }
        }


        public static async Task<string> FindAvailableId(TaskContext _context, string prefix, TaskTypes type)
        {
            string id = CreateId(prefix);

            while (await TaskExists(_context, type, id))
            {
                id = CreateId(prefix);
            }

            return id;
        }

        public static SubTaskDto ConvertSubTaskToDto(SubTask st)
        {
            return new SubTaskDto
            {
                Id = st.Id,
                Title = st.Title,
                IsComplete = st.IsComplete,
                TaskId = st.TaskId
            };
        }

    }
}
