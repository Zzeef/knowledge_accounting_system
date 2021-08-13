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
    [Authorize(Roles = "admin")]
    public class MarkAdminController : Controller
    {
        private IMarkService MarkService 
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IMarkService>();
            }
        }

        // GET: Mark
        public ActionResult Index()
        {
            return View(MarkService.Marks);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Required] string name)
        {
            if (name == "") 
            {
                ModelState.AddModelError("","Ведите названия предмета");
                return View(); 
            }
            if (ModelState.IsValid)
            {
                OperationDetails result = await MarkService.Create(name);
                if (result.Succedeed)
                {
                    return RedirectToAction("Index");
                }                
            }
            ModelState.AddModelError("","Такой предмет уже существует");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var role = await MarkService.FindMarkByIdAsync(id);
            if (role != null)
            {
                OperationDetails result = await MarkService.DeleteMarkAsync(role);
                if (result.Succedeed)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(result.Property, result.Message);
            }
            return View();
        }
    }
}