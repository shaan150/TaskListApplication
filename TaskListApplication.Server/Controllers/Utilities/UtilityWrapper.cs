using TaskListApplication.Server.Data;
using TaskListApplication.Server.Enums;

namespace TaskListApplication.Server.Controllers.Utilities
{
    public static class UtilityWrapper
    {
        public static Func<TaskContext, string, TaskTypes, Task<string>> FindAvailableIdFunc = Utility.FindAvailableId;
        public static Func<TaskContext, TaskTypes, string, Task<bool>> TaskExistsFunc = Utility.TaskExists;
    }
}
