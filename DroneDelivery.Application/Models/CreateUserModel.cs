using System.ComponentModel.DataAnnotations;

namespace DroneDelivery.Application.Models
{
    public class CreateUserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
