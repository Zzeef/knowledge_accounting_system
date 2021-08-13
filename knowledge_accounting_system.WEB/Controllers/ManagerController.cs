using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Infrastructure;
using knowledge_accounting_system.BLL.Interfaces;
using knowledge_accounting_system.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace knowledge_accounting_system.WEB.Controllers
{
    [Authorize(Roles = "manager")]
    public class ManagerController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IPostService PostService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IPostService>();
            }
        }

        private IMarkService MarkService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IMarkService>();
            }
        }

        private IUserMarkService UserMarkService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserMarkService>();
            }
        }

        public new async Task<ActionResult> Profile()
        {
            UserDTO user = await UserService.FindUserByNameAsync(User.Identity.Name);
            return View(user);
        }

        public ActionResult Search()
        {
            return View(MarkService.Marks);
        }

        public async Task<ActionResult> SearchResult(bool Sort, string Mark, int Score)
        {
            List<UserMarkModel> itemList = new List<UserMarkModel>();            

            foreach (var i in UserMarkService.UserMarks)
            {
                var mark = await MarkService.FindMarkByIdAsync(i.MarkId);
                var name = await UserService.FindUserByIdAsync(i.UserId);
                UserMarkModel item = new UserMarkModel 
                {
                    Mark = mark.Name,
                    Name = name.Name,
                    Score = i.Score
                };
                itemList.Add(item);
            }

            if (Mark != "Предметы")
            {
                itemList = itemList.Where(x => x.Mark == Mark).ToList();
            }

            if (Score != 0) 
            {
                itemList = itemList.Where(x => x.Score == Score).ToList();
            }

            if (Sort)
            {
                itemList = itemList.OrderBy(x => x.Name).ThenBy(x => x.Mark).ToList();
            }
            else 
            {
                itemList = itemList.OrderByDescending(x => x.Name).ThenByDescending(x => x.Mark).ToList();
            }
            
            return PartialView(itemList);
        }

        public ActionResult AddPost() 
        {
            return View(new PostAddModel());
        }

        [HttpPost]
        public async Task<ActionResult> AddPost(PostAddModel item)
        {
            if (ModelState.IsValid) 
            {
                PostDTO newItem = new PostDTO
                {
                    Name = item.Name,
                    Title = item.Title,
                    Text = item.Text,
                    Date = item.Date
                };
                var result = await PostService.AddPost(newItem);
                if (result.Succedeed)
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Profile");
            }
            return View(item);
        }

        public ActionResult DeletePost() 
        {
            return View(PostService.Posts);
        }

        [HttpPost]
        public async Task<ActionResult> DeletePost(int id)
        {
            var result = await PostService.DeletePost(id);
            if (result.Succedeed) 
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Неудалось удалить пост");
            return View(PostService.Posts);
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