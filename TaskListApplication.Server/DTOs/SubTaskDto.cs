namespace TaskListApplication.Server.DTOs
{
    public class SubTaskDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }

        public string TaskId { get; set; }
    }
}
