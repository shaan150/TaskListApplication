// Controllers/SubTasksController.cs
using Microsoft.AspNetCore.Mvc;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Exceptions;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using static TaskListApplication.Server.Controllers.SubTasksController.SubTasksControllerCreator;
using static TaskListApplication.Server.Controllers.SubTasksController.SubTasksControllerRetriever;

namespace TaskListApplication.Server.Controllers.SubTasksController
{
    [Route("api/subtasks")]
    [ApiController]
    public class SubTasksController : ControllerBase
    {
        private readonly TaskContext _context;

        public SubTasksController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<SubTaskDto>>> GetAllSubTasks()
        {
            try
            {
                return await GetSubTasksDto(_context, t => true);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("{subTaskId}")]
        public async Task<ActionResult<SubTaskDto>> GetSubTaskById(string subTaskId)
        {
            try
            {
                return await GetSubTaskDto(_context, subTaskId);
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
        public async Task<ActionResult<string>> CreateSubTask(SubTaskCreateDto subTaskCreateDto)
        {
            try
            {
                string id = await AddSubTask(_context, subTaskCreateDto);
                return CreatedAtAction(nameof(GetSubTaskById), new { subTaskId = id }, subTaskCreateDto);
            }
            catch (CreationException e)
            {
                return BadRequest(new { message = e.Message });
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

        [HttpPut("update/{subTaskId}")]
        public async Task<IActionResult> UpdateSubTask(string subTaskId, SubTaskDto subTaskDto)
        {
            try
            {
                await SubTasksControllerUpdater.UpdateSubTask(_context, subTaskDto);
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

        [HttpDelete("delete/{subTaskId}")]
        public async Task<IActionResult> DeleteSubTask(string subTaskId)
        {
            try
            {
                await SubTasksControllerDeleter.DeleteSubTask(_context, subTaskId);
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
