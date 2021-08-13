using knowledge_accounting_system.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace knowledge_accounting_system.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }      
    }

    
}
