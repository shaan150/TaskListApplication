using TaskListApplication.Server.Controllers.SubTasksController;
using TaskListApplication.Server.DTOs;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using static TaskListApplication.Tests.TestUtility;
using Xunit;
using SubTask = TaskListApplication.Server.Models.SubTask;
using ThreadingTask = System.Threading.Tasks.Task;

namespace TaskListApplication.Server.Tests.Controllers.SubTasksController
{
    public class SubTasksControllerUpdaterTests
    {
        [Fact]
        public async ThreadingTask UpdateSubTaskShouldUpdateSubTaskDetailsWhenSubTaskExists()
        {
            // Arrange
            using var context = CreateInMemoryContext("UpdateSubTaskShouldUpdateSubTaskDetailsWhenSubTaskExists");
            var subTask = new SubTask { Id = "subtask_1", Title = "Initial Title", IsComplete = false, TaskId = "task_1" };
            await context.SubTasks.AddAsync(subTask);
            await context.SaveChangesAsync();

            var subTaskDto = new SubTaskDto { Id = "subtask_1", Title = "Updated Title", IsComplete = true, TaskId = "task_1" };

            // Act
            await SubTasksControllerUpdater.UpdateSubTask(context, subTaskDto);

            // Assert
            var updatedSubTask = await context.SubTasks.FindAsync("subtask_1");
            Assert.NotNull(updatedSubTask);
            Assert.Equal("Updated Title", updatedSubTask.Title);
            Assert.True(updatedSubTask.IsComplete);
            Assert.Equal("task_1", updatedSubTask.TaskId);
        }

        [Fact]
        public async ThreadingTask UpdateSubTaskShouldThrowNotFoundExceptionWhenSubTaskDoesNotExist()
        {
            // Arrange
            using var context = CreateInMemoryContext("UpdateSubTaskShouldThrowNotFoundExceptionWhenSubTaskDoesNotExist");
            var subTaskDto = new SubTaskDto { Id = "non_existing_subtask", Title = "Updated Title", IsComplete = true, TaskId = "task_1" };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => SubTasksControllerUpdater.UpdateSubTask(context, subTaskDto));
        }
    }
}
