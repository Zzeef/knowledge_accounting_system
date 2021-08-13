using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace knowledge_accounting_system.BLL.Interfaces
{
    public interface IRoleService : IDisposable
    {
        IEnumerable<IdentityRole> Roles { get; }
        Task<OperationDetails> Create(string name);
        Task<RoleDTO> FindRoleByIdAsync(string id);
        Task<OperationDetails> DeleteRoleAsync(RoleDTO roleDto);
    }
}
