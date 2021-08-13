using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace knowledge_accounting_system.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        IEnumerable<IdentityUser> Users { get; }
        Task<UserDTO> FindUserByIdAsync(string id);
        Task<UserDTO> FindUserByNameAsync(string name);
        Task<OperationDetails> DeleteUserAsync(UserDTO userDto);
        Task<OperationDetails> UserValidatorAsync(UserDTO userDto);
        Task<OperationDetails> PasswordValidatorAsync(string password);
        string HashPassword(string password);
        Task<OperationDetails> UserUpdateAsync(UserDTO userDTO);
        string GetUserName(string id);
        Task<OperationDetails> AddToRoleAsync(string id, string roleName);
        Task<OperationDetails> RemoveFromRoleAsync(string id, string roleName);
    }
}
