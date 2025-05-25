using System.ComponentModel.DataAnnotations;

namespace todosproject.Entities.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string username {  get; set; }
        [Required]
        public string password { get; set; }

    }
}
