namespace TaskListApplication.Server.Models
{
    public class Task : BaseTask
    {
        private List<SubTask> subTasks = new List<SubTask>();

        public List<SubTask> SubTasks { get => subTasks; set => subTasks = value; }
    }
}
