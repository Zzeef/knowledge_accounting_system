using knowledge_accounting_system.DAL.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace knowledge_accounting_system.DAL.Interfaces
{
    public interface IPostManager : IDisposable
    {
        Task<bool> AddPost(ApplicationPost item);
        Task<bool> DeletePost(int id);
        Task<ApplicationPost> FindPostByIdAsync(int id);
        IQueryable<ApplicationPost> Posts { get; }

    }
}
