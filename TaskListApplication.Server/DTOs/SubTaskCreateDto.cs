namespace TaskListApplication.Server.DTOs
{
    public class SubTaskCreateDto
    {
        public string Title { get; set; }
        public bool IsComplete { get; set; }
        public string TaskId { get; set; }
    }
}
