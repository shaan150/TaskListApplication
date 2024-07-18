// Controllers/SubTasksController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Models;

namespace TaskListApplication.Server.Controllers.SubTasksController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTasksController : ControllerBase
    {
        private readonly TaskContext _context;

        public SubTasksController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubTaskDto>>> GetSubTasks()
        {
            var subTasks = await _context.SubTasks.ToListAsync();
            return subTasks.Select(st => new SubTaskDto
            {
                Id = st.Id,
                Title = st.Title,
                IsComplete = st.IsComplete,
                TaskId = st.TaskId
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubTaskDto>> GetSubTask(string id)
        {
            var subTask = await _context.SubTasks.FindAsync(id);

            if (subTask == null)
            {
                return NotFound();
            }

            var subTaskDto = new SubTaskDto
            {
                Id = subTask.Id,
                Title = subTask.Title,
                IsComplete = subTask.IsComplete,
                TaskId = subTask.TaskId
            };

            return subTaskDto;
        }

        [HttpPost]
        public async Task<ActionResult<SubTaskDto>> PostSubTask(SubTaskDto subTaskDto)
        {
            // unique id generator for subtask


            string id = "st" + Guid.NewGuid().ToString().Substring(0, 8);


            var subTask = new SubTask
            {
                Title = subTaskDto.Title,
                IsComplete = subTaskDto.IsComplete,
                TaskId = subTaskDto.TaskId
            };

            _context.SubTasks.Add(subTask);
            await _context.SaveChangesAsync();

            subTaskDto.Id = subTask.Id;
            return CreatedAtAction(nameof(GetSubTask), new { id = subTask.Id }, subTaskDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubTask(string id, SubTaskDto subTaskDto)
        {
            if (id != subTaskDto.Id)
            {
                return BadRequest();
            }

            var subTask = await _context.SubTasks.FindAsync(id);
            if (subTask == null)
            {
                return NotFound();
            }

            subTask.Title = subTaskDto.Title;
            subTask.IsComplete = subTaskDto.IsComplete;
            subTask.TaskId = subTaskDto.TaskId;

            _context.Entry(subTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubTask(string id)
        {
            var subTask = await _context.SubTasks.FindAsync(id);
            if (subTask == null)
            {
                return NotFound();
            }

            _context.SubTasks.Remove(subTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
