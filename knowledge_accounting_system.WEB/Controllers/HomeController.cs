using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using knowledge_accounting_system.BLL.Interfaces;

namespace knowledge_accounting_system.WEB.Controllers
{
    public class HomeController : Controller
    {
        private IPostService PostService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IPostService>();
            }
        }

        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("admin")) 
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(PostService.Posts);
        }

    }
}