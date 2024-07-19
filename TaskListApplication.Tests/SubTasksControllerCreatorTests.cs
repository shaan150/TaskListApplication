using TaskListApplication.Server.Controllers.SubTasksController;
using TaskListApplication.Server.Controllers.Utilities;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Exceptions;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using Xunit;
using static TaskListApplication.Tests.TestUtility;
using ThreadingTask = System.Threading.Tasks.Task;
using Task = TaskListApplication.Server.Models.Task;

namespace TaskListApplication.Server.Tests.Controllers.SubTasksController
{
    public class SubTasksControllerCreatorTests
    {

        [Fact]
        public async ThreadingTask AddSubTaskShouldReturnSubTaskIdWhenSubTaskIsSuccessfullyCreated()
        {
            // Arrange
            using var context = CreateInMemoryContext("AddSubTaskShouldReturnSubTaskIdWhenSubTaskIsSuccessfullyCreated");
            var task = new Task { Id = "task_1", Title = "Test Task", IsComplete = false };
            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            var subTaskCreateDto = new SubTaskCreateDto { TaskId = "task_1", Title = "Test SubTask", IsComplete = false };
            var expectedId = "subtask_1";

            UtilityWrapper.FindAvailableIdFunc = (ctx, prefix, type) => ThreadingTask.FromResult(expectedId);
            UtilityWrapper.TaskExistsFunc = (ctx, type, id) => ThreadingTask.FromResult(true);

            // Act
            var result = await SubTasksControllerCreator.AddSubTask(context, subTaskCreateDto);

            // Assert
            Assert.Equal(expectedId, result);
            var subTask = await context.SubTasks.FindAsync(expectedId);
            Assert.NotNull(subTask);
            Assert.Equal("Test SubTask", subTask.Title);
            Assert.False(subTask.IsComplete);
            Assert.Equal("task_1", subTask.TaskId);
        }

        [Fact]
        public async ThreadingTask AddSubTaskShouldThrowNotFoundExceptionWhenTaskDoesNotExist()
        {
            // Arrange
            using var context = CreateInMemoryContext("AddSubTaskShouldThrowNotFoundExceptionWhenTaskDoesNotExist");

            var subTaskCreateDto = new SubTaskCreateDto { TaskId = "non_existing_task", Title = "Test SubTask", IsComplete = false };

            UtilityWrapper.TaskExistsFunc = (ctx, type, id) => ThreadingTask.FromResult(false);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => SubTasksControllerCreator.AddSubTask(context, subTaskCreateDto));
        }

        [Fact]
        public async ThreadingTask AddSubTaskShouldThrowCreationExceptionWhenSubTaskDoesNotExistAfterCreation()
        {
            // Arrange
            using var context = CreateInMemoryContext("AddSubTaskShouldThrowCreationExceptionWhenSubTaskDoesNotExistAfterCreation");
            var task = new Task { Id = "task_1", Title = "Test Task", IsComplete = false };
            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            var subTaskCreateDto = new SubTaskCreateDto { TaskId = "task_1", Title = "Test SubTask", IsComplete = false };
            var expectedId = "subtask_1";

            UtilityWrapper.FindAvailableIdFunc = (ctx, prefix, type) => ThreadingTask.FromResult(expectedId);
            UtilityWrapper.TaskExistsFunc = (ctx, type, id) => ThreadingTask.FromResult(false);

            // Act & Assert
            await Assert.ThrowsAsync<CreationException>(() => SubTasksControllerCreator.AddSubTask(context, subTaskCreateDto));
        }
    }
}
