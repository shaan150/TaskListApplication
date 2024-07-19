using TaskListApplication.Server.Controllers.TasksController;
using TaskListApplication.Server.Controllers.Utilities;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Exceptions;
using Xunit;
using ThreadingTask = System.Threading.Tasks.Task;
using static TaskListApplication.Tests.TestUtility;
using TaskTypes = TaskListApplication.Server.Enums.TaskTypes;
using TaskTypesExtensions = TaskListApplication.Server.Enums.TaskTypesExtensions;

namespace TaskListApplication.Server.Tests.Controllers.TasksController
{
    public class TasksControllerCreatorTests
    {

        [Fact]
        public async ThreadingTask AddTaskShouldReturnTaskIdWhenTaskIsSuccessfullyCreated()
        {
            // Arrange
            var taskCreateDto = new TaskCreateDto { Title = "Test Task", IsComplete = false };
            var expectedId = "successfull_task";

            using var context = CreateInMemoryContext("AddTaskShouldReturnTaskIdWhenTaskIsSuccessfullyCreated");

            UtilityWrapper.FindAvailableIdFunc = (context, prefix, taskType) => ThreadingTask.FromResult(expectedId);

            // Act
            var result = await TasksControllerCreator.AddTask(context, taskCreateDto);

            // Assert
            Assert.Equal(expectedId, result);
            var task = await context.Tasks.FindAsync(expectedId);
            Assert.NotNull(task);
            Assert.Equal("Test Task", task.Title);
            Assert.False(task.IsComplete);
        }

        [Fact]
        public async ThreadingTask AddTaskShouldThrowCreationExceptionWhenTaskDoesNotExistAfterCreation()
        {
            // Arrange
            var taskCreateDto = new TaskCreateDto { Title = "Test Task", IsComplete = false };
            var expectedId = "null_task";
            var id = "task_9";

            using var context = CreateInMemoryContext("AddTaskShouldThrowCreationExceptionWhenTaskDoesNotExistAfterCreation");
            UtilityWrapper.FindAvailableIdFunc = (context, prefix, taskType) => ThreadingTask.FromResult(id);
            UtilityWrapper.TaskExistsFunc = (context, taskType, id) => ThreadingTask.FromResult(false);

            // Act & Assert
            await Assert.ThrowsAsync<CreationException>(() => TasksControllerCreator.AddTask(context, taskCreateDto));
            var task = await context.Tasks.FindAsync(expectedId);
            Assert.Null(task);
        }
    }
}
