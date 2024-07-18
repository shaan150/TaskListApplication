// Controllers/TasksController.cs
using Microsoft.AspNetCore.Mvc;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using static TaskListApplication.Server.Controllers.TasksController.TasksControllerRetriever;
using static TaskListApplication.Server.Controllers.TasksController.TasksControllerCreator;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using TaskListApplication.Server.Exceptions;

namespace TaskListApplication.Server.Controllers.TasksController
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskContext _context;

        public TasksController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
        {
            try
            {
                return await GetTasks(_context, t => true);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(string taskId)
        {
            try
            {
                return await GetTaskDto(_context, taskId);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<String>> CreateTask(TaskCreateDto taskCreateDto)
        {
            try
            {
                var id = await AddTask(_context, taskCreateDto);
                return CreatedAtAction(nameof(GetTaskById), new { taskId = id }, taskCreateDto);
            }
            catch (CreationException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPut("update/{taskId}")]
        public async Task<IActionResult> UpdateTask(string taskId, TaskDto taskDto)
        {
            try
            {
                await TasksControllerUpdater.UpdateTask(_context, taskId, taskDto);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpDelete("delete/{taskId}")]
        public async Task<IActionResult> DeleteTask(string taskId)
        {
            try
            {
                bool deleted = await TasksControllerDeleter.DeleteTask(_context, taskId);
                if (!deleted)
                {
                    return BadRequest(new { message = "Unable to delete task." });
                }
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
