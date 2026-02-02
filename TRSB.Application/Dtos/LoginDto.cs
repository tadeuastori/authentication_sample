using System.ComponentModel.DataAnnotations;

namespace TRSB.Application.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "User name OR Email is required")]
        public string LoginValue { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
