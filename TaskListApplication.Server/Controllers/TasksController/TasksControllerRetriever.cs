using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;

namespace TaskListApplication.Server.Controllers.TasksController
{
    public static class TasksControllerRetriever
    {

        // This class is used to retrieve tasks from the database
        public static async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks(TaskContext context, System.Func<TaskDto, bool> filter)
        {
            var tasks = await context.Tasks.Include(t => t.SubTasks).ToListAsync();
            return tasks.Select(t => new TaskDto
            {
                Title = t.Title,
                IsComplete = t.IsComplete,
                SubTasks = t.SubTasks.Select(st => new SubTaskDto
                {
                    Id = st.Id,
                    Title = st.Title,
                    IsComplete = st.IsComplete,
                    TaskId = st.TaskId
                }).ToList()
            }).Where(filter).ToList();
        }

        // get tasks by id
        public static async Task<Models.Task> GetTask(TaskContext context, string id)
        {
            var task = await context.Tasks.Include(t => t.SubTasks).FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                throw new Exception("Task not found");
            }

            return task;
        }

        // get tasksdto 

        public static async Task<ActionResult<TaskDto>> GetTaskDto(TaskContext context, string id)
        {
            var task = await GetTask(context, id);

            return new TaskDto
            {
                Title = task.Title,
                IsComplete = task.IsComplete,
                SubTasks = task.SubTasks.Select(st => new SubTaskDto
                {
                    Id = st.Id,
                    Title = st.Title,
                    IsComplete = st.IsComplete,
                    TaskId = st.TaskId
                }).ToList()
            };
        }
    }
}
