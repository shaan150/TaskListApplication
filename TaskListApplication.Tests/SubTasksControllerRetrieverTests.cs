using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Controllers.SubTasksController;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using Xunit;
using SubTask = TaskListApplication.Server.Models.SubTask;
using ThreadingTask = System.Threading.Tasks.Task;

namespace TaskListApplication.Server.Tests.Controllers.SubTasksController
{
    public class SubTasksControllerRetrieverTests
    {
        private TaskContext CreateInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new TaskContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async ThreadingTask GetSubTasksDtoShouldReturnSubTaskDtosWhenSubTasksExist()
        {
            // Arrange
            using var context = CreateInMemoryContext("GetSubTasksDtoShouldReturnSubTaskDtosWhenSubTasksExist");
            var subTask = new SubTask { Id = "subtask_1", Title = "Test SubTask", IsComplete = false, TaskId = "task_1" };
            await context.SubTasks.AddAsync(subTask);
            await context.SaveChangesAsync();

            Func<SubTaskDto, bool> filter = dto => dto.Title == "Test SubTask";

            // Act
            var result = await SubTasksControllerRetriever.GetSubTasksDto(context, filter);

            // Assert
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<IEnumerable<SubTaskDto>>>(result);
            var subTaskDtos = Assert.IsAssignableFrom<IEnumerable<SubTaskDto>>(actionResult.Value);
            Assert.Single(subTaskDtos);
            var subTaskDto = subTaskDtos.First();
            Assert.Equal("subtask_1", subTaskDto.Id);
            Assert.Equal("Test SubTask", subTaskDto.Title);
        }

        [Fact]
        public async ThreadingTask GetSubTasksDtoShouldReturnEmptyListWhenNoSubTasksExist()
        {
            // Arrange
            using var context = CreateInMemoryContext("GetSubTasksDtoShouldReturnEmptyListWhenNoSubTasksExist");

            Func<SubTaskDto, bool> filter = dto => true;

            // Act
            var result = await SubTasksControllerRetriever.GetSubTasksDto(context, filter);

            // Assert
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<IEnumerable<SubTaskDto>>>(result);
            var subTaskDtos = Assert.IsAssignableFrom<IEnumerable<SubTaskDto>>(actionResult.Value);
            Assert.Empty(subTaskDtos);
        }

        [Fact]
        public async ThreadingTask GetSubTaskDtoShouldReturnSubTaskDtoWhenSubTaskExists()
        {
            // Arrange
            using var context = CreateInMemoryContext("GetSubTaskDtoShouldReturnSubTaskDtoWhenSubTaskExists");
            var subTask = new SubTask { Id = "subtask_1", Title = "Test SubTask", IsComplete = false, TaskId = "task_1" };
            await context.SubTasks.AddAsync(subTask);
            await context.SaveChangesAsync();

            // Act
            var result = await SubTasksControllerRetriever.GetSubTaskDto(context, "subtask_1");

            // Assert
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<SubTaskDto>>(result);
            var subTaskDto = Assert.IsAssignableFrom<SubTaskDto>(actionResult.Value);
            Assert.Equal("subtask_1", subTaskDto.Id);
            Assert.Equal("Test SubTask", subTaskDto.Title);
        }

        [Fact]
        public async ThreadingTask GetSubTaskDtoShouldThrowNotFoundExceptionWhenSubTaskDoesNotExist()
        {
            // Arrange
            using var context = CreateInMemoryContext("GetSubTaskDtoShouldThrowNotFoundExceptionWhenSubTaskDoesNotExist");

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => SubTasksControllerRetriever.GetSubTaskDto(context, "non_existing_subtask"));
        }
    }
}
