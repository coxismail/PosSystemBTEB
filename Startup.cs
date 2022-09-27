using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PosSystem.Models;

[assembly: OwinStartupAttribute(typeof(PosSystem.Startup))]
namespace PosSystem
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            createDefult();
        }

        public void createDefult()
        {
            var context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var user = new ApplicationUser() { UserName = "youremail@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, FullName = "Mohammad Ismail",  Email = "coxismail.bd@gmail.com", PhoneNumber = "01829391440" };
            var exist = UserManager.FindByName("youremail@gmail.com");

            if (exist == null)
            {
                IdentityResult result = UserManager.Create(user, "1234567");
            }

        }
    }
}
