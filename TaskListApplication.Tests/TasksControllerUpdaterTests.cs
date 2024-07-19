using TaskListApplication.Server.Controllers.TasksController;
using TaskListApplication.Server.Controllers.Utilities;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using Xunit;
using Task = TaskListApplication.Server.Models.Task;
using ThreadingTask = System.Threading.Tasks.Task;
using static TaskListApplication.Tests.TestUtility;

namespace TaskListApplication.Server.Tests.Controllers.TasksController
{
    public class TasksControllerUpdaterTests
    {
        [Fact]
        public async ThreadingTask UpdateTaskShouldReturnTrueWhenTaskIsSuccessfullyUpdated()
        {
            // Arrange
            var taskDto = new TaskDto { Id = "task_1", Title = "Updated Task", IsComplete = true };
            var initialTask = new Task { Id = "task_1", Title = "Initial Task", IsComplete = false };

            using var context = CreateInMemoryContext("UpdateTaskShouldReturnTrueWhenTaskIsSuccessfullyUpdated");
            await context.Tasks.AddAsync(initialTask);
            await context.SaveChangesAsync();

            UtilityWrapper.TaskExistsFunc = (context, taskType, id) => ThreadingTask.FromResult(true);

            // Act
            var result = await TasksControllerUpdater.UpdateTask(context, "task_1", taskDto);

            // Assert
            Assert.True(result);
            var updatedTask = await context.Tasks.FindAsync("task_1");
            Assert.NotNull(updatedTask);
            Assert.Equal("Updated Task", updatedTask.Title);
            Assert.True(updatedTask.IsComplete);
        }

        [Fact]
        public async ThreadingTask UpdateTaskShouldThrowNotFoundExceptionWhenTaskDoesNotExist()
        {
            // Arrange
            var taskDto = new TaskDto { Id = "non_existing_task", Title = "Updated Task", IsComplete = true };

            using var context = CreateInMemoryContext("UpdateTaskShouldThrowNotFoundExceptionWhenTaskDoesNotExist");

            UtilityWrapper.TaskExistsFunc = (context, taskType, id) => ThreadingTask.FromResult(false);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => TasksControllerUpdater.UpdateTask(context, "non_existing_task", taskDto));
        }
    }
}
