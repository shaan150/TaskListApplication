using Microsoft.AspNetCore.Mvc;
using TaskListApplication.Server.Controllers.TasksController;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using TaskListApplication.Server.Models;
using Xunit;
using static TaskListApplication.Tests.TestUtility;
using Task = TaskListApplication.Server.Models.Task;
using ThreadingTask = System.Threading.Tasks.Task;

namespace TaskListApplication.Server.Tests.Controllers.TasksController
{
    public class TasksControllerRetrieverTests
    {
        [Fact]
        public async ThreadingTask GetTasksShouldReturnTaskDtosWhenTasksExist()
        {
            // Arrange
            using var context = CreateInMemoryContext("GetTasksShouldReturnTaskDtosWhenTasksExist");
            var task = new Task { Id = "task_1", Title = "Test Task", IsComplete = false, SubTasks = new List<SubTask>() };
            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            Func<TaskDto, bool> filter = dto => dto.Title == "Test Task";

            // Act
            var result = await TasksControllerRetriever.GetTasks(context, filter);

            // Assert
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TaskDto>>>(result);
            var taskDtos = Assert.IsAssignableFrom<IEnumerable<TaskDto>>(actionResult.Value);
            Assert.Single(taskDtos);
            var taskDto = taskDtos.First();
            Assert.Equal("task_1", taskDto.Id);
            Assert.Equal("Test Task", taskDto.Title);
        }

        [Fact]
        public async ThreadingTask GetTasksShouldReturnEmptyListWhenNoTasksExist()
        {
            // Arrange
            using var context = CreateInMemoryContext("GetTasksShouldReturnEmptyListWhenNoTasksExist");

            Func<TaskDto, bool> filter = dto => true;

            // Act
            var result = await TasksControllerRetriever.GetTasks(context, filter);

            // Assert
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TaskDto>>>(result);
            var taskDtos = Assert.IsAssignableFrom<IEnumerable<TaskDto>>(actionResult.Value);
            Assert.Empty(taskDtos);
        }

        [Fact]
        public async ThreadingTask GetTaskShouldReturnTaskWhenTaskExists()
        {
            // Arrange
            using var context = CreateInMemoryContext("GetTaskShouldReturnTaskWhenTaskExists");
            string expectedId = "successful_return";
            var task = new Task { Id = expectedId, Title = "Test Task", IsComplete = false, SubTasks = new List<SubTask>() };
            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            // Act
            var result = await TasksControllerRetriever.GetTask(context, expectedId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedId, result.Id);
            Assert.Equal("Test Task", result.Title);
        }

        [Fact]
        public async ThreadingTask GetTaskShouldThrowNotFoundExceptionWhenTaskDoesNotExist()
        {
            // Arrange
            using var context = CreateInMemoryContext("GetTaskShouldThrowNotFoundExceptionWhenTaskDoesNotExist");

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => TasksControllerRetriever.GetTask(context, "non_existing_task"));
        }

        [Fact]
        public async ThreadingTask GetTaskDtoShouldReturnTaskDtoWhenTaskExists()
        {
            // Arrange
            using var context = CreateInMemoryContext("GetTaskDtoShouldReturnTaskDtoWhenTaskExists");
            var task = new Task { Id = "task_1", Title = "Test Task", IsComplete = false, SubTasks = new List<SubTask>() };
            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            // Act
            var result = await TasksControllerRetriever.GetTaskDto(context, "task_1");

            // Assert
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<TaskDto>>(result);
            var taskDto = Assert.IsAssignableFrom<TaskDto>(actionResult.Value);
            Assert.Equal("task_1", taskDto.Id);
            Assert.Equal("Test Task", taskDto.Title);
        }

        [Fact]
        public async ThreadingTask GetTaskDtoShouldThrowNotFoundExceptionWhenTaskDoesNotExist()
        {
            // Arrange
            using var context = CreateInMemoryContext("GetTaskDtoShouldThrowNotFoundExceptionWhenTaskDoesNotExist");

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => TasksControllerRetriever.GetTaskDto(context, "non_existing_task"));
        }
    }
}
