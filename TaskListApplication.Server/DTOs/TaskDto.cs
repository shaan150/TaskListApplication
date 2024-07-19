namespace TaskListApplication.Server.DTOs
{
    public class TaskDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
        public List<SubTaskDto> SubTasks { get; set; }
        public int SubTasksCount { get; set; }
    }
}
