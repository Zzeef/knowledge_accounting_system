using System.ComponentModel.DataAnnotations;

namespace knowledge_accounting_system.WEB.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Заполните поле имени")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Заполните поле пароля")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}