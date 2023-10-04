using System.ComponentModel.DataAnnotations;

namespace EasySales.Shared
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email es requerido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password es requerido.")]
        public string Password { get; set; }
    }
}
