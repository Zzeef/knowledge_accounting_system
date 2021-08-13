using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace knowledge_accounting_system.BLL.Interfaces
{
    public interface IMarkService : IDisposable
    {
        List<MarkDTO> Marks { get; }
        Task<OperationDetails> Create(string name);
        Task<MarkDTO> FindMarkByIdAsync(int id);
        Task<OperationDetails> DeleteMarkAsync(MarkDTO markDTO);
        MarkDTO FindMarkByNameAsync(string name);
    }
}
