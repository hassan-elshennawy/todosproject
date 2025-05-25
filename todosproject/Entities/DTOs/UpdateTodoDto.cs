namespace todosproject.Entities.DTOs
{
    public class UpdateTodoDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? isCompleted { get; set; }
    }
}
