using knowledge_accounting_system.DAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace knowledge_accounting_system.DAL.Interfaces
{
    public interface IMarkManager : IDisposable
    {
        IQueryable<ApplicationMark> Marks { get; }
        Task<bool> Create(ApplicationMark item);
        Task<bool> Delete(ApplicationMark item);
        Task<ApplicationMark> FindMarkByIdAsync(int id);
        ApplicationMark FindMarkByNameAsync(string name);
    }
}
