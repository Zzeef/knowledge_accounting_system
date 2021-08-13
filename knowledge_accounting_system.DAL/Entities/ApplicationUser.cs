using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace knowledge_accounting_system.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ApplicationUserMark> Marks { get; set; }

        public ApplicationUser() 
        {
            Marks = new List<ApplicationUserMark>();
        }
    }
}
