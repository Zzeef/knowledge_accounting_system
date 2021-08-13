using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Infrastructure;
using knowledge_accounting_system.BLL.Interfaces;
using knowledge_accounting_system.BLL.Services;
using knowledge_accounting_system.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace knowledge_accounting_system.WEB.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {

        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(UserService.Users);
        }

        public ActionResult Create()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    Role = "user"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            UserDTO user = await UserService.FindUserByIdAsync(id);
            if (user != null) 
            {
                OperationDetails operationDetails = await UserService.DeleteUserAsync(user);
                if (operationDetails.Succedeed)
                {
                    return RedirectToAction("Index");
                }
                else 
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
            }
            return View();
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null) { return RedirectToAction("Index"); }
            UserDTO user = await UserService.FindUserByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string userName, string email, string password)
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

                user.Name = userName;

                if ((validEmail.Succedeed && validPass == null) || (validEmail.Succedeed && password != string.Empty && validPass.Succedeed)) 
                {
                    OperationDetails result = await UserService.UserUpdateAsync(user);
                    if (result.Succedeed)
                    {
                        return RedirectToAction("Index");
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
