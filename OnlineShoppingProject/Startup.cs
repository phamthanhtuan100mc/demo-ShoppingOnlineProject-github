using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OnlineShoppingProject.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineShoppingProject.Startup))]
namespace OnlineShoppingProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // tạo ADMIN đầu tiên và tạo tài khoải ADMIN mặc định
            if (!roleManager.RoleExists("ADMIN"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "ADMIN";
                roleManager.Create(role);

                // Tạo super admin, ngừi có quyền cao nhất để quản trị web
                // gõ đúng như vầy
                var user = new ApplicationUser();
                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";
                string userPWD = "123456";
                var chkUser = userManager.Create(user, userPWD);

                // Thêm user đầu tiên vào là administrator
                if (chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "ADMIN");
                }
            }

            // In startup create first MANAGER role and crate a default MANAGER user
            if (!roleManager.RoleExists("MANAGER"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "MANAGER";
                roleManager.Create(role);

                // tạo tài khoản manager thuộc quyền MANAGER 
                var uManager = new ApplicationUser();
                uManager.UserName = "manager@gmail.com";
                uManager.Email = "manager@gmail.com";
                string userManagerPWD = "123456";
                var chkUserManager = userManager.Create(uManager, userManagerPWD);

                // thêm user manager
                if (chkUserManager.Succeeded)
                {
                    var result = userManager.AddToRole(uManager.Id, "MANAGER");
                }

                // Tạo tài khoản editor thuộc quyền MANAGER
                var userEditor = new ApplicationUser();
                userEditor.UserName = "editor@gmail.com";
                userEditor.Email = "editor@gmail.com";
                string userEditorPWD = "123456";
                var chkUserEditor = userManager.Create(userEditor, userEditorPWD);
                if (chkUserEditor.Succeeded)
                {
                    var result = userManager.AddToRole(userEditor.Id, "MANAGER");
                }
            }

            // in startup create first MEMBER role
            if (!roleManager.RoleExists("MEMBER"))
            {
                // đầu tiên, tạo quyền MEMBER
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "MEMBER";
                roleManager.Create(role);
            }
        }
    }
}