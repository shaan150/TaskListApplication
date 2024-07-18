using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Enums;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using SubTask = TaskListApplication.Server.Models.SubTask;

namespace TaskListApplication.Server.Controllers.SubTasksController
{
    public static class SubTasksControllerUpdater
    {
        public static async Task UpdateSubTask(TaskContext _context, SubTaskDto subTaskDto)
        {
            string id = subTaskDto.Id;

            SubTask subTask = await _context.SubTasks.FirstOrDefaultAsync(t => t.Id == id) ?? throw new NotFoundException(TaskTypes.SubTask, id);

            subTask.Title = subTaskDto.Title;
            subTask.IsComplete = subTaskDto.IsComplete;
            subTask.TaskId = subTaskDto.TaskId;

            _context.Entry(subTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
