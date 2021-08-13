using knowledge_accounting_system.DAL.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace knowledge_accounting_system.DAL.Interfaces
{
    public interface IUserMarkManager : IDisposable
    {
        IQueryable<ApplicationUserMark> UserMarks { get; }
        Task<bool> Create(ApplicationUserMark item);
        Task<bool> Delete(ApplicationUserMark item);
        Task<bool> FindUserMarkAsync(ApplicationUserMark item);
    }
}
