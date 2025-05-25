using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace todosproject.Entities
{
    public class Todo
    {
        [Required]
        public int Id { get; set; }
        public int UserId {  get; set; }
        [Required]
        public string Title {  get; set; } = string.Empty;
        public string? Description{  get; set; }
        public bool IsDone {  get; set; }
        public DateTime CreatedAt { get; set; }

        public User user { get; set; } = null!;
        public Todo() 
        {
            IsDone = false;
            CreatedAt = DateTime.UtcNow.AddHours(3);
        }
    }
}
