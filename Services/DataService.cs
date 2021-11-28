using BlogProject.Data;
using BlogProject.Enums;
using BlogProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            // to create the db frmo migrations
            await _dbContext.Database.MigrateAsync();
            
            // task 1: seed a few roles into the system
            await SeedRolesAsync();

            // task 2: seed a few users into the system
            await SeedUsersAsync();
        }


        private async Task SeedRolesAsync()
        {
            // if there are already roles in the system, do nothing
            if (_dbContext.Roles.Any())
            {
                return;
            }

            // otherwise we want to create a few roles
            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                // need to use the role manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            // if there are already users in the system, do nothing
            if (_dbContext.Users.Any())
            {
                return;
            }

            // for admin
            // step 1: create a new instance of bloguser
            var adminUser = new BlogUser()
            {
                Email = "edwardgreenwood38@gmail.com",
                UserName = "edwardgreenwood38@gmail.com",
                FirstName = "Edward",
                LastName = "Greenwood",
                Displayname = "Edward Greenwood",
                PhoneNumber = "(555) 789 9999",
                EmailConfirmed = true
            };

            // step 2: use the usermanager to create a new user taht is defined by the adminuser
            await _userManager.CreateAsync(adminUser, "Abc&123!");

            // step 3: add this user to the adminstrator role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Adminsitrator.ToString());


            // moderator
            // step 1: create a new instance of bloguser
            var modUser = new BlogUser()
            {
                Email = "jameskirk@mailinator.com",
                UserName = "jameskirk@mailinator.com",
                FirstName = "James",
                LastName = "Kirk",
                Displayname = "James Kirk",
                PhoneNumber = "(555) 789 8888",
                EmailConfirmed = true
            };

            // step 2: use the usermanager to create a new user taht is defined by the moderator
            await _userManager.CreateAsync(modUser, "Abc&123!");

            // step 3: add this user to the moderator role
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }


    }
}
