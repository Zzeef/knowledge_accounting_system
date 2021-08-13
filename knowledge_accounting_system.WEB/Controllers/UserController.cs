using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Infrastructure;
using knowledge_accounting_system.BLL.Interfaces;
using knowledge_accounting_system.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace knowledge_accounting_system.WEB.Controllers
{
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        private IMarkService MarkService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IMarkService>();
            }
        }

        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IUserMarkService UserMarkService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserMarkService>();
            }
        }

        public async Task<ActionResult> MarkMenu()
        {
            var user = await UserService.FindUserByNameAsync(User.Identity.Name);
            List<MarkAddModel> modelList = new List<MarkAddModel>();
            foreach (var i in user.Marks)
            {
                MarkDTO markName = await MarkService.FindMarkByIdAsync(i.MarkId);
                MarkAddModel model = new MarkAddModel
                {
                    Name = markName.Name,
                    Score = i.Score
                };
                modelList.Add(model);
            }
            return View(modelList);
        }

        public async Task<ActionResult> Add()
        {
            List<MarkDTO> result = new List<MarkDTO>();
            IEnumerable<UserMarkDTO> marks;
            UserDTO user =  await UserService.FindUserByNameAsync(User.Identity.Name);
            marks = UserMarkService.UserMarks.Where(x => x.UserId == user.Id);
            if (marks.Count() == 0) { return View(MarkService.Marks); }
            foreach (var i in MarkService.Marks) 
            {
                if (!marks.Any(x => x.MarkId == i.Id)) 
                {
                    result.Add(i);
                }
            }
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> Add(MarkAddModel item)
        {
            var user =  await UserService.FindUserByNameAsync(item.UserName);
            var mark =  MarkService.FindMarkByNameAsync(item.Name);

            UserMarkDTO newItem = new UserMarkDTO
            {
                UserId = user.Id,
                MarkId = mark.Id,
                Score = item.Score
            };
            var result = await UserMarkService.Create(newItem);
            if (result.Succedeed) 
            {
                return RedirectToAction("MarkMenu", "User");
            }
            ModelState.AddModelError("", "Предмет уже добавлен");
            return View(MarkService.Marks);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMark(string UserName, string MarkName, int Score) 
        {
            var user = await UserService.FindUserByNameAsync(UserName);
            var mark = MarkService.FindMarkByNameAsync(MarkName);

            UserMarkDTO newItem = new UserMarkDTO
            {
                UserId = user.Id,
                MarkId = mark.Id,
                Score = Score
            };
            await UserMarkService.Delete(newItem);
            return RedirectToAction("MarkMenu");
        }

        public new async Task<ActionResult> Profile()
        {
            UserDTO user = await UserService.FindUserByNameAsync(User.Identity.Name);
            return View(user);
        }

        public async Task<ActionResult> Edit()
        {
            var user = await UserService.FindUserByNameAsync(User.Identity.Name);
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            UserDTO user = await UserService.FindUserByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                OperationDetails validEmail = await UserService.UserValidatorAsync(user);
                if (!validEmail.Succedeed)
                {
                    ModelState.AddModelError(validEmail.Property, validEmail.Message);
                }

                OperationDetails validPass = null;
                if (password != string.Empty)
                {
                    validPass = await UserService.PasswordValidatorAsync(password);
                    if (validPass.Succedeed)
                    {
                        user.Password = UserService.HashPassword(password);
                    }
                    else
                    {
                        ModelState.AddModelError(validPass.Property, validPass.Message);
                    }
                }

                if ((validEmail.Succedeed && validPass == null) || (validEmail.Succedeed && password != string.Empty && validPass.Succedeed))
                {
                    OperationDetails result = await UserService.UserUpdateAsync(user);
                    if (result.Succedeed)
                    {
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        ModelState.AddModelError(result.Property, result.Message);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }
            return View(user);
        }
    }
}