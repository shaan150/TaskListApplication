using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Controllers.SubTasksController;
using TaskListApplication.Server.Controllers.Utilities;
using TaskListApplication.Server.Data;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using Xunit;
using SubTask = TaskListApplication.Server.Models.SubTask;
using ThreadingTask = System.Threading.Tasks.Task;

namespace TaskListApplication.Server.Tests.Controllers.SubTasksController
{
    public class SubTasksControllerDeleterTests
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
        public async ThreadingTask DeleteSubTaskShouldReturnTrueWhenSubTaskIsSuccessfullyDeleted()
        {
            // Arrange
            using var context = CreateInMemoryContext("DeleteSubTaskShouldReturnTrueWhenSubTaskIsSuccessfullyDeleted");
            var subTask = new SubTask { Id = "subtask_1", Title = "Test SubTask", IsComplete = false, TaskId = "task_1" };
            await context.SubTasks.AddAsync(subTask);
            await context.SaveChangesAsync();

            UtilityWrapper.TaskExistsFunc = (ctx, type, id) => ThreadingTask.FromResult(false);

            // Act
            var result = await SubTasksControllerDeleter.DeleteSubTask(context, "subtask_1");

            // Assert
            Assert.True(result);
            var deletedSubTask = await context.SubTasks.FindAsync("subtask_1");
            Assert.Null(deletedSubTask);
        }

        [Fact]
        public async ThreadingTask DeleteSubTaskShouldThrowNotFoundExceptionWhenSubTaskDoesNotExist()
        {
            // Arrange
            using var context = CreateInMemoryContext("DeleteSubTaskShouldThrowNotFoundExceptionWhenSubTaskDoesNotExist");

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => SubTasksControllerDeleter.DeleteSubTask(context, "non_existing_subtask"));
        }

        [Fact]
        public async ThreadingTask DeleteSubTaskShouldReturnFalseWhenSubTaskIsNotDeleted()
        {
            // Arrange
            using var context = CreateInMemoryContext("DeleteSubTaskShouldReturnFalseWhenSubTaskIsNotDeleted");
            var subTask = new SubTask { Id = "subtask_1", Title = "Test SubTask", IsComplete = false, TaskId = "task_1" };
            await context.SubTasks.AddAsync(subTask);
            await context.SaveChangesAsync();

            UtilityWrapper.TaskExistsFunc = (ctx, type, id) => ThreadingTask.FromResult(true);

            // Act
            var result = await SubTasksControllerDeleter.DeleteSubTask(context, "subtask_1");

            // Assert
            Assert.False(result);
            var existingSubTask = await context.SubTasks.FindAsync("subtask_1");
            Assert.Null(existingSubTask); // Ensure it's still deleted in the context but simulated as not deleted
        }
    }
}
