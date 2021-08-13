using System.ComponentModel.DataAnnotations;

namespace knowledge_accounting_system.WEB.Models
{
    public class MarkModificationModel
    {
        [Required]
        public string MarkName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}