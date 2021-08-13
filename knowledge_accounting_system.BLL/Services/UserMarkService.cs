using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Infrastructure;
using knowledge_accounting_system.BLL.Interfaces;
using knowledge_accounting_system.DAL.Entities;
using knowledge_accounting_system.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace knowledge_accounting_system.BLL.Services
{
    public class UserMarkService : IUserMarkService
    {
        IUnitOfWork Database { get; set; }

        public UserMarkService(IUnitOfWork uow)
        {
            Database = uow;

        }

        public List<UserMarkDTO> UserMarks
        {
            get
            {
                List<UserMarkDTO> result = new List<UserMarkDTO>();
                foreach (var userMark in Database.UserMarkManager.UserMarks)
                {
                    UserMarkDTO UserMarkDTO = new UserMarkDTO
                    {
                        UserId = userMark.UserId,
                        MarkId = userMark.MarkId,
                        Score = userMark.Score
                    };
                    result.Add(UserMarkDTO);
                }
                return result;
            }
        }

        /// <summary>
        /// Создаёт связь предмета и юзера
        /// </summary>
        /// <param name="item">Объект связи</param>
        public async Task<OperationDetails> Create(UserMarkDTO item)
        {         
            ApplicationUserMark newItem = new ApplicationUserMark
            {
                UserId = item.UserId,
                MarkId = item.MarkId,
                Score = item.Score
            };
            if (!await Database.UserMarkManager.FindUserMarkAsync(newItem)) 
            {
                await Database.UserMarkManager.Create(newItem);
                await Database.SaveAsync();
                return new OperationDetails(true, "Предмет добавлен", "");
            }
            return new OperationDetails(false, "Предмет не добавлен", "");
        }

        /// <summary>
        /// Удаляет связь предмета и юзера
        /// </summary>
        /// <param name="item">Объект связи</param>
        public async Task<OperationDetails> Delete(UserMarkDTO item) 
        {
            ApplicationUserMark newItem = new ApplicationUserMark
            { 
                UserId = item.UserId,
                MarkId = item.MarkId,
                Score = item.Score
            };
            if (await Database.UserMarkManager.FindUserMarkAsync(newItem)) 
            {
                await Database.UserMarkManager.Delete(newItem);
                await Database.SaveAsync();
                return new OperationDetails(true, "Предмет удалён", "");
            }
            return new OperationDetails(false, "Предмет не удалён", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
