using knowledge_accounting_system.BLL.Infrastructure;
using knowledge_accounting_system.BLL.Interfaces;
using knowledge_accounting_system.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace knowledge_accounting_system.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleAdminController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IRoleService RoleService 
        {
            get 
            {
                return HttpContext.GetOwinContext().GetUserManager<IRoleService>();
            }
        }
        // GET: Role
        public ActionResult Index()
        {
            return View(RoleService.Roles.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                OperationDetails result = await RoleService.Create(name);
                if (result.Succedeed)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(result.Property, result.Message);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleService.FindRoleByIdAsync(id);
            if (role != null) 
            {
                OperationDetails result = await RoleService.DeleteRoleAsync(role);
                if (result.Succedeed) 
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(result.Property, result.Message);
            }
            return View("Error", new string[] { "Роль не найдена" });
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null) { return RedirectToAction("Index"); }
            var role = await RoleService.FindRoleByIdAsync(id);
            string[] memberIDs = role.Users.Select(x => x.UserId).ToArray();
            IEnumerable<object> members = UserService.Users.Where(x => memberIDs.Any(y => y == x.Id));

            IEnumerable<object> nonMembers = UserService.Users.Except(members);

            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleModificationModel model)
        {
            OperationDetails result;
            if (ModelState.IsValid) 
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { }) 
                {
                    result = await UserService.AddToRoleAsync(userId, model.RoleName);

                    if (!result.Succedeed) 
                    {
                        return View("Error", result.Message);
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    result = await UserService.RemoveFromRoleAsync(userId,
                    model.RoleName);

                    if (!result.Succedeed)
                    {
                        return View("Error", result.Message);
                    }
                }
                return RedirectToAction("Index");
            }
            return View("Error", new string[] { "Роль не найдена" });
        }
    }
}