using knowledge_accounting_system.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace knowledge_accounting_system.WEB.Models
{
    public class RoleEditModel
    {
        public RoleDTO Role { get; set; }

        public IEnumerable<object> Members { get; set; }

        public IEnumerable<object> NonMembers { get; set; }
    }
}