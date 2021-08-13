using System;
using System.ComponentModel.DataAnnotations;

namespace knowledge_accounting_system.WEB.Models
{
    public class PostAddModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Текст")]
        public string Text { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}