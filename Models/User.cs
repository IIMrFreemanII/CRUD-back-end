using System.ComponentModel.DataAnnotations;

namespace CRUD_back_end.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter user name")]
        public string Name { get; set; }
        [Range(1, 100, ErrorMessage = "Age must be between 1 - 100")]
        [Required(ErrorMessage = "Enter age")]
        public int Age { get; set; }
    }
}