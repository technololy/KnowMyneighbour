using KnowMyneighbour.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowMyneighbour.Magic
{
    public class Role
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public bool AddUserAndRole(string LoginUserName, string LoginPassWord)
        {

            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(new ApplicationDbContext()));
            ir = rm.Create(new IdentityRole("admin"));

            var user = new ApplicationUser() { UserName = "admin@admin.com" };
            var result = UserManager.Create(user, "somepassword");
            UserManager.AddToRole(user.Id, "admin");

            return true;
        }
    }
}