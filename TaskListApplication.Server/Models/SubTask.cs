namespace TaskListApplication.Server.Models
{
    public class SubTask
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public bool IsComplete { get; set; }
        public required string TaskId { get; set; }
    }
}
