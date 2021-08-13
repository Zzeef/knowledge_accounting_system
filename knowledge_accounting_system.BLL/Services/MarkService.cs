using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Infrastructure;
using knowledge_accounting_system.BLL.Interfaces;
using knowledge_accounting_system.DAL.Entities;
using knowledge_accounting_system.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace knowledge_accounting_system.BLL.Services
{
    public class MarkService : IMarkService
    {
        IUnitOfWork Database { get; set; }

        public MarkService(IUnitOfWork uow)
        {
            Database = uow;

        }

        /// <summary>
        /// Возвращает оценки
        /// </summary>
        public List<MarkDTO> Marks 
        {
            get
            {
                List<MarkDTO> result = new List<MarkDTO>();
                foreach (var mark in Database.MarkManager.Marks.ToList()) 
                {
                    MarkDTO markDTO = new MarkDTO
                    {
                        Id = mark.Id,
                        Name = mark.Name
                    };
                    result.Add(markDTO);
                }
                return result;
            }
        }

        /// <summary>
        /// Удаляет оценку
        /// </summary>
        /// <param name="markDTO">Объект оценки</param>
        public async Task<OperationDetails> DeleteMarkAsync(MarkDTO markDTO)
        {
            ApplicationMark mark = Database.MarkManager.FindMarkByNameAsync(markDTO.Name);
            await Database.MarkManager.Delete(mark);
            return new OperationDetails(true, "Предмет удалён", "");
        }

        /// <summary>
        /// Ищет оценку по id
        /// </summary>
        /// <param name="id">id оценки</param>
        /// <returns>Объект оценки</returns>
        public async Task<MarkDTO> FindMarkByIdAsync(int id) 
        {
            ApplicationMark mark = await Database.MarkManager.FindMarkByIdAsync(id);
            MarkDTO markDTO = new MarkDTO
            {
                Id = mark.Id,
                Name = mark.Name,
                Users = new List<AppUserMark>()
            };
            foreach (var m in mark.Users) 
            {
                AppUserMark appUserMark = new AppUserMark
                {
                    UserId = m.UserId,
                    MarkId = m.MarkId,
                    Score = m.Score
                };
                markDTO.Users.Add(appUserMark);
            }
            return markDTO; 
        }

        /// <summary>
        /// Ищет оценку по name
        /// </summary>
        /// <param name="name">name оценки</param>
        /// <returns>Объект оценки</returns>
        public MarkDTO FindMarkByNameAsync(string name) 
        {
            ApplicationMark mark = Database.MarkManager.FindMarkByNameAsync(name);
            MarkDTO markDTO = new MarkDTO
            {
                Id = mark.Id,
                Name = mark.Name,
                Users = new List<AppUserMark>()
            };
            foreach (var m in mark.Users)
            {
                AppUserMark appUserMark = new AppUserMark
                {
                    UserId = m.UserId,
                    MarkId = m.MarkId,
                    Score = m.Score
                };
                markDTO.Users.Add(appUserMark);
            }
            return markDTO;
        }

        /// <summary>
        /// Создаёт оценку
        /// </summary>
        /// <param name="name">name оценки</param>
        public async Task<OperationDetails> Create(string name) 
        {
            ApplicationMark mark = Database.MarkManager.FindMarkByNameAsync(name);
            if (mark == null) 
            {
                mark = new ApplicationMark { Name = name };
                var result = await Database.MarkManager.Create(mark);
                if (result) 
                {
                    await Database.SaveAsync();
                    return new OperationDetails(true, "Предмет создана", "");
                }
            }
            return new OperationDetails(false, "Такой предмет уже существует", "Mark");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
