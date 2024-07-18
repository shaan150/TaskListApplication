using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Data;

namespace TaskListApplication.Server.Controllers.Utilities
{
    public class Utility
    {
        public static string CreateId(string prefix)
        {
            return $"{prefix}-{Guid.NewGuid().ToString()}";
        }

        // check if task exists
        public static async Task<bool> TaskExists(TaskContext context, string id)
        {
            return await context.Tasks.AnyAsync(e => e.Id == id);
        }
    }
}
