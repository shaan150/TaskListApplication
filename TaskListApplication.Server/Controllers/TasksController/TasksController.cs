// Controllers/TasksController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using static Retriever = TaskListApplication.Server.Controllers.TasksController.TasksControllerRetriever;
using static Creator = TaskListApplication.Server.Controllers.TasksController.TasksControllerCreator;
using static Updater = TaskListApplication.Server.Controllers.TasksController.TasksControllerUpdater;
using static Deleter = TaskListApplication.Server.Controllers.TasksController.TasksControllerDeleter;
using TaskListApplication.Server.Controllers.Utilities;

namespace TaskListApplication.Server.Controllers.TasksController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskContext _context;

        public TasksController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            return await Retriever.GetTasks(_context, t => true);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(string id)
        {
            try
            {
                return await Retriever.GetTaskDto(_context, id);
            }
            catch {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> PostTask(TaskDto taskDto)
        {
            try
            {
                var id = await Creator.AddTask(_context, taskDto);

                return CreatedAtAction(nameof(GetTask), new { id = id }, taskDto);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(string id, TaskDto taskDto)
        {
            try
            {
                await Updater.UpdateTask(_context, id, taskDto);

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await Utility.TaskExists(_context, id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            try
            {
                bool deleted = await Deleter.DeleteTask(_context, id);

                if (!deleted)
                {
                    return BadRequest("Unable to delete task.");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }
    }
}
