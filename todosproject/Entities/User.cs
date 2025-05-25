using System.ComponentModel.DataAnnotations;
using todosproject.Entities.Enums;

namespace todosproject.Entities
{
    public class User
    {
        [Required]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public int age { set; get; }
        [Required]
        public string email { set; get; }
        [Required]
        public string password { set; get; }
        public UserRole Role { get; set; } = UserRole.NUser;

        public List<Todo> todos { set; get; } = new();
    }
}
