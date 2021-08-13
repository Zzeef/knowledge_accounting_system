using knowledge_accounting_system.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace knowledge_accounting_system.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IMarkManager MarkManager { get; }
        IUserMarkManager UserMarkManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IPostManager PostManager { get; }
        Task SaveAsync();
    }
}
