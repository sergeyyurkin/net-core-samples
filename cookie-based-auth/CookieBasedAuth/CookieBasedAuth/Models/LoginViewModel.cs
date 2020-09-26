using System.ComponentModel.DataAnnotations;

namespace CookieBasedAuth.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
    }
}
