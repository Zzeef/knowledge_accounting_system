using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace knowledge_accounting_system.DAL.Entities
{
    public class ApplicationMark
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUserMark> Users { get; set; }

        public ApplicationMark() 
        {
            Users = new List<ApplicationUserMark>();
        }
    }
}