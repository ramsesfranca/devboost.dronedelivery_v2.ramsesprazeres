using System.ComponentModel.DataAnnotations;

namespace DroneDelivery.Application.Models
{
    public class LoginModel
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
