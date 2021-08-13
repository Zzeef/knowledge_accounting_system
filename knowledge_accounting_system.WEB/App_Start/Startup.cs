using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using knowledge_accounting_system.BLL.Services;
using Microsoft.AspNet.Identity;
using knowledge_accounting_system.BLL.Interfaces;

[assembly: OwinStartup(typeof(knowledge_accounting_system.WEB.App_Start.Startup))]

namespace knowledge_accounting_system.WEB.App_Start
{
    public class Startup
    {
        readonly IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            
            app.CreatePerOwinContext<IPostService>(CreatePostService);
            app.CreatePerOwinContext<IUserMarkService>(CreateUserMarkService);
            app.CreatePerOwinContext<IMarkService>(CreateMarkService);
            app.CreatePerOwinContext<IRoleService>(CreateRoleService);
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("Database");
        }

        private IRoleService CreateRoleService() 
        {
            return serviceCreator.CreateRoleService("Database");
        }

        private IMarkService CreateMarkService()
        {
            return serviceCreator.CreateMarkService("Database");
        }

        private IUserMarkService CreateUserMarkService() 
        {
            return serviceCreator.CreateUserMarkService("Database");
        }

        private IPostService CreatePostService() 
        {
            return serviceCreator.CreatePostService("Database");
        }
    }
}