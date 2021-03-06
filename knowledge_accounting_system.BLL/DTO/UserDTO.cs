using System.Collections.Generic;

namespace knowledge_accounting_system.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public List<AppUserMark> Marks { get; set; }
    }
}
