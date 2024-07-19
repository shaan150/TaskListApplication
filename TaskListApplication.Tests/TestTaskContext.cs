using Microsoft.EntityFrameworkCore;
using TaskListApplication.Server.Data;

public class TestTaskContext : TaskContext
{
    public TestTaskContext(DbContextOptions<TaskContext> options) : base(options) { }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (SimulateSaveChangesFailure)
        {
            // return 0 
            return Task.FromResult(0);
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    // Simulate a failure when saving changes by taking in a previous value 
    // and returning the value

    public bool SimulateSaveChangesFailure { get; set; }
}
