using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Infrastructure;
using knowledge_accounting_system.BLL.Interfaces;
using knowledge_accounting_system.DAL.Entities;
using knowledge_accounting_system.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace knowledge_accounting_system.BLL.Services
{
    public class RoleService : IRoleService
    {
        IUnitOfWork Database { get; set; }

        public RoleService(IUnitOfWork uow) 
        {
            Database = uow;
        }

        /// <summary>
        /// Возвращает список ролей
        /// </summary>
        public IEnumerable<IdentityRole> Roles => Database.RoleManager.Roles;

        /// <summary>
        /// Создаёт новую роль
        /// </summary>
        /// <param name="name">Название роли</param>
        public async Task<OperationDetails> Create(string name) 
        {
            ApplicationRole role = await Database.RoleManager.FindByNameAsync(name);
            if (role == null)
            {
                role = new ApplicationRole { Name = name };
                var result = await Database.RoleManager.CreateAsync(role);
                if (result.Errors.Any()) 
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }
                await Database.SaveAsync();
                return new OperationDetails(true, "Роль создана", "");
            }
            return new OperationDetails(false, "Такая роль уже существует", "Role");
        }
        /// <summary>
        /// Ищет роль по id 
        /// </summary>
        /// <param name="id">id роли</param>
        /// <returns>Объект роли</returns>
        public async Task<RoleDTO> FindRoleByIdAsync(string id)
        {
            ApplicationRole role = await Database.RoleManager.FindByIdAsync(id);
            RoleDTO roleDto = new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Users = role.Users
            };
            return roleDto;
        }

        /// <summary>
        /// Ищет роль по Name 
        /// </summary>
        /// <param name="name">name роли</param>
        /// <returns>Объект роли</returns>
        public async Task<RoleDTO> FindRoleByNameAsync(string name)
        {
            ApplicationRole role = await Database.RoleManager.FindByNameAsync(name);
            RoleDTO roleDto = new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Users = role.Users
            };
            return roleDto;
        }
        /// <summary>
        /// Удаляет роль
        /// </summary>
        /// <param name="roleDto">Объект роли</param>
        public async Task<OperationDetails> DeleteRoleAsync(RoleDTO roleDto) 
        {
            ApplicationRole role = await Database.RoleManager.FindByNameAsync(roleDto.Name);
            var result = await Database.RoleManager.DeleteAsync(role);
            if (result.Errors.Any()) 
            {
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
            }
            return new OperationDetails(true, "Роль удалена", "");
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
