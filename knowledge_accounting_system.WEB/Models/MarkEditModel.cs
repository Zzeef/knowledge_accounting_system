using knowledge_accounting_system.BLL.DTO;
using System.Collections.Generic;

namespace knowledge_accounting_system.WEB.Models
{
    public class MarkEditModel
    {
        public MarkDTO Mark { get; set; }

        public IEnumerable<object> Members { get; set; }

        public IEnumerable<object> NonMembers { get; set; }
    }
}