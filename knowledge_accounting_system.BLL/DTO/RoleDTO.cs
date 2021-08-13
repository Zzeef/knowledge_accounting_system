using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace knowledge_accounting_system.BLL.DTO
{
    public class RoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<IdentityUserRole> Users { get; set; }
    }
}
