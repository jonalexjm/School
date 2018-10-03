
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using School.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Colegio.Backend.Helpers
{
    public class Utilities
    {
        private static ApplicationDbContext userContext = new ApplicationDbContext();// access a security tables 
        private static LocalDataContext db = new LocalDataContext();

        public static void checkRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }

        public static void CheckSuperUser(string role)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var email = WebConfigurationManager.AppSettings["AdminUser"];
            var password = WebConfigurationManager.AppSettings["AdminPassWord"];
            var userASP = userManager.FindByName(email);
            if (userASP == null)
            {
                CreateUserASP(email, role, password);
                return;
            }
        }


        public static void CreateUserASP(string email)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email,


            };

            userManager.Create(userASP, email);
        }

        public static void CreateUserASP(string email, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email,


            };

            userManager.Create(userASP, email);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static void CreateUserASP(string email, string roleName, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email,


            };

            userManager.Create(userASP, email);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static void AddRoleToUser(string email, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userAsp = userManager.FindByEmail(email);
            if (userAsp == null)
            {
                return;

            }

            userManager.AddToRole(userAsp.Id, roleName);

        }
    }

}