using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using knowledge_accounting_system.BLL.Interfaces;

namespace knowledge_accounting_system.BLL.Infrastructure
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            var mgr = HttpContext.Current.GetOwinContext().GetUserManager<IUserService>();

            return new MvcHtmlString(mgr.GetUserName(id));
        }
    }
}
