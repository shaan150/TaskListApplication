namespace TaskListApplication.Server.Models
{
    public class Task
    {
        public required string Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
        public List<SubTask> SubTasks { get; set; }
    }
}
