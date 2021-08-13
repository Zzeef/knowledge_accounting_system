using System.ComponentModel.DataAnnotations;

namespace knowledge_accounting_system.WEB.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Имя не может быть пустым")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email не может быть пустым")]
        [EmailAddress(ErrorMessage = "Некоректный адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль не может быть пустым")]
        [DataType(DataType.Password)]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=\\S+$).{6,}", ErrorMessage = "Пароль должен содержать больше 1 цифры, 1 буквы, 1 буквы с заглавной, без пробелов и длинее 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Нужно выбрать роль")]
        public string Role { get; set; }
    }
}