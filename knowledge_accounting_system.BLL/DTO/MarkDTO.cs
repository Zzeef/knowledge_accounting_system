using System.Collections.Generic;

namespace knowledge_accounting_system.BLL.DTO
{
    public class MarkDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<AppUserMark> Users { get; set; }
    }

    public class AppUserMark 
    {
        public string UserId { get; set; }

        public int MarkId { get; set; }

        public int Score { get; set; }
    }
}

