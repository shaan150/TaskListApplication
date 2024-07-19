using TaskListApplication.Server.Controllers.TasksController;
using TaskListApplication.Server.Controllers.Utilities;
using TaskListApplication.Server.Exceptions.TaskExceptions;
using TaskListApplication.Server.Models;
using Xunit;
using Task = TaskListApplication.Server.Models.Task;
using ThreadingTask = System.Threading.Tasks.Task;
using static TaskListApplication.Tests.TestUtility;

namespace TaskListApplication.Server.Tests.Controllers.TasksController
{
    public class TasksControllerDeleterTests
    {

        [Fact]
        public async ThreadingTask DeleteTaskShouldThrowNotFoundExceptionWhenTaskDoesNotExist()
        {
            // Arrange
            var taskId = "non_existing_task";

            using var context = CreateInMemoryContext("DeleteTaskShouldThrowNotFoundExceptionWhenTaskDoesNotExist");
            UtilityWrapper.TaskExistsFunc = (context, taskType, id) => ThreadingTask.FromResult(false);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => TasksControllerDeleter.DeleteTask(context, taskId));
        }

        [Fact]
        public async ThreadingTask DeleteTaskShouldThrowHasSubTasksExceptionWhenTaskHasSubTasks()
        {
            // Arrange
            var taskId = "task_with_subtasks";
            var subTask = new SubTask { Id = "subtask_1", Title = "Sub Task", IsComplete = false, TaskId = taskId };
            var task = new Task { Id = taskId, Title = "Test Task", IsComplete = false, SubTasks = new List<SubTask> { subTask } };

            using var context = CreateInMemoryContext("DeleteTaskShouldThrowHasSubTasksExceptionWhenTaskHasSubTasks");
            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            UtilityWrapper.TaskExistsFunc = (context, taskType, id) => Utility.TaskExists(context, taskType, id);

            // Act & Assert
            await Assert.ThrowsAsync<HasSubTasksException>(() => TasksControllerDeleter.DeleteTask(context, taskId));
        }
    }
}
