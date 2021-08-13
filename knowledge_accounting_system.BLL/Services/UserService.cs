using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Infrastructure;
using knowledge_accounting_system.DAL.Entities;
using knowledge_accounting_system.DAL.Interfaces;
using knowledge_accounting_system.BLL.Interfaces;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace knowledge_accounting_system.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }
        /// <summary>
        /// Возвращает пользователей  
        /// </summary>
        public IEnumerable<IdentityUser> Users => Database.UserManager.Users;

        /// <summary>
        /// Создаёт пользователя
        /// </summary>
        /// <param name="userDto">Объект пользователя</param>
        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Name };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Any())
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        /// <summary>
        /// Аутентифицирует пользователя
        /// </summary>
        /// <param name="userDto">Объект пользователя</param>
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Name, userDto.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        /// <summary>
        /// Ищет юзера по id
        /// </summary>
        /// <param name="id">id юзера</param>
        /// <returns>Объект пользователя</returns>
        public async Task<UserDTO> FindUserByIdAsync(string id) 
        {
            ApplicationUser user = await Database.UserManager.FindByIdAsync(id);
            UserDTO userDto = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.PasswordHash,
                Name = user.UserName
            };
            return userDto;
        }

        /// <summary>
        /// Ищет юзера по Name
        /// </summary>
        /// <param name="name">name юзера</param>
        /// <returns>Объект пользователя</returns>
        public async Task<UserDTO> FindUserByNameAsync(string name) 
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(name);
            UserDTO userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.PasswordHash,
                Name = user.UserName,
                Marks = new List<AppUserMark>()
            };
            foreach (var m in user.Marks)
            {
                AppUserMark appUserMark = new AppUserMark
                {
                    UserId = m.UserId,
                    MarkId = m.MarkId,
                    Score = m.Score
                };
                userDTO.Marks.Add(appUserMark);
            }
            return userDTO;
        }

        /// <summary>
        /// Удаляет юзера
        /// </summary>
        /// <param name="userDto">Объект юзера</param>
        public async Task<OperationDetails> DeleteUserAsync(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userDto.Name);
            var result = await Database.UserManager.DeleteAsync(user);
            if (result.Errors.Any()) 
            {
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
            }
            return new OperationDetails(true, "Пользователь удалён", "");
        }

        public async Task<OperationDetails> UserValidatorAsync(UserDTO userDTO) 
        {
            ApplicationUser user = new ApplicationUser
            {
                Id = userDTO.Id,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Name
            };
            var result = await Database.UserManager.UserValidator.ValidateAsync(user);
            if (result.Succeeded) 
            {
                return new OperationDetails(true, "Валидация прошла успешно", "");
            }
            return new OperationDetails(false, "Валидация провалена", "");
        }

        public async Task<OperationDetails> PasswordValidatorAsync(string password) 
        {
            var result = await Database.UserManager.PasswordValidator.ValidateAsync(password);
            if (result.Succeeded) 
            {
                return new OperationDetails(true, "Валидация прошла успешно", "");
            }
            return new OperationDetails(false, "Валидация провалена", "");
        }

        public string HashPassword(string password) 
        {
            return Database.UserManager.PasswordHasher.HashPassword(password);
        }

        public async Task<OperationDetails> UserUpdateAsync(UserDTO userDTO) 
        {
            ApplicationUser user = await Database.UserManager.FindByIdAsync(userDTO.Id);
            user.Email = userDTO.Email; user.UserName = userDTO.Name; user.PasswordHash = userDTO.Password;

            var result = await Database.UserManager.UpdateAsync(user);
            if (result.Succeeded) 
            {
                return new OperationDetails(true, "Пользователь обновлён", "");
            }
            return new OperationDetails(false, "Произошла ошибка при обновлении пользвателя", "");
        }

        public string GetUserName(string id) 
        {
            return Database.UserManager.FindByIdAsync(id).Result.UserName;
        }

        public async Task<OperationDetails> AddToRoleAsync(string id, string roleName) 
        {
            var result = await Database.UserManager.AddToRoleAsync(id,roleName);
            if (result.Succeeded) 
            {
                return new OperationDetails(true, "Роль пользователя обновлёна", "");
            }
            return new OperationDetails(false, "Произошла ошибка при обновлении пользвателя", "");
        }

        public async Task<OperationDetails> RemoveFromRoleAsync(string id, string roleName) 
        {
            var result = await Database.UserManager.RemoveFromRoleAsync(id, roleName);
            if (result.Succeeded) 
            {
                return new OperationDetails(true, "Роль пользователя обновлёна", "");
            }
            return new OperationDetails(false, "Произошла ошибка при обновлении пользвателя", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
