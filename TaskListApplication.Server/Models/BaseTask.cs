namespace TaskListApplication.Server.Models
{
    public class BaseTask
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required bool IsComplete { get; set; }
    }
}
