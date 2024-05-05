using System.ComponentModel.DataAnnotations;

namespace Cupones.Models.Account
{
    public class AccountViewModel
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите ваш логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите ваш логин")]
        public string Login;

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
    }
}
