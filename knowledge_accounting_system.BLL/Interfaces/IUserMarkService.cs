using knowledge_accounting_system.BLL.Infrastructure;
using knowledge_accounting_system.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace knowledge_accounting_system.BLL.Interfaces
{
    public interface IUserMarkService : IDisposable
    {
        List<UserMarkDTO> UserMarks { get; }
        Task<OperationDetails> Create(UserMarkDTO name);
        Task<OperationDetails> Delete(UserMarkDTO item);
    }
}
